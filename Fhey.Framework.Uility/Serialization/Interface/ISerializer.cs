namespace Fhey.Framework.Uility.Serialization.Interface
{
    public interface ISerializer<TSerialized>
    {
        TSerialized Serialize<TObject>(TObject obj);
        TObject Deserialize<TObject>(TSerialized serializedValue);
    }
}
