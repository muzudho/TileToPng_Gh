using Grayscale.TileToPng.n___100_cmdline_;
using Grayscale.TileToPng.n100____cmdline_;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateCommandline(string line)
        {
            Commandline commandline = UtilCommandlineParser.Parse(line);

            this.ucMain1.Selection = commandline.GetNumber();
            this.ucMain1.SelectionScanOrder = commandline.GetScanOrder();
            this.ucMain1.SelectionMargin = commandline.GetMargin();
            this.ucMain1.SelectionBrush = new SolidBrush(Color.FromArgb(commandline.GetColorA(), commandline.GetColorR(), commandline.GetColorG(), commandline.GetColorB()));


            string binary = Convert.ToString(this.ucMain1.Selection, 2);
            MessageBox.Show("デバッグ binary=" + binary + " scanOrder=" + commandline.GetScanOrder() + " margin=" + commandline.GetMargin().ToString());
            this.Refresh();
        }
    }
}
