using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取Configuration对象
            var config = new Fhey.Framework.Uility.FileOperation.ConfigOperation();
            //根据Key读取<add>元素的Value
            string name = config.Get("name");
            //写入<add>元素的Value
            config.Set("name", "fx163");
            //增加<add>元素
            config.Add("url", "http://www.fx163.net");
            //删除<add>元素
            config.Remove("name");
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        
    }
    public class ConfigOperation
    {
        
    }
}
