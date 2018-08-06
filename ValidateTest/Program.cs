using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var Validator = new Fhey.Framework.Validate.NumberValidate ();
            var a = Validator.IsNumber("ff"); 
        }
    }
}
