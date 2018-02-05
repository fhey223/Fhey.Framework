using System;

namespace Fhey.Framework.Uility.Pooling
{
    public interface IPoolRepository : IDisposable
    {
        IPool<T> Create<T>(IPoolConfiguration<T> config) where T : class,IDisposable;
        IPool<T> Get<T>(string key) where T : class, IDisposable;
        void Remove(string key);
    }
}
