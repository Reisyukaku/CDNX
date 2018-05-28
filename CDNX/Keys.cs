using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CDNNX {
    public partial class Keys : Form {

        private const uint MAXKEYS = 5;
        private List<TextBox> mkTxts;

        public Keys() {
            InitializeComponent();
            mkTxts = new List<TextBox>();

            for (int i = 0; i < MAXKEYS; i++) {
                TextBox tb = new TextBox();
                tb.Location = new System.Drawing.Point(56, 32 + (i * 26));
                tb.Size = new System.Drawing.Size(225, 20);
                tb.Text = INIFile.Read("keys", "MK"+i.ToString("D2"));

                Label lbl = new Label();
                lbl.Location = new System.Drawing.Point(6, 35 + (i * 26));
                lbl.Size = new System.Drawing.Size(44, 13);
                lbl.Text = $"MK_{i:D2}:";

                groupBox1.Controls.Add(tb);
                groupBox1.Controls.Add(lbl);
                mkTxts.Add(tb);
            }

            keySrcTxt.Text = INIFile.Read("keys", "keyseed");
            kekSrcTxt.Text = INIFile.Read("keys", "kekseed");
            akaekTxt.Text = INIFile.Read("keys", "akaeksrc");
            okaekTxt.Text = INIFile.Read("keys", "okaeksrc");
            skaekTxt.Text = INIFile.Read("keys", "skaeksrc");
            headKeyText.Text = INIFile.Read("keys", "headkey");
        }

        public string GetMkByInd(int keynum) {
            return mkTxts[keynum].Text;
        }

        public static void Create() {
            for (int i = 0; i < MAXKEYS; i++)
                INIFile.Write("keys", "MK" + i.ToString("D2"), string.Empty);
            INIFile.Write("keys", "keyseed", string.Empty);
            INIFile.Write("keys", "kekseed", string.Empty);
            INIFile.Write("keys", "akaeksrc", string.Empty);
            INIFile.Write("keys", "okaeksrc", string.Empty);
            INIFile.Write("keys", "skaeksrc", string.Empty);
            INIFile.Write("keys", "headkey", string.Empty);
        }

        private void cancelKeysBut_Click(object sender, EventArgs e) {
            Close();
        }

        private void saveKeysBut_Click(object sender, EventArgs e) {
            int i = 0;
            foreach (TextBox txt in mkTxts) {
                INIFile.Write("keys", "MK" + i.ToString("D2"), txt.Text);
                i++;
            }
            INIFile.Write("keys", "keyseed", keySrcTxt.Text);
            INIFile.Write("keys", "kekseed", kekSrcTxt.Text);
            INIFile.Write("keys", "akaeksrc", akaekTxt.Text);
            INIFile.Write("keys", "okaeksrc", okaekTxt.Text);
            INIFile.Write("keys", "skaeksrc", skaekTxt.Text);
            INIFile.Write("keys", "headkey", headKeyText.Text);
            Close();
        }
    }
}
