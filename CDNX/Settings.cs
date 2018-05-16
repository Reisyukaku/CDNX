using System;
using System.Windows.Forms;

namespace CDNNX {

    public partial class Settings : Form {

        public Settings() {
            InitializeComponent();
        }

        public void LoadConfig() {
            didText.Text = INIFile.ReadSetting("did");
            firmText.Text = INIFile.ReadSetting("firmver");
            eidText.Text = INIFile.ReadSetting("eid");
            certText.Text = INIFile.ReadSetting("cert");
        }

        public void CreateConfig() {
            INIFile.WriteSetting("did", "0000000000000000");
            INIFile.WriteSetting("firmver", "0.0.0-0");
            INIFile.WriteSetting("eid", "lp1");
            INIFile.WriteSetting("cert", "");
        }

        private void cancelBut_Click(object sender, EventArgs e) {
            Close();
            Dispose();
        }

        private void saveBut_Click(object sender, EventArgs e) {
            INIFile.WriteSetting("did", didText.Text);
            INIFile.WriteSetting("firmver", firmText.Text);
            INIFile.WriteSetting("eid", eidText.Text);
            INIFile.WriteSetting("cert", certText.Text);

            Close();
            Dispose();
        }
    }
}
