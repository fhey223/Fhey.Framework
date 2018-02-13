using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Validate
{
    public class DateValidate:ValidateBase
    {
        //验证日期
        //支持:YYYY-MM-DD、YYYY/MM/DD、YYYY_MM_DD、YYYY.MM.DD的形式
        public bool IsDate(string value)
        {
            return CommonValidate(value, RegxFactory.IsDate);
        }

        //验证年份(年份范围为 0001 - 9999)
         public bool IsYear(string value)
        {
            return CommonValidate(value, RegxFactory.IsYear);
        }
    }
}
