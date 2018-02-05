using System.Text;
using System.Text.RegularExpressions;

namespace Fhey.Framework.Conversion
{
    /// <summary>
    /// 字母与数字转换
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// 数字转大写字母
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetUppercase(int number)
        {
            if (number < 1 || number > 26)
            {
                return "*";
            }
            return ((char)('A' + (char)((number - 1) % 26))).ToString();
        }

        /// <summary>
        /// 数字装小写字母
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetLowercase(int number)
        {
            if (number < 1 || number > 26)
            {
                return "*";
            }
            return ((char)('a' + (char)((number - 1) % 26))).ToString();
        }

        /// <summary>
        /// 字母转数字
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int AlphabetToNum(string en)
        {
            int num;
            if (new Regex(@"^[A-Z]+$").IsMatch(en))
            {
                num = new ASCIIEncoding().GetBytes(en)[0] - 64;
            }
            else if (new Regex(@"^[a-z]+$").IsMatch(en))
            {
                num = new ASCIIEncoding().GetBytes(en)[0] - 96;
            }
            else
            {
                num = 0;
            }
            return num;
        }
    }
}
