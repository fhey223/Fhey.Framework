﻿using System.Text.RegularExpressions;

namespace Fhey.Framework.Validate
{
    public class ValidateBase
    {
        public ValidateBase()
        {

        }

        public bool CommonValidate(string value,string regx)
        {
            var a = Regex.Match(value, regx);
            return Regex.IsMatch (value, regx);
        }
    }
}
