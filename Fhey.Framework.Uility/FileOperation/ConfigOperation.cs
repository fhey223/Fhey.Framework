using System.Configuration;

namespace Fhey.Framework.Uility.FileOperation
{
    /// <summary>
    /// 经试验没有物理上改变config文件，但是在程序运行期间操作值是用变化的
    /// </summary>
    public class ConfigOperation
    {
        Configuration _obj;
        KeyValueConfigurationCollection _config; 
        public ConfigOperation()
        {
            _obj = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _config = _obj.AppSettings.Settings;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string Get(string Key)
        {
            return _config[Key].Value;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Add(string Key,string Value)
        {
            _config.Add (Key,Value);
            _obj.Save();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public void Set(string Key, string Value)
        {
             _config[Key].Value = Value;
            _obj.Save();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Key"></param>
        public void Remove(string Key)
        {
            _config.Remove(Key);
            _obj.Save();
        }
    }
}
