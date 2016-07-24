using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;
using Grayscale.TileToPng.n___100_cmdline_;
using Grayscale.TileToPng.n100____cmdline_;

namespace Grayscale.TileToPng
{
    public partial class UcMain : UserControl
    {
        public UcMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 金色のペン。
        /// </summary>
        private Pen m_penGold_;

        /// <summary>
        /// 青色のペン。
        /// </summary>
        private Pen m_penBlue_;

        /// <summary>
        /// 緑のブラシ。
        /// </summary>
        private Brush m_brushGreen_;

        /// <summary>
        /// リサイズ用のつまみ。
        /// </summary>
        private RectangleF m_resizer_;
        private bool m_resizerPressing_;

        /// <summary>
        /// グリッド
        /// </summary>
        private Grid m_grid_;
        /// <summary>
        /// ファイル名のテーブル。
        /// </summary>
        private string[,,] m_gridFilenames_;
        private Image[,,] m_gridImages_;
        private const int GRID_MAX_WIDTH = 30;
        private const int GRID_MAX_HEIGHT = 30;
        private const int GRID_MAX_LAYER = 5;

        /// <summary>
        /// （コピー＆ペースト）
        /// </summary>
        private string m_clipboardFilename_;
        private Image m_clipboardImage_;

        /// <summary>
        /// 水色のカーソル
        /// </summary>
        private RectangleF m_cursor_;
        private Point m_cursorPos_;
        private int m_cursorZ_;

        /// <summary>
        /// レイヤー表示（編集レイヤー・ラジオボタン）
        /// </summary>
        private RectangleF[] m_layersEditsRadiobuttons_;

        /// <summary>
        /// レイヤー表示（可視レイヤー・チェックボックス）
        /// </summary>
        private Rectangle[] m_layersVisibledCheckboxes_;
        private bool[] m_layersVisibled_;

        /// <summary>
        /// 選択範囲関連
        /// </summary>
        private long m_selection_;
        public long Selection
        {
            get { return this.m_selection_; }
            set { this.m_selection_ = value; }
        }
        private ScanOrder m_selectionScanOrder_;
        public ScanOrder SelectionScanOrder
        {
            get { return this.m_selectionScanOrder_; }
            set { this.m_selectionScanOrder_ = value; }
        }
        private Margin m_selectionMargin_;
        public Margin SelectionMargin
        {
            get { return this.m_selectionMargin_; }
            set { this.m_selectionMargin_ = value; }
        }
        /// <summary>
        /// 選択範囲用の、半透明の指定色のブラシ。
        /// </summary>
        private Brush m_brushSelectionTranslucent_;
        public Brush SelectionBrush
        {
            get { return this.m_brushSelectionTranslucent_; }
            set { this.m_brushSelectionTranslucent_ = value; }
        }

        private void UcMain_Load(object sender, EventArgs e)
        {
            //────────────────────────────────────────
            // ペンとブラシ
            //────────────────────────────────────────
            this.m_penGold_ = new Pen(Brushes.Gold, 3.0f);
            this.m_penBlue_ = new Pen(Brushes.Blue, 3.0f);
            this.m_brushGreen_ = new SolidBrush(Color.Green);
            this.m_brushSelectionTranslucent_ = new SolidBrush(Color.FromArgb(128, 255, 255, 0));

            //────────────────────────────────────────
            // グリッド
            //────────────────────────────────────────
            this.m_grid_ = new GridImpl(
                // 原点
                60.0f,
                40.0f,
                // セルのサイズ
                40.0f,
                40.0f
                );

            // リサイザー（リサイズ用のつまみ）は、グリッドの右下に付く。
            this.m_resizer_ = new RectangleF(
                //タテ線５本分の横幅
                5 * this.m_grid_.CellW + this.m_grid_.Ox - 20.0f/2,
                //ヨコ線５本分の横幅
                5 * this.m_grid_.CellH + this.m_grid_.Oy - 20.0f / 2,
                20.0f,
                20.0f);

            this.UpdateGridSize();

            //────────────────────────────────────────
            // 選択範囲
            //────────────────────────────────────────
            this.m_selectionScanOrder_ = ScanOrder.None;
            this.m_selectionMargin_ = new MarginImpl();

            //────────────────────────────────────────
            // カーソル
            //────────────────────────────────────────
            this.m_cursorPos_ = new Point();
            this.m_cursor_ = new RectangleF();
            this.UpdateCursor();

            //────────────────────────────────────────
            // テーブル
            //────────────────────────────────────────
            // とりあえず固定長で。
            this.m_gridFilenames_ = new string[UcMain.GRID_MAX_LAYER, UcMain.GRID_MAX_HEIGHT, UcMain.GRID_MAX_WIDTH];
            this.m_gridImages_ = new Image[UcMain.GRID_MAX_LAYER, UcMain.GRID_MAX_HEIGHT, UcMain.GRID_MAX_WIDTH];

            //────────────────────────────────────────
            // レイヤー表示（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            this.m_layersEditsRadiobuttons_ = new RectangleF[UcMain.GRID_MAX_LAYER];
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                this.m_layersEditsRadiobuttons_[iLayer] = new RectangleF(
                    32.0f,
                    (UcMain.GRID_MAX_LAYER-1-iLayer) * this.m_grid_.CellH + 20.0f - 20.0f / 2 + this.m_grid_.Oy,
                    20.0f,
                    20.0f
                    );
            }

            //────────────────────────────────────────
            // レイヤー表示（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            this.m_layersVisibled_ = new bool[UcMain.GRID_MAX_LAYER];
            this.m_layersVisibledCheckboxes_ = new Rectangle[UcMain.GRID_MAX_LAYER];
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                // 初期状態はすべて可視
                this.m_layersVisibled_[iLayer] = true;

                this.m_layersVisibledCheckboxes_[iLayer] = new Rectangle(
                    8,
                    (int)((UcMain.GRID_MAX_LAYER - 1 - iLayer) * this.m_grid_.CellH + 20.0f - 20.0f / 2 + this.m_grid_.Oy),
                    20,
                    20
                    );
            }
        }


        private void UpdateGridSize()
        {
            // ヨコ線とタテ線の終点
            this.m_grid_.Next = new PointF(
                this.m_resizer_.X + this.m_resizer_.Width / 2,
                this.m_resizer_.Y + this.m_resizer_.Height / 2
                );
        }

        private void UpdateCursor()
        {
            this.m_cursor_.X = this.m_cursorPos_.X * this.m_grid_.CellW + this.m_grid_.Ox;
            this.m_cursor_.Y = this.m_cursorPos_.Y * this.m_grid_.CellH + this.m_grid_.Oy;
            this.m_cursor_.Width = this.m_grid_.CellW;
            this.m_cursor_.Height = this.m_grid_.CellH;
        }

        private void UcMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                if (iLayer == this.m_cursorZ_)
                {
                    g.FillEllipse(this.m_brushGreen_, this.m_layersEditsRadiobuttons_[iLayer]);
                }

                g.DrawEllipse(this.m_penGold_, this.m_layersEditsRadiobuttons_[iLayer]);
            }

            //────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                if (this.m_layersVisibled_[iLayer])
                {
                    g.FillRectangle(this.m_brushGreen_, this.m_layersVisibledCheckboxes_[iLayer]);
                }

                g.DrawRectangle(this.m_penGold_, this.m_layersVisibledCheckboxes_[iLayer]);
            }

            //────────────────────────────────────────
            // グリッド
            //────────────────────────────────────────

            // ヨコ線を引きます。
            for (float y= this.m_grid_.Oy; y<= this.m_grid_.Next.Y; y+= this.m_grid_.CellH)
            {
                g.DrawLine(this.m_penGold_, this.m_grid_.Ox, y, this.m_grid_.Next.X, y);
            }

            // タテ線を引きます。
            for (float x = this.m_grid_.Ox; x <= this.m_grid_.Next.X; x += this.m_grid_.CellW)
            {
                g.DrawLine(this.m_penGold_, x, this.m_grid_.Oy, x, this.m_grid_.Next.Y);
            }

            // とりあえず画像描画
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                if(this.m_layersVisibled_[iLayer])
                {
                    for (int y = 0; y < UcMain.GRID_MAX_HEIGHT; y++)
                    {
                        for (int x = 0; x < UcMain.GRID_MAX_WIDTH; x++)
                        {
                            if (null != this.m_gridImages_[iLayer, y, x])
                            {
                                g.DrawImage(this.m_gridImages_[iLayer, y, x],
                                    x * this.m_grid_.CellW + this.m_grid_.Ox,
                                    y * this.m_grid_.CellH + this.m_grid_.Oy
                                    );
                            }
                        }
                    }
                }
            }

            //────────────────────────────────────────
            // 選択範囲
            //────────────────────────────────────────
            this.PaintSelection(g, false);

            //────────────────────────────────────────
            // 青い四角のカーソル
            //────────────────────────────────────────
            g.DrawRectangle(this.m_penBlue_, this.m_cursor_.X, this.m_cursor_.Y, this.m_cursor_.Width, this.m_cursor_.Height);

            // 「つまみ」を角っこに置きます。
            if (this.m_resizerPressing_)
            {
                g.DrawEllipse(this.m_penBlue_, this.m_resizer_);
            }
            else
            {
                g.DrawEllipse(this.m_penGold_, this.m_resizer_);
            }
        }

        private void PaintSelection(Graphics g, bool isWriteCase)
        {
            if (
                0 < this.m_selection_
                &&
                !(0 == this.m_grid_.Cols || 0 == this.m_grid_.Rows)
            )
            {
                string binary = Convert.ToString(this.m_selection_, 2);
                int height = (int)(this.m_resizer_.Height / this.m_grid_.CellH);
                int width = (int)(this.m_resizer_.Width / this.m_grid_.CellW);

                PointF origin;
                if (ScanOrder.Hsw == this.m_selectionScanOrder_)
                {
                    float ox;
                    float oy;
                    if (isWriteCase)
                    {
                        ox = 0.0f;
                        oy = 0.0f;
                    }
                    else
                    {
                        ox = this.m_grid_.Ox;
                        oy = this.m_grid_.Oy;
                    }

                    origin = new PointF(
                        ox,
                        ((int)((this.m_grid_.Next.Y - this.m_grid_.Oy) / this.m_grid_.CellH) - 1) * this.m_grid_.CellH + oy
                        );
                }
                else
                {
                    // 未実装
                    origin = new PointF(); ;
                }

                for (int iScan = 0; iScan < binary.Length; iScan++)
                {
                    char figure = binary[binary.Length - 1 - iScan];

                    // マージンを省いたテーブル・サイズ。
                    int cols = this.m_grid_.Cols - this.m_selectionMargin_.East - this.m_selectionMargin_.West;
                    int rows = this.m_grid_.Rows - this.m_selectionMargin_.North - this.m_selectionMargin_.South;

                    int x;
                    switch (this.m_selectionScanOrder_)
                    {
                        case ScanOrder.Hsw:
                            x = (int)((iScan % cols + this.m_selectionMargin_.West) * this.m_grid_.CellW + origin.X);
                            break;
                        default:
                            x = (int)(iScan % rows * this.m_grid_.CellW + origin.X);
                            break;
                    }


                    int y;
                    switch (this.m_selectionScanOrder_)
                    {
                        case ScanOrder.Hsw:
                            y = (int)((iScan / cols + this.m_selectionMargin_.South) * -this.m_grid_.CellH + origin.Y);
                            break;
                        default:
                            y = (int)(iScan / rows * this.m_grid_.CellH + origin.Y);
                            break;
                    }

                    switch (figure)
                    {
                        case '1':
                            g.FillRectangle(
                                this.m_brushSelectionTranslucent_,
                                x,
                                y,
                                this.m_grid_.CellW,
                                this.m_grid_.CellH
                                );
                            break;
                    }
                }
            }
        }

        private void WritePng()
        {
            // テーブル・サイズ
            int cols, rows;
            {
                cols = (int)((this.m_grid_.Next.X - this.m_grid_.Ox) / this.m_grid_.CellW);
                rows = (int)((this.m_grid_.Next.Y - this.m_grid_.Oy) / this.m_grid_.CellH);

                if (UcMain.GRID_MAX_WIDTH < cols)
                {
                    cols = UcMain.GRID_MAX_WIDTH;
                }

                if (UcMain.GRID_MAX_HEIGHT < rows)
                {
                    rows = UcMain.GRID_MAX_HEIGHT;
                }

                /*１マスに、でかい画像を使っていることもあるので、これは理屈が合わなくなるぜ。
                // データを走査
                int dataMaxRow = 0;//1スタートの数字
                int dataMaxCol = 0;
                for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
                {
                    for (int iRow = 0; iRow < rows; iRow++)
                    {
                        for (int iCol = 0; iCol < cols; iCol++)
                        {
                            if (null != this.m_gridImages_[iLayer, iRow, iCol])
                            {
                                if (dataMaxCol < iCol + 1)
                                {
                                    dataMaxCol = iCol + 1;
                                }

                                if (dataMaxRow < iRow + 1)
                                {
                                    dataMaxRow = iRow + 1;
                                }
                            }
                        }
                    }
                }

                if (dataMaxCol < cols)
                {
                    cols = dataMaxCol;
                }

                if (dataMaxRow < rows)
                {
                    rows = dataMaxRow;
                }
                */
            }

            // 画像
            //タイムスタンプ
            string timestamp;
            {
                StringBuilder s = new StringBuilder();
                DateTime now = System.DateTime.Now;
                s.Append(string.Format("{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}_{6:000}",
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    now.Second,
                    now.Millisecond
                    ));
                timestamp = s.ToString();
            }

            {
                //Graphicsオブジェクトを取得
                Graphics g = null;

                try
                {
                    Bitmap bitmap;
                    bitmap = new Bitmap(
                        (int)(cols * this.m_grid_.CellW),
                        (int)(rows * this.m_grid_.CellH)
                        );
                    g = Graphics.FromImage(bitmap);

                    // とりあえず画像描画
                    for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
                    {
                        if (this.m_layersVisibled_[iLayer])
                        {
                            for (int row = 0; row < rows; row++)
                            {
                                for (int col = 0; col < cols; col++)
                                {
                                    if (null != this.m_gridImages_[iLayer, row, col])
                                    {
                                        // マージン無し
                                        g.DrawImage(this.m_gridImages_[iLayer, row, col],
                                            col * this.m_grid_.CellW,
                                            row * this.m_grid_.CellH
                                            );
                                    }
                                }
                            }
                        }
                    }

                    // 選択範囲描画
                    this.PaintSelection(g,true);

                    string file = Path.Combine(Application.StartupPath, "TileToPng_" + timestamp + ".png");
                    bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("保存しました： " + file);
                }
                finally
                {
                    if (null != g)
                    {
                        g.Dispose();
                    }
                }
            }
        }

        private void UcMain_MouseDown(object sender, MouseEventArgs e)
        {
            bool isRefresh = false;

            // リサイザー
            if (this.m_resizer_.Contains(e.Location))
            {
                this.m_resizerPressing_ = true;
                isRefresh = true;
            }

            //────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                if (this.m_layersEditsRadiobuttons_[iLayer].Contains(e.Location))
                {
                    this.m_cursorZ_ = iLayer;
                    isRefresh = true;
                    break;
                }
            }

            //────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                if (this.m_layersVisibledCheckboxes_[iLayer].Contains(e.Location))
                {
                    this.m_layersVisibled_[iLayer] = !this.m_layersVisibled_[iLayer];
                    isRefresh = true;
                    break;
                }
            }

            if (!isRefresh)
            {
                // まだ何もクリックしていなければ、カーソル移動をする。
                this.m_cursorPos_.X = (int)((e.X - this.m_grid_.Ox) / this.m_grid_.CellW);
                this.m_cursorPos_.Y = (int)((e.Y - this.m_grid_.Oy) / this.m_grid_.CellH);
                this.UpdateCursor();
                isRefresh = true;
            }

            if (isRefresh)
            {
                this.Refresh();
            }
        }

        private void UcMain_MouseUp(object sender, MouseEventArgs e)
        {
            if(this.m_resizerPressing_)
            {
                this.m_resizerPressing_ = false;
                this.Refresh();
            }
        }

        private void UcMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_resizerPressing_)
            {
                this.m_resizer_.X = e.X - this.m_resizer_.Width / 2;
                this.m_resizer_.Y = e.Y - this.m_resizer_.Height / 2;
                this.UpdateGridSize();
                this.Refresh();
            }
        }

        private void UcMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    //────────────────────────────────────────
                    // 編集
                    //────────────────────────────────────────
                    case Keys.C:
                        // コピー
                        this.m_clipboardFilename_ = this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X];
                        this.m_clipboardImage_ = this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X];
                        break;

                    case Keys.X:
                        // カット
                        this.m_clipboardFilename_ = this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X];
                        this.m_clipboardImage_ = this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X];

                        this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = null;
                        this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = null;
                        this.Refresh();
                        break;

                    case Keys.V:
                        // ペースト
                        this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = this.m_clipboardFilename_;
                        this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = this.m_clipboardImage_;
                        this.Refresh();
                        break;

                    //────────────────────────────────────────
                    // 入出力
                    //────────────────────────────────────────
                    case Keys.S:
                        // 保存
                        this.SaveWorking();
                        break;

                    case Keys.L:
                        // 再開
                        this.LoadWorking();
                        break;
                }
            }

            switch (e.KeyCode)
            {
                //────────────────────────────────────────
                // カーソル移動
                //────────────────────────────────────────
                case Keys.Enter://改行
                    this.DoNewline();
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Up://↑
                    this.m_cursorPos_.Y--;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Right://→
                    this.m_cursorPos_.X++;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Down://↓
                    this.m_cursorPos_.Y++;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Left://←
                    this.m_cursorPos_.X--;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                //────────────────────────────────────────
                // 編集
                //────────────────────────────────────────
                case Keys.Delete://削除
                    this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = null;
                    this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = null;
                    this.Refresh();
                    break;

                //────────────────────────────────────────
                // 入出力
                //────────────────────────────────────────
                //case Keys.PrintScreen: //プリント・スクリーン・キーは利かないようだ。
                case Keys.P://画像出力
                    this.WritePng();
                    break;
            }
        }

        private void UcMain_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 改行
        /// </summary>
        private void DoNewline()
        {
            if (
                this.m_cursorPos_.Y + 1 < GRID_MAX_HEIGHT
            )
            {
                // 強制改行
                this.m_cursorPos_.X = 0;
                this.m_cursorPos_.Y++;
            }
        }

        private void UcMain_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileNames =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // カーソルのある位置から順番に追加する。

            foreach (string name in fileNames)
            {
                if (
                    GRID_MAX_WIDTH <= this.m_cursorPos_.X
                    )
                {
                    this.DoNewline();
                }

                if (
                    this.m_cursorPos_.X < GRID_MAX_WIDTH
                    &&
                    this.m_cursorPos_.Y < GRID_MAX_HEIGHT
                    )
                {
                    this.m_gridFilenames_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = name;

                    // とりあえず画像読み込み
                    this.m_gridImages_[this.m_cursorZ_, this.m_cursorPos_.Y, this.m_cursorPos_.X] = Image.FromFile(name);

                    // カーソルを右へ。
                    this.m_cursorPos_.X++;
                    this.UpdateCursor();
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// 前回の作業状態から再開。
        /// </summary>
        private void LoadWorking()
        {
            string file = Path.Combine(Application.StartupPath, "TileToPng_save.txt");
            if (File.Exists(file))
            {
                string text = File.ReadAllText(file);

                string[] tokens = text.Split(',');
                int index = 0;

                for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
                {
                    for (int y = 0; y < UcMain.GRID_MAX_HEIGHT; y++)
                    {
                        for (int x = 0; x < UcMain.GRID_MAX_WIDTH; x++)
                        {
                            string token = tokens[index].Trim();
                            index++;

                            // 「%HOME%」という文字列が含まれていれば、フォルダーへのパスに置き換えるぜ☆（＾▽＾）
                            if (-1<token.IndexOf("%HOME%"))
                            {
                                token = token.Replace("%HOME%", Application.StartupPath);
                            }

                            if(""== token)
                            {
                                this.m_gridFilenames_[iLayer, y, x] = "";
                                this.m_gridImages_[iLayer, y, x] = null;
                            }
                            else
                            {
                                this.m_gridFilenames_[iLayer, y, x] = token;
                                // とりあえず画像読み込み
                                this.m_gridImages_[iLayer, y, x] = Image.FromFile(token);
                            }
                        }
                    }
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// 作業状態の保存。
        /// </summary>
        private void SaveWorking()
        {
            StringBuilder sb = new StringBuilder();

            for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
            {
                for (int y = 0; y < UcMain.GRID_MAX_HEIGHT; y++)
                {
                    for (int x = 0; x < UcMain.GRID_MAX_WIDTH; x++)
                    {
                        string filename = this.m_gridFilenames_[iLayer, y, x];


                        // フォルダーへのパスを「%HOME%」という文字に置き換えて短くするんだぜ☆（＾▽＾）
                        if (null!= filename && filename.StartsWith(Application.StartupPath))
                        {
                            filename = "%HOME%"+filename.Substring(Application.StartupPath.Length);
                        }

                        sb.Append(filename);
                        sb.Append(",");
                    }
                    sb.AppendLine();
                }
            }

            string file = Path.Combine(Application.StartupPath, "TileToPng_save.txt");
            File.WriteAllText(file,sb.ToString());
        }
        
    }
}
