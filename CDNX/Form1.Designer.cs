namespace CDNNX {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dlBut = new System.Windows.Forms.Button();
            this.tidText = new System.Windows.Forms.TextBox();
            this.verText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outText = new System.Windows.Forms.TextBox();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // dlBut
            // 
            this.dlBut.Location = new System.Drawing.Point(203, 51);
            this.dlBut.Name = "dlBut";
            this.dlBut.Size = new System.Drawing.Size(75, 23);
            this.dlBut.TabIndex = 0;
            this.dlBut.Text = "Download";
            this.dlBut.UseVisualStyleBackColor = true;
            this.dlBut.Click += new System.EventHandler(this.dlBut_Click);
            // 
            // tidText
            // 
            this.tidText.Location = new System.Drawing.Point(12, 25);
            this.tidText.MaxLength = 16;
            this.tidText.Name = "tidText";
            this.tidText.Size = new System.Drawing.Size(176, 20);
            this.tidText.TabIndex = 1;
            this.tidText.Text = "0000000000000000";
            // 
            // verText
            // 
            this.verText.Location = new System.Drawing.Point(194, 25);
            this.verText.Name = "verText";
            this.verText.Size = new System.Drawing.Size(84, 20);
            this.verText.TabIndex = 2;
            this.verText.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Version:";
            // 
            // outText
            // 
            this.outText.Location = new System.Drawing.Point(12, 80);
            this.outText.Multiline = true;
            this.outText.Name = "outText";
            this.outText.Size = new System.Drawing.Size(266, 246);
            this.outText.TabIndex = 5;
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(15, 332);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(263, 16);
            this.progBar.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 360);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.outText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dlBut);
            this.Controls.Add(this.tidText);
            this.Controls.Add(this.verText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CDNX";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button dlBut;
		private System.Windows.Forms.TextBox tidText;
		private System.Windows.Forms.TextBox verText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox outText;
		private System.Windows.Forms.ProgressBar progBar;
	}
}

