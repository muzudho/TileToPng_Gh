using System;

namespace Grayscale.TileToPng.options
{
    public class EngineOption_NumberImpl : IEngineOption
    {
        public EngineOption_NumberImpl()
        {
        }

        public EngineOption_NumberImpl(long value)
        {
            this.m_value_ = value;
            this.m_default_ = value;
        }

        public EngineOption_NumberImpl(long value, long defaultValue)
        {
            this.m_value_ = value;
            this.m_default_ = defaultValue;
        }

        /// <summary>
        /// 既定値 long
        /// </summary>
        long m_default_;
        public object Default
        {
            get { return this.m_default_; }
            set { this.m_default_ = (long)value; }
        }

        /// <summary>
        /// 現在値 long
        /// </summary>
        long m_value_;
        public object Value
        {
            get { return this.m_value_; }
            set { this.m_value_ = (long)value; }
        }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            throw new ApplicationException("型変換エラー");
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public long GetNumber()
        {
            return this.m_value_;
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value"></param>
        public void ParseValue(string value)
        {
            if (long.TryParse(value, out long result))
            {
                this.m_value_ = result;
            }
        }
    }
}
