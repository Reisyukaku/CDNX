namespace CDNNX
{
    partial class Settings
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.saveSettingsBut = new System.Windows.Forms.Button();
            this.cancelSettingsBut = new System.Windows.Forms.Button();
            this.didText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.certText = new System.Windows.Forms.TextBox();
            this.browseCertBut = new System.Windows.Forms.Button();
            this.eidText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.firmText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveSettingsBut
            // 
            this.saveSettingsBut.Location = new System.Drawing.Point(226, 116);
            this.saveSettingsBut.Name = "saveSettingsBut";
            this.saveSettingsBut.Size = new System.Drawing.Size(90, 26);
            this.saveSettingsBut.TabIndex = 0;
            this.saveSettingsBut.Text = "Save";
            this.saveSettingsBut.UseVisualStyleBackColor = true;
            this.saveSettingsBut.Click += new System.EventHandler(this.saveBut_Click);
            // 
            // cancelSettingsBut
            // 
            this.cancelSettingsBut.Location = new System.Drawing.Point(130, 116);
            this.cancelSettingsBut.Name = "cancelSettingsBut";
            this.cancelSettingsBut.Size = new System.Drawing.Size(90, 26);
            this.cancelSettingsBut.TabIndex = 1;
            this.cancelSettingsBut.Text = "Cancel";
            this.cancelSettingsBut.UseVisualStyleBackColor = true;
            this.cancelSettingsBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // didText
            // 
            this.didText.Location = new System.Drawing.Point(74, 12);
            this.didText.Name = "didText";
            this.didText.Size = new System.Drawing.Size(242, 20);
            this.didText.TabIndex = 2;
            this.didText.Text = "0000000000000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "DeviceId: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Console cert: ";
            // 
            // certText
            // 
            this.certText.Location = new System.Drawing.Point(91, 38);
            this.certText.Name = "certText";
            this.certText.Size = new System.Drawing.Size(149, 20);
            this.certText.TabIndex = 5;
            // 
            // browseCertBut
            // 
            this.browseCertBut.Location = new System.Drawing.Point(246, 38);
            this.browseCertBut.Name = "browseCertBut";
            this.browseCertBut.Size = new System.Drawing.Size(71, 20);
            this.browseCertBut.TabIndex = 6;
            this.browseCertBut.Text = "Browse";
            this.browseCertBut.UseVisualStyleBackColor = true;
            this.browseCertBut.Click += new System.EventHandler(this.browseCertBut_Click);
            // 
            // eidText
            // 
            this.eidText.Location = new System.Drawing.Point(91, 64);
            this.eidText.Name = "eidText";
            this.eidText.Size = new System.Drawing.Size(149, 20);
            this.eidText.TabIndex = 8;
            this.eidText.Text = "lp1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Environment: ";
            // 
            // firmText
            // 
            this.firmText.Location = new System.Drawing.Point(91, 90);
            this.firmText.Name = "firmText";
            this.firmText.Size = new System.Drawing.Size(149, 20);
            this.firmText.TabIndex = 10;
            this.firmText.Text = "0.0.0-0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Firmware Ver:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 151);
            this.Controls.Add(this.firmText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eidText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browseCertBut);
            this.Controls.Add(this.certText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.didText);
            this.Controls.Add(this.cancelSettingsBut);
            this.Controls.Add(this.saveSettingsBut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveSettingsBut;
        private System.Windows.Forms.Button cancelSettingsBut;
        private System.Windows.Forms.TextBox didText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox certText;
        private System.Windows.Forms.Button browseCertBut;
        private System.Windows.Forms.TextBox eidText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firmText;
        private System.Windows.Forms.Label label4;
    }
}