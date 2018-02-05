using System.Web.Script.Serialization;
using Fhey.Framework.Uility.Serialization.Interface;

namespace Fhey.Framework.Uility.Serialization
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<TObject>(TObject obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public TObject Deserialize<TObject>(string serializedValue)
        {
            return new JavaScriptSerializer().Deserialize<TObject>(serializedValue);
        }
    }
}
