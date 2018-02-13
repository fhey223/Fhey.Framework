using System.Text.RegularExpressions;

namespace Fhey.Framework.Validate
{
    public class NumberValidate:ValidateBase
    {
        //判断输入是否为数字
        public bool IsNumber(string value)
        {
            //return CommonValidate(value, @"/^\-?[0-9]+(.[0-9]+)?$/");
            // return Regex.IsMatch(value, @"/^\-?[0-9]+(.[0-9]+)?$/");
            return new Regex(@"^\-?[0-9]+(.[0-9]+)?$").Match(value).Success;
        }

        //是否为整数
        public bool IsInt(string value)
        {
            return CommonValidate(value, RegxFactory.IsInt);
        }

        //是否为正数，除零外首位不能不为零
        public bool IsPositiveNumer(string value)
        {
            return CommonValidate(value, RegxFactory.IsPositiveNumer);
        }

        //是否为正整数
        public bool IsPositiveInt(string value)
        {
            return CommonValidate(value, RegxFactory.IsPositiveInt);
        }

        //是否为自然数
        public bool IsNaturalNumer(string value)
        {
            return CommonValidate(value, RegxFactory.IsNaturalNumer);
        }

        //是否正浮点数
        public bool IsPositiveFloat(string value)
        {
            return CommonValidate(value, RegxFactory.IsPositiveFloat);
        }

        //位数校验
        public bool CheckLength(string value, int length)
        {
            return value.Length.Equals(length);
        }

        //浮点数位数校验
        public bool CheckPoint(string value, int length)
        {
            return Regex.IsMatch(value, @" /^[1-9]([0-9]+)?(\.[0-9]{" + length + "})$/g");
        }
    }
}
