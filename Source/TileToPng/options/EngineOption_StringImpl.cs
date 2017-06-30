using System;

namespace Grayscale.TileToPng.options
{
    public class EngineOption_StringImpl : IEngineOption
    {
        public EngineOption_StringImpl()
        {
            this.m_value_ = "";
            this.m_default_ = "";
        }

        /// <summary>
        /// 最初に入ってきた値を、既定値とします。
        /// </summary>
        /// <param name="value"></param>
        public EngineOption_StringImpl(string value)
        {
            this.m_value_ = value;
            this.m_default_ = value;
        }

        public EngineOption_StringImpl(string value, string defaultValue)
        {
            this.m_value_ = value;
            this.m_default_ = defaultValue;
        }

        /// <summary>
        /// 既定値 string
        /// </summary>
        string m_default_;
        public object Default
        {
            get { return this.m_default_; }
            set { this.m_default_ = (string)value; }
        }

        /// <summary>
        /// 現在値
        /// </summary>
        string m_value_;
        public object Value
        {
            get { return this.m_value_; }
            set { this.m_value_ = (string)value; }
        }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            if (bool.TryParse(this.m_value_,out bool result))
            {
                return result;
            }

            throw new ApplicationException("型変換エラー");
        }


        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public long GetNumber()
        {
            if (long.TryParse(this.m_value_, out long result))
            {
                return result;
            }

            throw new ApplicationException("型変換エラー");
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value"></param>
        public void ParseValue(string value)
        {
            this.m_value_ = value;
        }
    }
}
