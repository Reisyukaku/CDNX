using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CDNX.Mono
{
    public partial class Keys : Form
    {
        private const uint MAXKEYS = 5;
        private readonly List<TextBox> mkTxts;

        public Keys()
        {
            this.InitializeComponent();
            this.mkTxts = new List<TextBox>();

            for (int i = 0; i < MAXKEYS; i++)
            {
                TextBox tb = new TextBox
                {
                    Location = new System.Drawing.Point(56, 32 + (i * 26)),
                    Size = new System.Drawing.Size(225, 20),
                    Text = INIFile.Read("keys", "MK" + i.ToString("D2"))
                };

                Label lbl = new Label
                {
                    Location = new System.Drawing.Point(6, 35 + (i * 26)),
                    Size = new System.Drawing.Size(44, 13),
                    Text = $"MK_{i:D2}:"
                };

                this.groupBox1.Controls.Add(tb);
                this.groupBox1.Controls.Add(lbl);
                this.mkTxts.Add(tb);
            }

            this.keySrcTxt.Text = INIFile.Read("keys", "keyseed");
            this.kekSrcTxt.Text = INIFile.Read("keys", "kekseed");
            this.akaekTxt.Text = INIFile.Read("keys", "akaeksrc");
            this.okaekTxt.Text = INIFile.Read("keys", "okaeksrc");
            this.skaekTxt.Text = INIFile.Read("keys", "skaeksrc");
            this.headKeyText.Text = INIFile.Read("keys", "headkey");
        }

        public string GetMkByInd(int keynum)
        {
            return this.mkTxts[keynum].Text;
        }

        public static void Create()
        {
            for (int i = 0; i < MAXKEYS; i++)
                INIFile.Write("keys", "MK" + i.ToString("D2"), string.Empty);
            INIFile.Write("keys", "keyseed", string.Empty);
            INIFile.Write("keys", "kekseed", string.Empty);
            INIFile.Write("keys", "akaeksrc", string.Empty);
            INIFile.Write("keys", "okaeksrc", string.Empty);
            INIFile.Write("keys", "skaeksrc", string.Empty);
            INIFile.Write("keys", "headkey", string.Empty);
        }

        private void cancelKeysBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveKeysBut_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (TextBox txt in this.mkTxts)
            {
                INIFile.Write("keys", "MK" + i.ToString("D2"), txt.Text);
                i++;
            }

            INIFile.Write("keys", "keyseed", this.keySrcTxt.Text);
            INIFile.Write("keys", "kekseed", this.kekSrcTxt.Text);
            INIFile.Write("keys", "akaeksrc", this.akaekTxt.Text);
            INIFile.Write("keys", "okaeksrc", this.okaekTxt.Text);
            INIFile.Write("keys", "skaeksrc", this.skaekTxt.Text);
            INIFile.Write("keys", "headkey", this.headKeyText.Text);
            this.Close();
        }
    }
}
