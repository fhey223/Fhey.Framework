namespace Fhey.Framework.Uility.Network.Http.Interface
{
    public interface IHttpRequestJsonObjectResultValidator
    {
        bool Validate<T>(T jsonObject);
    }
}