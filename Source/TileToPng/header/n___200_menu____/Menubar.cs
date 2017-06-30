using System.Drawing;

namespace Grayscale.TileToPng.n___200_menu____
{
    public interface Menubar
    {
        /// <summary>
        /// サイズの更新
        /// </summary>
        /// <param name="width"></param>
        void UpdateSize(int width);

        /// <summary>
        /// カーソル・モード
        /// </summary>
        CursorMode CursorMode { get; set; }

        /// <summary>
        /// 位置とサイズ
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// 背景色ブラシ
        /// </summary>
        Brush BrushBackground { get; set; }

        /// <summary>
        /// 文字ブラシ
        /// </summary>
        Brush BrushText { get; set; }

        /// <summary>
        /// フォント
        /// </summary>
        Font Font { get; set; }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g"></param>
        void Paint(Graphics g);

        /// <summary>
        /// マウス左ボタン押下時
        /// </summary>
        /// <param name="mouseLocation"></param>
        bool OnMouseLeftDown(Point mouseLocation);

    }
}
