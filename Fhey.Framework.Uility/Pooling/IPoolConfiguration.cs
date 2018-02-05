using System;
using System.Security.Cryptography.X509Certificates;

namespace Fhey.Framework.Uility.Pooling
{
    public interface IPoolConfiguration<T> where T : class,IDisposable
    {
        string Key { get; set; }
        int InitialSize { get; set; }
        int MaxSize { get; set; }
        IPoolObjectFactory<T> ObjectFactory { get; set; }
    }
}
