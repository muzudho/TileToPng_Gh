using Grayscale.TileToPng.n___200_menu____;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Grayscale.TileToPng.n200____menu____
{
    public class MenubarImpl : Menubar
    {
        public MenubarImpl()
        {
            int height = 20;//グラデーションのための幅
            this.m_bounds_ = new Rectangle(0,0,
                10,//目視できるように幅をちょっとだけ指定。
                height);
            this.m_brushBackground_ = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),//不透明の黒
                Color.FromArgb(96, 0, 0, 0)//半透明の黒
                );
            this.m_brushText_ = new SolidBrush(Color.White);
            this.m_font_ = new Font("Serif", 12.0f);
            this.m_cursorMode_ = CursorMode.Pointer;
        }

        public MenubarImpl(int x, int y, int width, int height, Font font)
        {
            this.m_bounds_ = new Rectangle(x,y,width,height);
            this.m_brushBackground_ = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),//不透明の黒
                Color.FromArgb(96, 0, 0, 0)//半透明の黒
                );
            this.m_brushText_ = new SolidBrush(Color.White);
            this.m_font_ = font;
            this.m_cursorMode_ = CursorMode.Pointer;
        }

        /// <summary>
        /// サイズの更新
        /// </summary>
        /// <param name="width"></param>
        public void UpdateSize(int width)
        {
            this.m_bounds_.Width = width;
        }

        /// <summary>
        /// カーソル・モード
        /// </summary>
        private CursorMode m_cursorMode_;
        public CursorMode CursorMode
        {
            get { return this.m_cursorMode_; }
            set { this.m_cursorMode_ = value; }
        }

        /// <summary>
        /// 位置とサイズ
        /// </summary>
        private Rectangle m_bounds_;
        public Rectangle Bounds
        {
            get { return this.m_bounds_; }
            set { this.m_bounds_ = value; }
        }

        /// <summary>
        /// 背景色ブラシ
        /// </summary>
        private Brush m_brushBackground_;
        public Brush BrushBackground
        {
            get { return this.m_brushBackground_; }
            set { this.m_brushBackground_=value; }
        }

        /// <summary>
        /// 文字ブラシ
        /// </summary>
        private Brush m_brushText_;
        public Brush BrushText
        {
            get { return this.m_brushText_; }
            set { this.m_brushText_ = value; }
        }

        /// <summary>
        /// フォント
        /// </summary>
        private Font m_font_;
        public Font Font
        {
            get { return this.m_font_; }
            set { this.m_font_ = value; }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            g.FillRectangle(this.BrushBackground, this.Bounds);

            // カーソル・モード
            string label;
            switch (this.CursorMode)
            {
                case CursorMode.Pointer:
                    label = "カーソル: ポインター";
                    break;
                case CursorMode.Copy:
                    label = "カーソル: コピー";
                    break;
                default:
                    label = "カーソル: ムーブ";
                    break;
            }

            g.DrawString(label, this.Font,this.BrushText, new PointF(this.Bounds.X, this.Bounds.Y));
        }

        /// <summary>
        /// マウス左ボタン押下時
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <returns>このオブジェクトをクリックしたのなら真</returns>
        public bool OnMouseLeftDown(Point mouseLocation)
        {
            if (this.Bounds.Contains(mouseLocation))
            {
                // カーソル・モードをローテーションするぜ☆（＾▽＾）
                switch (this.CursorMode)
                {
                    case CursorMode.Pointer:
                        this.CursorMode = CursorMode.Copy;
                        break;
                    case CursorMode.Copy:
                        this.CursorMode = CursorMode.Move;
                        break;
                    default:
                        this.CursorMode = CursorMode.Pointer;
                        break;
                }
                return true;
            }

            return false;
        }
    }
}
