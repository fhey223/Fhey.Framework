using System.IO;

namespace Fhey.Framework.Uility.FileOperation
{
    public class FileOperation
    {
        /// <summary>
        /// 将文件转换成字符串
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isSpace"></param>
        /// <returns></returns>
        public static string GetTempleContent(string path)
        {
            string result = string.Empty;
            string sFileName = "";//HttpContext.Current.Server.MapPath(path);
            if (File.Exists(sFileName))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(sFileName))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch
                {
                    result = "读取文件(" + path + ")出错";
                }
            }
            else
            {
                result = "找不到文件：" + path;
            }
            return result;
        }

    }
}
