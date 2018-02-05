using System;
using System.Collections;

namespace Fhey.Framework.Uility.Reflection.Interface
{
    public interface IObjectStuffer
    {
        void Populate(object obj,IDictionary propertys);
    }
}
