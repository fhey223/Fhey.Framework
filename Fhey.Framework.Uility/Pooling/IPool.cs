using System;

namespace Fhey.Framework.Uility.Pooling
{
    public interface IPool<T> : IDisposable where T : class,IDisposable
    {
        void Put(T obj);
        T Get();
        int Size { get; }
        IPoolConfiguration<T> Config { get; }
    }
}
