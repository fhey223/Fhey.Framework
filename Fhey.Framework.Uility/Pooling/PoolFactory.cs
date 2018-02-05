using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Pooling
{
    public class PoolFactory
    {
        private static IPoolRepository _poolRepository;

        static PoolFactory()
        {
            _poolRepository = new PoolRepository();
        }

        public static IPool<T> Create<T>(IPoolConfiguration<T> config) where T : class,IDisposable
        { 
            return _poolRepository.Create(config);
        }
    }
}
