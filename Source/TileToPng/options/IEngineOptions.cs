namespace Grayscale.TileToPng.options
{
    /// <summary>
    /// USI「setoption」コマンドのリストです。
    /// </summary>
    public interface IEngineOptions
    {
        /// <summary>
        /// 項目の有無
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ContainsKey(string name);

        /// <summary>
        /// 項目を上書き。なければ文字列型として項目を追加。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entry"></param>
        void ParseValue_AutoAdd(string name, string value);

        /// <summary>
        /// 項目を追加。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entry"></param>
        void AddEntry(string name, IEngineOption entry);

        /// <summary>
        /// 項目を取得。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEngineOption GetEntry(string name);

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void ParseValue(string name, string value);

    }
}
