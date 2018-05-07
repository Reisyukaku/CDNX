using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDNNX {
	public partial class Form1 : Form {

		public Form1() {
			InitializeComponent();
			if (!File.Exists(Directory.GetCurrentDirectory() + @"\nx_tls_client_cert.pfx") || !File.Exists(Directory.GetCurrentDirectory() + @"\NXCrypt.dll")) {
				MessageBox.Show("Missing dependency in root dir.");
				Environment.Exit(0);
			}
		}

		void DownloadFile(string url, string filename) {
			try {
				//Setup webrequest
				DateTime startTime = DateTime.UtcNow;
				X509Certificate2 cert = new X509Certificate2(Directory.GetCurrentDirectory() + @"\nx_tls_client_cert.pfx", "switch");
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.ClientCertificates.Add(cert);
				ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
				WebResponse response = request.GetResponse();
				ThreadSafe(() => {
					progBar.Maximum = (int)(response.ContentLength / 0x1000);
					progBar.Step = 1;
				});
				

				//Read response in chunks of 0x1000 bytes
				using (Stream responseStream = response.GetResponseStream()) {
					using (Stream fileStream = File.OpenWrite(Directory.GetCurrentDirectory() + @"\" + filename)) {
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
				while (!File.Exists(Directory.GetCurrentDirectory() + @"\" + filename)) ;
			} catch (Exception e) {
				Console.WriteLine(e);
			}
		}

		void downloadContent(string tid, string ver) {
			//Download metadata
			string url = Properties.Resources.CDNUrl + "/t/a/" + tid + "/" + ver;
			DownloadFile(url, ver);

			//Decrypt/parse meta data and download NCAs
			string meta = Directory.GetCurrentDirectory() + @"\" + ver;
			if (File.Exists(meta)) {
				NCA3 nca3 = new NCA3(meta);
				CNMT cnmt = new CNMT(new BinaryReader(new MemoryStream(nca3.pfs0.Files[0].RawData)));
				WriteLine("Title: {0} v{1}\nType: {2}\n", cnmt.TitleId.ToString("X8"), ver, cnmt.Type);
				Task.Run(() => {
					foreach (var nca in cnmt.contEntries) {
						ThreadSafe(() => { WriteLine("[{0}]\n{1}", nca.Type, nca.NcaId); });
						DownloadFile(Properties.Resources.CDNUrl + "/c/c/" + nca.NcaId, nca.NcaId);
					}
					ThreadSafe(() => { WriteLine("Done!"); });
				});
			} else {
				WriteLine("Error retriving meta!");
			}
		}

		#region GUI
		private void dlBut_Click(object sender, EventArgs e) {
			//Sanitize GUI inputs
			outText.Text = "";
			if (!Utils.IsValidTid(tidText.Text)) {
				WriteLine("Invalid TID!");
				return;
			}
			if (!Utils.IsValidVersion(verText.Text)) {
				WriteLine("Invalid version!");
				return;
			}

			downloadContent(tidText.Text, verText.Text);
		}
		#endregion

		#region MISC
		void WriteLine(string str, params object[] args) {
			string res = "";
			foreach (var s in str.Split('\n')) res += s + Environment.NewLine;
			outText.Text += string.Format(res, args);
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
