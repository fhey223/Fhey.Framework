using System;
using System.CodeDom.Compiler;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

namespace Common
{
    public class StringHelper 
    {
        #region Tools
        /// <summary>
        ///     隐藏手机号 中间4位
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public string MobilePhoneHideConverter(string mobile)
        {
            if (string.IsNullOrEmpty(mobile) || mobile.Length != 11)
                return "***********";
            mobile = mobile.Substring(0, 3) + "****" + mobile.Substring(7, mobile.Length - 7);
            return mobile;
        }

        /// <summary>
        ///     金额 千位加逗号
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns></returns>
        public string MoneyConvert(decimal num)
        {
            var newstr = string.Empty;
            var r = new Regex(@"(\d+?)(\d{3})*(\.\d+|$)");
            var m = r.Match(num.ToString());
            newstr += m.Groups[1].Value;
            for (var i = 0; i < m.Groups[2].Captures.Count; i++)
            {
                newstr += "," + m.Groups[2].Captures[i].Value;
            }
            newstr += m.Groups[3].Value;
            if (num < 0)
            {
                newstr = "-" + newstr;
            }
            return newstr;
        }

        /// <summary>
        ///     金额 千位加逗号
        /// </summary>
        /// <param name="num">金额</param>
        /// <param name="round">保留小数点后几位</param>
        /// <returns></returns>
        public string MoneyConvert(decimal num, int round)
        {
            var newstr = string.Empty;
            var r = new Regex(@"(\d+?)(\d{3})*(\.\d+|$)");
            var m = r.Match(Math.Round(num, round).ToString());
            newstr += m.Groups[1].Value;
            for (var i = 0; i < m.Groups[2].Captures.Count; i++)
            {
                newstr += "," + m.Groups[2].Captures[i].Value;
            }
            newstr += m.Groups[3].Value;
            if (num < 0)
            {
                newstr = "-" + newstr;
            }
            return newstr;
        }
        #endregion
    }
}