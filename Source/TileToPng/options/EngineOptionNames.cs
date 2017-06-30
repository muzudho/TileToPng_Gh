namespace Grayscale.TileToPng.options
{
    public abstract class EngineOptionNames
    {
        /// <summary>
        /// 思考時間制限（ミリ秒）
        /// </summary>
        public const string THINKING_MILLI_SECOND = "thinkingMilliSecond";

        /// <summary>
        /// USI「ponder」の使用の有無です。
        /// </summary>
        public const string USI_PONDER = "USI_ponder";

        /// <summary>
        /// サーバーに「noop」コマンドを投げてもよいかどうかです。
        /// サーバーに「noop」を送ると、「ok」を返してくれるものとします。１分間を空けてください。
        /// </summary>
        public const string NOOPABLE = "noopable";
    }
}
