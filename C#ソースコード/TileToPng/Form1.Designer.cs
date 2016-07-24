namespace Grayscale.TileToPng
{
    partial class Form1
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
            this.ucMain1 = new Grayscale.TileToPng.UcMain();
            this.ucCommandline1 = new Grayscale.TileToPng.UcCommandline();
            this.SuspendLayout();
            // 
            // ucMain1
            // 
            this.ucMain1.AllowDrop = true;
            this.ucMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMain1.Location = new System.Drawing.Point(0, 0);
            this.ucMain1.Name = "ucMain1";
            this.ucMain1.Size = new System.Drawing.Size(550, 373);
            this.ucMain1.TabIndex = 0;
            // 
            // ucCommandline1
            // 
            this.ucCommandline1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucCommandline1.Location = new System.Drawing.Point(0, 352);
            this.ucCommandline1.Name = "ucCommandline1";
            this.ucCommandline1.Size = new System.Drawing.Size(550, 21);
            this.ucCommandline1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 373);
            this.Controls.Add(this.ucCommandline1);
            this.Controls.Add(this.ucMain1);
            this.Name = "Form1";
            this.Text = "TileToPng";
            this.ResumeLayout(false);

        }

        #endregion

        private UcMain ucMain1;
        private UcCommandline ucCommandline1;
    }
}

