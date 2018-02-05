using System.Text;


namespace Fhey.Framework.Uility.Extension
{
    public static class StringExtensions
    {
        #region Class Methods
        /// <summary>
        ///     对字符串进行裁剪
        /// </summary>
        public static string Cut(this string str, int maxLength)
        {
            return Cut(str, maxLength, "");
        }

        /// <summary>
        ///     对字符串进行裁剪(区分单字节及双字节字符)
        /// </summary>
        /// <param name="str">需要裁剪的字符串</param>
        /// <param name="maxLength">裁剪的长度，按双字节计数</param>
        /// <param name="AppendLineString">如果进行了裁剪需要附加的字符</param>
        public static string Cut(this string str, int maxLength, string AppendLineString)
        {
            if (!string.IsNullOrEmpty(str) && str.Length >= maxLength &&
                Encoding.UTF8.GetBytes(str).Length >= maxLength * 2)
            {
                var checkedStringBuilder = new StringBuilder();
                var AppendLineedLenth = 0;
                for (var i = 0; i < str.Length; i++)
                {
                    var _char = str[i];
                    checkedStringBuilder.Append(_char);
                    AppendLineedLenth += Encoding.Default.GetBytes(new[] { _char }).Length;
                    if (AppendLineedLenth >= maxLength * 2)
                        break;
                }
                str = checkedStringBuilder + AppendLineString;
            }
            return str;
        }

        /// <summary>
        ///     删除字符最后结尾指定的字符
        /// </summary>
        public static string DelLastChar(this string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                str =  "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
                str = str.Substring(0, str.LastIndexOf(strchar));
            return str;
        }
        #endregion
    }
}
