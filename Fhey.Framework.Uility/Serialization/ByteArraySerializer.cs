using System.Text;
using Fhey.Framework.Uility.Serialization.Interface;

namespace Fhey.Framework.Uility.Serialization
{
    public class ByteArraySerializer : IByteArraySerializer
    {
        public byte[] Serialize<TObject>(TObject obj)
        {
            return Encoding.UTF8.GetBytes(obj.ToString());
        }

        public TObject Deserialize<TObject>(byte[] serializedValue)
        {
            if (null == serializedValue) return default(TObject);
            object obj = Encoding.UTF8.GetString(serializedValue);
            return (TObject)obj;
        }
    }
}
