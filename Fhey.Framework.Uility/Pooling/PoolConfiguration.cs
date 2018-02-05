using System;

namespace Fhey.Framework.Uility.Pooling
{
    public class PoolConfiguration<T> : IPoolConfiguration<T> where T : class,IDisposable
    {
        public virtual string Key { get; set; }
        public virtual int InitialSize { get; set; }
        public virtual int MaxSize { get; set; }
        public IPoolObjectFactory<T> ObjectFactory { get; set; }
    }
}
