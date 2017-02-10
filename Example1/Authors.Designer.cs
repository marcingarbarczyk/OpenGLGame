namespace Example1
{
    partial class Authors
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
            this.authorsBg = new System.Windows.Forms.PictureBox();
            this.btnimgBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.authorsBg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnimgBack)).BeginInit();
            this.SuspendLayout();
            // 
            // authorsBg
            // 
            this.authorsBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authorsBg.Location = new System.Drawing.Point(0, 0);
            this.authorsBg.Name = "authorsBg";
            this.authorsBg.Size = new System.Drawing.Size(1403, 698);
            this.authorsBg.TabIndex = 0;
            this.authorsBg.TabStop = false;
            // 
            // btnimgBack
            // 
            this.btnimgBack.Location = new System.Drawing.Point(858, 501);
            this.btnimgBack.Name = "btnimgBack";
            this.btnimgBack.Size = new System.Drawing.Size(451, 130);
            this.btnimgBack.TabIndex = 1;
            this.btnimgBack.TabStop = false;
            this.btnimgBack.Click += new System.EventHandler(this.btnimgBack_Click);
            this.btnimgBack.MouseLeave += new System.EventHandler(this.btnimgBack_MouseLeave);
            this.btnimgBack.MouseHover += new System.EventHandler(this.btnimgBack_MouseHover);
            // 
            // Authors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 698);
            this.Controls.Add(this.btnimgBack);
            this.Controls.Add(this.authorsBg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Authors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorzy gry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Authors_FormClosing);
            this.Load += new System.EventHandler(this.Authors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.authorsBg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnimgBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox authorsBg;
        private System.Windows.Forms.PictureBox btnimgBack;
    }
}