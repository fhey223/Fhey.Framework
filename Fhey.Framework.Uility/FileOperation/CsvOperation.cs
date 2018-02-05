using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Fhey.Framework.Uility.FileOperation
{
    public class CsvOperation 
    {
        public string Export(string fileName, DataTable data)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Rows.Count == 0)
                throw new ArgumentNullException("fileName or data", "参数值不合法");
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in data.Columns)
            {
                if (sb.Length > 0)
                    sb.Append(',');

                sb.Append(column.Caption);
            }
            sb.Append("\n");

            foreach (var row in data.AsEnumerable().Select(r => string.Join(",", r.ItemArray)))
            {
                sb.AppendLine(row);
            }

            string filePath = string.Format("{0}{1}{2}", AppDomain.CurrentDomain.BaseDirectory, fileName, ".CSV");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.GetEncoding("GB2312")))
            {
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }

            sb.Clear();
            return filePath;
        }

        public string Import(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader str = new StreamReader(fs,Encoding.Default);
            return str.ReadToEnd(); ;
        }
    }
}
