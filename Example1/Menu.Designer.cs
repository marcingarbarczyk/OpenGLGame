namespace Example1
{
    partial class Menu
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
            this.mainMenuBg = new System.Windows.Forms.PictureBox();
            this.imgbtnNewGame = new System.Windows.Forms.PictureBox();
            this.imgbtnAuthors = new System.Windows.Forms.PictureBox();
            this.imgbtnExit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuBg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnNewGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnAuthors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnExit)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuBg
            // 
            this.mainMenuBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuBg.Location = new System.Drawing.Point(0, 0);
            this.mainMenuBg.Name = "mainMenuBg";
            this.mainMenuBg.Size = new System.Drawing.Size(1403, 698);
            this.mainMenuBg.TabIndex = 4;
            this.mainMenuBg.TabStop = false;
            this.mainMenuBg.Click += new System.EventHandler(this.mainMenuBg_Click);
            // 
            // imgbtnNewGame
            // 
            this.imgbtnNewGame.Location = new System.Drawing.Point(503, 168);
            this.imgbtnNewGame.Name = "imgbtnNewGame";
            this.imgbtnNewGame.Size = new System.Drawing.Size(451, 130);
            this.imgbtnNewGame.TabIndex = 5;
            this.imgbtnNewGame.TabStop = false;
            this.imgbtnNewGame.Click += new System.EventHandler(this.imgbtnNewGame_Click);
            this.imgbtnNewGame.MouseLeave += new System.EventHandler(this.imgbtnNewGame_MouseLeave);
            this.imgbtnNewGame.MouseHover += new System.EventHandler(this.imgbtnNewGame_MouseHover);
            // 
            // imgbtnAuthors
            // 
            this.imgbtnAuthors.Location = new System.Drawing.Point(503, 314);
            this.imgbtnAuthors.Name = "imgbtnAuthors";
            this.imgbtnAuthors.Size = new System.Drawing.Size(451, 130);
            this.imgbtnAuthors.TabIndex = 6;
            this.imgbtnAuthors.TabStop = false;
            this.imgbtnAuthors.Click += new System.EventHandler(this.imgbtnAuthors_Click);
            this.imgbtnAuthors.MouseLeave += new System.EventHandler(this.imgbtnAuthors_MouseLeave);
            this.imgbtnAuthors.MouseHover += new System.EventHandler(this.imgbtnAuthors_MouseHover);
            // 
            // imgbtnExit
            // 
            this.imgbtnExit.Location = new System.Drawing.Point(505, 462);
            this.imgbtnExit.Name = "imgbtnExit";
            this.imgbtnExit.Size = new System.Drawing.Size(451, 130);
            this.imgbtnExit.TabIndex = 7;
            this.imgbtnExit.TabStop = false;
            this.imgbtnExit.Click += new System.EventHandler(this.imgbtnExit_Click);
            this.imgbtnExit.MouseLeave += new System.EventHandler(this.imgbtnExit_MouseLeave);
            this.imgbtnExit.MouseHover += new System.EventHandler(this.imgbtnExit_MouseHover);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 698);
            this.Controls.Add(this.imgbtnExit);
            this.Controls.Add(this.imgbtnAuthors);
            this.Controls.Add(this.imgbtnNewGame);
            this.Controls.Add(this.mainMenuBg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuBg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnNewGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnAuthors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtnExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox mainMenuBg;
        private System.Windows.Forms.PictureBox imgbtnNewGame;
        private System.Windows.Forms.PictureBox imgbtnAuthors;
        private System.Windows.Forms.PictureBox imgbtnExit;
    }
}