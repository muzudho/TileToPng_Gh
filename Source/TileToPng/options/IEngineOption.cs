namespace Grayscale.TileToPng.options
{
    public interface IEngineOption
    {

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value"></param>
        void ParseValue(string value);

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        bool IsTrue();

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        long GetNumber();

        /// <summary>
        /// 既定値
        /// bool, long, string
        /// </summary>
        object Default { get; set; }

        /// <summary>
        /// 現在値
        /// bool, long, string
        /// </summary>
        object Value { get; set; }
    }
}
