using NLua;
using System;
using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // Luaファイル名
        public const string LUA_FILE = "Work/TileToPng.lua";

        // 静的コンストラクター
        static Program()
        {
            #region Luaの初期設定
            lua = new Lua();
            // 初期化
            lua.LoadCLRPackage();

            // ファイルの読み込み
            lua.DoFile(LUA_FILE);

            // タイル画像の横幅
            var value = lua["TILE_WIDTH"];
            if (!(value is double)) { value = 32d; }
            tileWidth = (int)((double)value);

            // タイル画像の縦幅
            value = lua["TILE_HEIGHT"];
            if (!(value is double)) { value = 32d; }
            tileHeight = (int)((double)value);

            #endregion
        }

        // Luaスクリプト・ファイルを１個しか使わないのなら、インスタンス１つで十分。
        public static Lua lua;

        public static int tileWidth;
        public static int tileHeight;
    }
}
