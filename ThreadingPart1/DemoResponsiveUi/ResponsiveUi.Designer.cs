namespace DemoResponsiveUi
{
    partial class ResponsiveUi
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
            this.BtnWithoutThread = new System.Windows.Forms.Button();
            this.BtnThreaded = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnWithoutThread
            // 
            this.BtnWithoutThread.Location = new System.Drawing.Point(13, 12);
            this.BtnWithoutThread.Name = "BtnWithoutThread";
            this.BtnWithoutThread.Size = new System.Drawing.Size(775, 97);
            this.BtnWithoutThread.TabIndex = 0;
            this.BtnWithoutThread.Text = "Without Thread";
            this.BtnWithoutThread.UseVisualStyleBackColor = true;
            this.BtnWithoutThread.Click += new System.EventHandler(this.BtnWithoutThread_Click);
            // 
            // BtnThreaded
            // 
            this.BtnThreaded.Location = new System.Drawing.Point(13, 115);
            this.BtnThreaded.Name = "BtnThreaded";
            this.BtnThreaded.Size = new System.Drawing.Size(775, 97);
            this.BtnThreaded.TabIndex = 1;
            this.BtnThreaded.Text = "Threaded";
            this.BtnThreaded.UseVisualStyleBackColor = true;
            this.BtnThreaded.Click += new System.EventHandler(this.BtnThreaded_Click);
            // 
            // ResponsiveUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 229);
            this.Controls.Add(this.BtnThreaded);
            this.Controls.Add(this.BtnWithoutThread);
            this.Name = "ResponsiveUi";
            this.Text = "Responsive UI";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnWithoutThread;
        private System.Windows.Forms.Button BtnThreaded;
    }
}

