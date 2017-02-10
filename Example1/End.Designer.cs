namespace Example1
{
    partial class End
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
            this.endgameBg = new System.Windows.Forms.PictureBox();
            this.lblPoints = new System.Windows.Forms.Label();
            this.imgbtnStart = new System.Windows.Forms.PictureBox();
            this.imgbtnEnd = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.endgameBg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // endgameBg
            // 
            this.endgameBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endgameBg.Location = new System.Drawing.Point(0, 0);
            this.endgameBg.Name = "endgameBg";
            this.endgameBg.Size = new System.Drawing.Size(1403, 698);
            this.endgameBg.TabIndex = 0;
            this.endgameBg.TabStop = false;
            // 
            // lblPoints
            // 
            this.lblPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPoints.Location = new System.Drawing.Point(594, 350);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(290, 72);
            this.lblPoints.TabIndex = 1;
            this.lblPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgbtnStart
            // 
            this.imgbtnStart.Location = new System.Drawing.Point(44, 526);
            this.imgbtnStart.Name = "imgbtnStart";
            this.imgbtnStart.Size = new System.Drawing.Size(451, 130);
            this.imgbtnStart.TabIndex = 2;
            this.imgbtnStart.TabStop = false;
            this.imgbtnStart.Click += new System.EventHandler(this.imgbtnStart_Click);
            this.imgbtnStart.MouseLeave += new System.EventHandler(this.imgbtnStart_MouseLeave);
            this.imgbtnStart.MouseHover += new System.EventHandler(this.imgbtnStart_MouseHover);
            // 
            // imgbtnEnd
            // 
            this.imgbtnEnd.Location = new System.Drawing.Point(889, 526);
            this.imgbtnEnd.Name = "imgbtnEnd";
            this.imgbtnEnd.Size = new System.Drawing.Size(451, 130);
            this.imgbtnEnd.TabIndex = 3;
            this.imgbtnEnd.TabStop = false;
            this.imgbtnEnd.Click += new System.EventHandler(this.imgbtnEnd_Click);
            this.imgbtnEnd.MouseLeave += new System.EventHandler(this.imgbtnEnd_MouseLeave);
            this.imgbtnEnd.MouseHover += new System.EventHandler(this.imgbtnEnd_MouseHover);
            // 
            // End
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 698);
            this.Controls.Add(this.imgbtnEnd);
            this.Controls.Add(this.imgbtnStart);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.endgameBg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "End";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Koniec gry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.End_FormClosing);
            this.Load += new System.EventHandler(this.End_Load);
            ((System.ComponentModel.ISupportInitialize)(this.endgameBg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox endgameBg;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.PictureBox imgbtnStart;
        private System.Windows.Forms.PictureBox imgbtnEnd;
    }
}