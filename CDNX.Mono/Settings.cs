using System;
using System.IO;
using System.Windows.Forms;

namespace CDNX.Mono
{
    public partial class Settings : Form
    {
        public Settings()
        {
            this.InitializeComponent();

            this.didText.Text = INIFile.Read("settings", "did");
            this.firmText.Text = INIFile.Read("settings", "firmver");
            this.eidText.Text = INIFile.Read("settings", "eid");
            this.certText.Text = INIFile.Read("settings", "cert");
        }

        public static void Create()
        {
            INIFile.Write("settings", "did", "0000000000000000");
            INIFile.Write("settings", "firmver", "0.0.0-0");
            INIFile.Write("settings", "eid", "lp1");
            INIFile.Write("settings", "cert", Path.Combine(Application.StartupPath, "nx_tls_client_cert.pfx"));
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBut_Click(object sender, EventArgs e)
        {
            INIFile.Write("settings", "did", this.didText.Text);
            INIFile.Write("settings", "firmver", this.firmText.Text);
            INIFile.Write("settings", "eid", this.eidText.Text);
            INIFile.Write("settings", "cert", this.certText.Text);

            this.Close();
        }

        private void BrowseCertBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                this.certText.Text = ofd.FileName;
        }

        public static string GetCdnUrl()
        {
            return string.Format(Properties.Resources.CDNUrl, INIFile.Read("settings", "eid"));
        }
    }
}
