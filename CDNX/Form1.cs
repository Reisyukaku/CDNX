using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDNNX {

	public partial class Form1 : Form {

        public Form1() {
            //Initialize stuff
			InitializeComponent();

            //Check for pre-req. files
            if (!File.Exists(Path.Combine(Application.StartupPath, "config.ini"))) {
                Settings.Create();
                Keys.Create();
            }

            if (!File.Exists(Path.Combine(Application.StartupPath, "NXCrypt.dll"))) {
				MessageBox.Show("Missing NXCrypt.dll dependency in root dir.");
				Environment.Exit(0);
			}

            string cert = INIFile.Read("settings", "cert");
            if (!File.Exists(cert) || !Regex.IsMatch(cert, @"\.pfx")) {
                MessageBox.Show("Proper cert missing from settings!");
            }
		}

        string GetMetadataUrl(string tid, string ver) {
            StatusWrite("Getting meta NcaId..");
            string url = $"{Settings.GetCdnUrl()}/t/a/{tid}/{ver}";
            string ret = "";
            try {
                WebResponse response = HTTP.Request("HEAD", url);
                ret = $"{Settings.GetCdnUrl()}/c/a/{response.Headers.Get("X-Nintendo-Content-ID")}";
                response.Close();
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            return ret;
        }

        void DownloadFile(string url, string filename) {
            try {
                //Setup webrequest
                DateTime startTime = DateTime.UtcNow;
                WebResponse response = HTTP.Request("GET", url);
                int max = (int)(response.ContentLength / 0x1000);
                ThreadSafe(() => {
                    progBar.Maximum = max < 0x1000 ? 1 : max;
                    progBar.Step = 1;
                
                    //Read response in chunks of 0x1000 bytes
                    string filepath = Path.Combine(Application.StartupPath, filename);
                    using (Stream responseStream = response.GetResponseStream()) {
                        using (Stream fileStream = File.OpenWrite(filepath)) {
                            byte[] buffer = new byte[0x1000];
                            int bytesRead;
                            do {
                                bytesRead = responseStream.Read(buffer, 0, 0x1000);
                                Console.WriteLine("Bytesread: " + bytesRead.ToString("X8"));
                                fileStream.Write(buffer, 0, bytesRead);
                                if ((DateTime.UtcNow - startTime).TotalMinutes > 5) throw new ApplicationException("Download timed out");
                                progBar.PerformStep();
                            } while (bytesRead > 0);
                        }
                    }
                });
                response.Close();
                } catch (Exception e) {
Console.WriteLine(e);
            }
        }

        void downloadContent(string tid, string ver) {
            try {
                //Download metadata
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, tid));
                string metaurl = GetMetadataUrl(tid, ver);
                StatusWrite("Downloading meta NCA...");
                DownloadFile(metaurl, $"{tid}/{ver}");
                
                //Decrypt/parse meta data and download NCAs
                string meta = Path.Combine(Application.StartupPath, tid, ver);
                if (File.Exists(meta)) {
                    StatusWrite("Parsing meta...");
                    NCA3 nca3 = new NCA3(meta);
                    CNMT cnmt = new CNMT(new BinaryReader(new MemoryStream(nca3.pfs0.Files[0].RawData)));
                    WriteLine("Title: {0} v{1}\nType: {2}\nMKey: {3}\n", cnmt.TitleId.ToString("X8"), ver, cnmt.Type, nca3.CryptoType.ToString("D2"));
                    foreach (ContEntry nca in cnmt.contEntries) {
                        WriteLine("[{0}]\n{1}", nca.Type, nca.NcaId);
                        StatusWrite("Downloading content NCA...");
                        DownloadFile($"{Settings.GetCdnUrl()}/c/c/{nca.NcaId}", $"{tid}/{nca.NcaId}");
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
                string[] v = verText.Text.Split('.');
                version = ((Convert.ToUInt32(v[0]) << 26) | (Convert.ToUInt32(v[1]) << 20) | (Convert.ToUInt32(v[2]) << 16) | Convert.ToUInt32(v[3])).ToString();
            } else {
                version = verText.Text;
            }

            Task.Run(() => { downloadContent(tidText.Text, version); });
		}

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            Settings settings = new Settings();
            settings.Show();
        }

        private void keysToolStripMenuItem_Click(object sender, EventArgs e) {
            Keys keys = new Keys();
            keys.Show();
        }
        #endregion

        #region MISC
        void WriteLine(string str, params object[] args) {
			string res = str.Split('\n').Aggregate("", (current, s) => current + (s + Environment.NewLine));
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
