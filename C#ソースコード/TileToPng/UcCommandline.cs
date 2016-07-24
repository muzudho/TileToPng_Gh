using Grayscale.TileToPng.n___100_cmdline_;
using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    public partial class UcCommandline : UserControl
    {
        public UcCommandline()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((Form1)this.ParentForm).UpdateCommandline(((TextBox)sender).Text);
            }
        }
    }
}
