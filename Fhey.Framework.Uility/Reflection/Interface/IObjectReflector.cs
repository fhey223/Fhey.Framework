using System;
using System.Collections.Generic;

namespace Fhey.Framework.Uility.Reflection.Interface
{
    public interface IObjectReflector
    {
        TResult Create<TResult>(Type type, IDictionary<string, object> propertys) where TResult : class;

        object Create(string type, IDictionary<string, object> propertys);

        object Create(string type);

        TResult Create<TResult>(Type type, object[] args, IDictionary<string, object> propertys) where TResult : class;

        object Create(string type, object[] args, IDictionary<string, object> propertys);
    }
}
