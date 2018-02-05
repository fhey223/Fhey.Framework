using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Pooling
{
    public interface IPoolObjectFactory<T> where T : class,IDisposable
    {
        Func<object[], T> Creator { get; set; }
        Action<T> Destroyer { get; set; }

        T Create(params object[] parameters);
        void Destroy(T obj);
    }
}
