namespace Example1
{
    partial class GameWindow
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
            this.openGLControl1 = new SharpGL.OpenGLCtrl();
            this.playerX = new System.Windows.Forms.Label();
            this.playerY = new System.Windows.Forms.Label();
            this.gameMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawRenderTime = false;
            this.openGLControl1.FrameRate = 29.41176F;
            this.openGLControl1.GDIEnabled = false;
            this.openGLControl1.Location = new System.Drawing.Point(2, -1);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(1010, 566);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyUp);
            // 
            // playerX
            // 
            this.playerX.AutoSize = true;
            this.playerX.Location = new System.Drawing.Point(705, 475);
            this.playerX.Name = "playerX";
            this.playerX.Size = new System.Drawing.Size(0, 13);
            this.playerX.TabIndex = 1;
            this.playerX.Visible = false;
            // 
            // playerY
            // 
            this.playerY.AutoSize = true;
            this.playerY.Location = new System.Drawing.Point(831, 475);
            this.playerY.Name = "playerY";
            this.playerY.Size = new System.Drawing.Size(0, 13);
            this.playerY.TabIndex = 2;
            this.playerY.Visible = false;
            // 
            // gameMessage
            // 
            this.gameMessage.AutoSize = true;
            this.gameMessage.Location = new System.Drawing.Point(27, 24);
            this.gameMessage.Name = "gameMessage";
            this.gameMessage.Size = new System.Drawing.Size(0, 13);
            this.gameMessage.TabIndex = 3;
            this.gameMessage.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.gameMessage);
            this.Controls.Add(this.playerY);
            this.Controls.Add(this.playerX);
            this.Controls.Add(this.openGLControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 600);
            this.MinimumSize = new System.Drawing.Size(520, 540);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Example 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.GameWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private SharpGL.OpenGLCtrl openGLControl1;
        private System.Windows.Forms.Label playerX;
        private System.Windows.Forms.Label playerY;
        private System.Windows.Forms.Label gameMessage;
    }
}

