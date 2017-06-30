using Grayscale.TileToPng.n___100_cmdline_;
using System.Drawing;

namespace Grayscale.TileToPng.n100____cmdline_
{
    public class CommandlineImpl : Commandline
    {
        public CommandlineImpl(
            long value,
            ScanOrder scanOrder,
            Margin margin,
            int colorA,
            int colorR,
            int colorG,
            int colorB
            )
        {
            this.m_number_ = value;
            this.m_scanOrder_ = scanOrder;
            this.m_margin_ = margin;
            this.m_colorA_ = colorA;
            this.m_colorR_ = colorR;
            this.m_colorG_ = colorG;
            this.m_colorB_ = colorB;
        }

        /// <summary>
        /// 何かしらの指定された数字。用途は任意。
        /// </summary>
        private long m_number_;
        public long GetNumber()
        {
            return this.m_number_;
        }
        public void SetNumber(long value)
        {
            this.m_number_ = value;
        }

        /// <summary>
        /// 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        private Margin m_margin_;
        public Margin GetMargin()
        {
            return this.m_margin_;
        }
        public void SetMargin(Margin value)
        {
            this.m_margin_ = value;
        }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        private int m_colorA_;
        public int GetColorA()
        {
            return this.m_colorA_;
        }
        public void SetColorA(int value)
        {
            this.m_colorA_ = value;
        }
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        private int m_colorR_;
        public int GetColorR()
        {
            return this.m_colorR_;
        }
        public void SetColorR(int value)
        {
            this.m_colorR_ = value;
        }
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        private int m_colorG_;
        public int GetColorG()
        {
            return this.m_colorG_;
        }
        public void SetColorG(int value)
        {
            this.m_colorG_ = value;
        }
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        private int m_colorB_;
        public int GetColorB()
        {
            return this.m_colorB_;
        }
        public void SetColorB(int value)
        {
            this.m_colorB_ = value;
        }

        /// <summary>
        /// 走査する順番。
        /// </summary>
        private ScanOrder m_scanOrder_;
        public ScanOrder GetScanOrder()
        {
            return this.m_scanOrder_;
        }
        public void SetScanOrder(ScanOrder value)
        {
            this.m_scanOrder_ = value;
        }

    }
}
