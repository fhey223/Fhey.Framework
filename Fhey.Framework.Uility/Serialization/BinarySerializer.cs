using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Fhey.Framework.Uility.Serialization.Interface;

namespace Fhey.Framework.Uility.Serialization
{
    public class BinarySerializer : IByteArraySerializer
    {
        public byte[] Serialize<TObject>(TObject obj)
        {
            if (null == obj) return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public TObject Deserialize<TObject>(byte[] serializedValue)
        {
            if (null == serializedValue || serializedValue.Length<1) return default(TObject);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(serializedValue))
            {
                ms.Seek(0, SeekOrigin.Begin);
                object obj = bf.Deserialize(ms);
                return (TObject)obj;
            }
        }
    }
}
