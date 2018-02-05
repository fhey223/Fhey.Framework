using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Pooling
{
    public class PoolObjectFactory<T> : IPoolObjectFactory<T> where T : class, IDisposable
    {
        public Func<object[],T> Creator { get; set; }
        public Action<T> Destroyer { get; set; }

        public virtual T Create(params object[] parameters)
        {
            return Creator(parameters);
        }

        public virtual void Destroy(T obj)
        {
            Destroyer(obj);
        }
    }
}
