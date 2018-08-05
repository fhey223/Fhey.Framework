//using Microsoft.International.Converters.PinYinConverter;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Fhey.Framework.Uility
//{
//    /// <summary>
//    /// 汉子转拼音
//    /// </summary>
//    public class PinYinZipUility
//    {
//        public static string GetPinyin(string str)
//        {
//            var r = string.Empty;
//            foreach (var obj in str)
//            {
//                try
//                {
//                    var chineseChar = new ChineseChar(obj);
//                    var t = Convert.ToString(chineseChar.Pinyins[0]);
//                    r += t.Substring(0, t.Length - 1);
//                }
//                catch
//                {
//                    r += Convert.ToString(obj);
//                }
//            }
//            return r;
//        }

//        /// <summary> 
//        /// 获取拼音首字母
//        /// </summary> 
//        /// <param name="str">汉字</param> 
//        /// <returns>首字母</returns> 
//        public static string GetFirstPinyin(string str)
//        {
//            try
//            {
//                return new ChineseChar(str[0]).Pinyins[0].ToString().Substring(0, 1);
//            }
//            catch
//            {
//                return string.Empty;
//            }
//        }
//    }
//}
