namespace Fhey.Framework.Validate
{
    public class ContactValidate:ValidateBase
    {
        //是否手机号
        public bool IsPhone(string value)
        {
            return CommonValidate(value, RegxFactory.IsPhone);
        }

        //是否固定电话(国内)
         public bool IsTel(string value)
         {
            return CommonValidate(value, RegxFactory.IsTel);
         }

        //是否Email
         public bool IsEmail(string value)
        {
            return CommonValidate(value, RegxFactory.IsEmail);
        }

        // 是否邮政编码
         public bool IsZipCode(string value)
        {
            return CommonValidate(value, RegxFactory.IsZipCode);
        }

        // 是否QQ
         public bool IsQQ(string value)
        {
            return CommonValidate(value, RegxFactory.IsQQ);
        }

        //身份证验证
         public bool IsIdentity(string value)
        {
            return CommonValidate(value, RegxFactory.IsIdentity);
        }

        //是否网址
        public bool IsURL(string value)
        {
            return CommonValidate(value, RegxFactory.IsURL);
        }
    }
}
