namespace Example1
{
    partial class FormExample1
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
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawRenderTime = false;
            this.openGLControl1.FrameRate = 29.41176F;
            this.openGLControl1.GDIEnabled = false;
            this.openGLControl1.Location = new System.Drawing.Point(2, -1);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(500, 500);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Load += new System.EventHandler(this.openGLControl1_Load);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyUp);
            // 
            // FormExample1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 501);
            this.Controls.Add(this.openGLControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 540);
            this.MinimumSize = new System.Drawing.Size(520, 540);
            this.Name = "FormExample1";
            this.Text = "Example 1";
            this.ResumeLayout(false);

        }

        #endregion

		private SharpGL.OpenGLCtrl openGLControl1;

    }
}

