using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Fhey.Framework.Uility.Serialization.Interface;

namespace Fhey.Framework.Uility.Serialization
{
    public class XmlSerializer : IXmlSerializer
    {
        public string Serialize<TObject>(TObject obj)
        {
            MemoryStream ms = new MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(TObject));
            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);
            xs.Serialize(ms, obj, xmlns);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string str = sr.ReadToEnd();
            ms.Close();
            ms.Dispose();
            sr.Close();
            sr.Dispose();
            return str;
        }
        public TObject Deserialize<TObject>(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(TObject));
                return (TObject)xs.Deserialize(sr);
            }
        }
    }
}
