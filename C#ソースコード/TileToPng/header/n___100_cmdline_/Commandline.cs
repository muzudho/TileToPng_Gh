using System.Drawing;

namespace Grayscale.TileToPng.n___100_cmdline_
{
    public interface Commandline
    {
        /// <summary>
        /// 何かしらの指定された数字。用途は任意。
        /// </summary>
        long GetNumber();
        void SetNumber(long value);

        /// <summary>
        /// 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        Margin GetMargin();
        void SetMargin(Margin value);

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int GetColorA();
        void SetColorA(int value);
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int GetColorR();
        void SetColorR(int value);
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int GetColorG();
        void SetColorG(int value);
        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int GetColorB();
        void SetColorB(int value);

        /// <summary>
        /// 走査する順番。
        /// </summary>
        ScanOrder GetScanOrder();
        void SetScanOrder(ScanOrder value);

    }
}
