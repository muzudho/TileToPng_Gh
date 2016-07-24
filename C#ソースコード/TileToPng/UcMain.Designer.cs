namespace Grayscale.TileToPng
{
    partial class UcMain
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UcMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "UcMain";
            this.Size = new System.Drawing.Size(621, 493);
            this.Load += new System.EventHandler(this.UcMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UcMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.UcMain_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UcMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UcMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UcMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UcMain_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UcMain_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
