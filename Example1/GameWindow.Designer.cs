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
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawRenderTime = false;
            this.openGLControl1.FrameRate = 29.41176F;
            this.openGLControl1.GDIEnabled = false;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(1340, 679);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.Load += new System.EventHandler(this.GameWindow_Load);
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Load += new System.EventHandler(this.GameWindow_Load);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyUp);
            
            // 
            // playerX
            // 
            this.playerX.AutoSize = true;
            this.playerX.Location = new System.Drawing.Point(940, 585);
            this.playerX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerX.Name = "playerX";
            this.playerX.Size = new System.Drawing.Size(0, 17);
            this.playerX.TabIndex = 1;
            this.playerX.Visible = false;
            // 
            // playerY
            // 
            this.playerY.AutoSize = true;
            this.playerY.Location = new System.Drawing.Point(1108, 585);
            this.playerY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerY.Name = "playerY";
            this.playerY.Size = new System.Drawing.Size(0, 17);
            this.playerY.TabIndex = 2;
            this.playerY.Visible = false;
            // 
            // gameMessage
            // 
            this.gameMessage.AutoSize = true;
            this.gameMessage.Location = new System.Drawing.Point(36, 30);
            this.gameMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameMessage.Name = "gameMessage";
            this.gameMessage.Size = new System.Drawing.Size(0, 17);
            this.gameMessage.TabIndex = 3;
            this.gameMessage.Visible = false;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPoints.ForeColor = System.Drawing.Color.Coral;
            this.lblPoints.Location = new System.Drawing.Point(1143, 30);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(0, 29);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Visible = false;
            // 
            // lblLevel
            // 
            this.lblLevel.BackColor = System.Drawing.Color.Black;
            this.lblLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(0, 0);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(1340, 679);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLevel.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1340, 679);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.gameMessage);
            this.Controls.Add(this.playerY);
            this.Controls.Add(this.playerX);
            this.Controls.Add(this.openGLControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1358, 726);
            this.MinimumSize = new System.Drawing.Size(686, 653);
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
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblLevel;
    }
}

