using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDNNX {

	public partial class Form1 : Form {

        public Form1() {
            //Initialize stuff
			InitializeComponent();

            //Check for pre-req. files
            if (!File.Exists(Directory.GetCurrentDirectory() + "/config.ini")) {
                Settings.Create();
                Keys.Create();
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\NXCrypt.dll")) {
				MessageBox.Show("Missing NXCrypt.dll dependency in root dir.");
				Environment.Exit(0);
			}

            string cert = INIFile.Read("settings", "cert");
            if (!File.Exists(cert) || !Regex.IsMatch(cert, @"\.pfx")) {
                MessageBox.Show("Proper cert missing from settings!");
            }
		}

        void DownloadFile(string url, string filename) {
            try {
                //Setup webrequest
                DateTime startTime = DateTime.UtcNow;
                var response = HTTP.Request("GET", url);
                ThreadSafe(() => {
                    int max = (int)(response.ContentLength / 0x1000);
                    progBar.Maximum = max < 0x1000 ? 1 : max;
                    progBar.Step = 1;
                });
                //Read response in chunks of 0x1000 bytes
                string filepath = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), filename);
                using (Stream responseStream = response.GetResponseStream()) {
                    using (Stream fileStream = File.OpenWrite(filepath)) {
                        byte[] buffer = new byte[0x1000];
                        int bytesRead = 0;
                        do {
                            bytesRead = responseStream.Read(buffer, 0, 0x1000);
                            fileStream.Write(buffer, 0, bytesRead);
                            if ((DateTime.UtcNow - startTime).TotalMinutes > 5) throw new ApplicationException("Download timed out");
                            ThreadSafe(() => { progBar.PerformStep(); });
                        } while (bytesRead > 0);
                    }
                }
                response.Close();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        string GetMetadataUrl(string tid, string ver) {
            StatusWrite("Getting meta NcaId..");
            string url = string.Format("{0}/t/a/{1}/{2}", Settings.GetCdnUrl(), tid, ver);
            string ret = "";
            try {
                var response = HTTP.Request("HEAD", url);
                ret = string.Format("{0}/c/a/{1}", Settings.GetCdnUrl(), response.Headers.Get("X-Nintendo-Content-ID"));
                response.Close();
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            return ret;
        }

        void downloadContent(string tid, string ver) {
            try {
                //Download metadata
                Directory.CreateDirectory(string.Format("{0}/{1}", Directory.GetCurrentDirectory(), tid));
                var metaurl = GetMetadataUrl(tid, ver);
                StatusWrite("Downloading meta NCA...");
                DownloadFile(metaurl, string.Format("{0}/{1}", tid, ver));
                
                //Decrypt/parse meta data and download NCAs
                string meta = string.Format("{0}/{1}/{2}", Directory.GetCurrentDirectory(), tid, ver);
                if (File.Exists(meta)) {
                    StatusWrite("Parsing meta...");
                    NCA3 nca3 = new NCA3(meta);
                    CNMT cnmt = new CNMT(new BinaryReader(new MemoryStream(nca3.pfs0.Files[0].RawData)));
                    WriteLine("Title: {0} v{1}\nType: {2}\nMKey: {3}\n", cnmt.TitleId.ToString("X8"), ver, cnmt.Type, nca3.CryptoType.ToString("D2"));
                    foreach (var nca in cnmt.contEntries) {
                        WriteLine("[{0}]\n{1}", nca.Type, nca.NcaId);
                        StatusWrite("Downloading content NCA...");
                        DownloadFile(string.Format("{0}/c/c/{1}", Settings.GetCdnUrl(), nca.NcaId), string.Format("{0}/{1}", tid, nca.NcaId));
                    }
                    WriteLine("Done!");
                } else {
                    WriteLine("Error retriving meta!");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
		}

		#region GUI
		private void dlBut_Click(object sender, EventArgs e) {
            StatusWrite("Starting...");

			//Sanitize GUI inputs
			outText.Text = "";
            string version = "";
			if (!Utils.IsValidTid(tidText.Text)) {
				WriteLine("Invalid TID!");
				return;
			}
			if (!Utils.IsValidVersion(verText.Text)) {
				WriteLine("Invalid version!");
				return;
			}

            //Check if appropriate settings are set
            if (
                INIFile.Read("keys", "kekseed") == string.Empty || 
                INIFile.Read("keys", "keyseed") == string.Empty ||
                INIFile.Read("keys", "akaeksrc") == string.Empty ||
                INIFile.Read("keys", "okaeksrc") == string.Empty ||
                INIFile.Read("keys", "skaeksrc") == string.Empty ||
                INIFile.Read("keys", "headkey") == string.Empty
            ) {
                MessageBox.Show("Please fill in all seeds!");
                return;
            }

            //if version string was in decimal format, convert
            if (Regex.Match(verText.Text, @"[0-9]\.[0-9]\.[0-9]\.[0-9]*").Success) {
                var v = verText.Text.Split('.');
                version = ((Convert.ToUInt32(v[0]) << 26) | (Convert.ToUInt32(v[1]) << 20) | (Convert.ToUInt32(v[2]) << 16) | Convert.ToUInt32(v[3])).ToString();
            } else {
                version = verText.Text;
            }

            Task.Run(() => { downloadContent(tidText.Text, version); });
		}

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            var settings = new Settings();
            settings.Show();
        }

        private void keysToolStripMenuItem_Click(object sender, EventArgs e) {
            var keys = new Keys();
            keys.Show();
        }
        #endregion

        #region MISC
        void WriteLine(string str, params object[] args) {
			string res = "";
			foreach (var s in str.Split('\n')) res += s + Environment.NewLine;
            ThreadSafe(() => { outText.Text += string.Format(res, args); });
		}

        void StatusWrite(string str, params object[] args) {
            string res = "";
            ThreadSafe(() => { statusLbl.Text += string.Format(res, args); });
        }

        private void ThreadSafe(MethodInvoker method) {
			if (InvokeRequired)
				Invoke(method);
			else
				method();
		}
        #endregion


    }
}
