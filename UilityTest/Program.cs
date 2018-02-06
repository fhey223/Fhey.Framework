using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new int[]{1,2};
            var a = JsonConvert.SerializeObject("er");
            var a1 = JsonConvert.DeserializeObject<string>(a);

            Set("1", a);


        }

        public static bool Set<T>(string key, T value, DateTime? expiresTime = default(DateTime?))
        {

            var a= expiresTime - DateTime.Now;
            return true;
        }

        public static bool Set(string key, string value, DateTime? expiresTime = default(DateTime?))
        {

            var a = expiresTime - DateTime.Now;
            return true;
        }

    }
}
