using System;
using System.IO;
using System.Windows.Forms;

namespace CDNNX {

    public partial class Settings : Form {

        public Settings() {
            InitializeComponent();

            didText.Text = INIFile.Read("settings", "did");
            firmText.Text = INIFile.Read("settings", "firmver");
            eidText.Text = INIFile.Read("settings", "eid");
            certText.Text = INIFile.Read("settings", "cert");
        }

        public static void Create() {
            INIFile.Write("settings", "did", "0000000000000000");
            INIFile.Write("settings", "firmver", "0.0.0-0");
            INIFile.Write("settings", "eid", "lp1");
            INIFile.Write("settings", "cert", Directory.GetCurrentDirectory() + "/nx_tls_client_cert.pfx");
        }

        private void cancelBut_Click(object sender, EventArgs e) {
            Close();
        }

        private void saveBut_Click(object sender, EventArgs e) {
            INIFile.Write("settings", "did", didText.Text);
            INIFile.Write("settings", "firmver", firmText.Text);
            INIFile.Write("settings", "eid", eidText.Text);
            INIFile.Write("settings", "cert", certText.Text);

            Close();
        }

        private void browseCertBut_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                certText.Text = ofd.FileName;
        }

        public static string GetCdnUrl() {
            return string.Format(Properties.Resources.CDNUrl, INIFile.Read("settings", "eid"));
        }
    }
}
