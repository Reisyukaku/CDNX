namespace CDNX.Mono
{
    partial class Keys
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Keys));
            this.cancelKeysBut = new System.Windows.Forms.Button();
            this.saveKeysBut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.keySrcTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kekSrcTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.skaekTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okaekTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.akaekTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.headKeyText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelKeysBut
            // 
            this.cancelKeysBut.Location = new System.Drawing.Point(456, 269);
            this.cancelKeysBut.Name = "cancelKeysBut";
            this.cancelKeysBut.Size = new System.Drawing.Size(90, 26);
            this.cancelKeysBut.TabIndex = 3;
            this.cancelKeysBut.Text = "Cancel";
            this.cancelKeysBut.UseVisualStyleBackColor = true;
            this.cancelKeysBut.Click += new System.EventHandler(this.cancelKeysBut_Click);
            // 
            // saveKeysBut
            // 
            this.saveKeysBut.Location = new System.Drawing.Point(552, 269);
            this.saveKeysBut.Name = "saveKeysBut";
            this.saveKeysBut.Size = new System.Drawing.Size(90, 26);
            this.saveKeysBut.TabIndex = 2;
            this.saveKeysBut.Text = "Save";
            this.saveKeysBut.UseVisualStyleBackColor = true;
            this.saveKeysBut.Click += new System.EventHandler(this.saveKeysBut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 251);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Masterkeys";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.keySrcTxt);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.kekSrcTxt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.skaekTxt);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.okaekTxt);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.akaekTxt);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(308, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 160);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seeds";
            // 
            // keySrcTxt
            // 
            this.keySrcTxt.Location = new System.Drawing.Point(97, 123);
            this.keySrcTxt.Name = "keySrcTxt";
            this.keySrcTxt.Size = new System.Drawing.Size(231, 20);
            this.keySrcTxt.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Key gen source:";
            // 
            // kekSrcTxt
            // 
            this.kekSrcTxt.Location = new System.Drawing.Point(97, 97);
            this.kekSrcTxt.Name = "kekSrcTxt";
            this.kekSrcTxt.Size = new System.Drawing.Size(231, 20);
            this.kekSrcTxt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Kek gen source:";
            // 
            // skaekTxt
            // 
            this.skaekTxt.Location = new System.Drawing.Point(116, 71);
            this.skaekTxt.Name = "skaekTxt";
            this.skaekTxt.Size = new System.Drawing.Size(212, 20);
            this.skaekTxt.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "System kaek source:";
            // 
            // okaekTxt
            // 
            this.okaekTxt.Location = new System.Drawing.Point(116, 45);
            this.okaekTxt.Name = "okaekTxt";
            this.okaekTxt.Size = new System.Drawing.Size(212, 20);
            this.okaekTxt.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ocean kaek source:";
            // 
            // akaekTxt
            // 
            this.akaekTxt.Location = new System.Drawing.Point(116, 19);
            this.akaekTxt.Name = "akaekTxt";
            this.akaekTxt.Size = new System.Drawing.Size(212, 20);
            this.akaekTxt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "App kaek source:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.headKeyText);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(308, 178);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 85);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other";
            // 
            // headKeyText
            // 
            this.headKeyText.Location = new System.Drawing.Point(77, 19);
            this.headKeyText.Name = "headKeyText";
            this.headKeyText.Size = new System.Drawing.Size(251, 20);
            this.headKeyText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Header key:";
            // 
            // Keys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 303);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelKeysBut);
            this.Controls.Add(this.saveKeysBut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Keys";
            this.Text = "Keys";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelKeysBut;
        private System.Windows.Forms.Button saveKeysBut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox skaekTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox okaekTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox akaekTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox headKeyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keySrcTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox kekSrcTxt;
        private System.Windows.Forms.Label label5;
    }
}