using System;
using System.Collections;
using System.Reflection;
using Fhey.Framework.Uility.Reflection.Interface;

namespace Fhey.Framework.Uility.Reflection
{
    public class ObjectStuffer : IObjectStuffer
    {
        public void Populate(object obj, IDictionary propertys)
        {            
            Type type = obj.GetType();
            foreach(string key in propertys.Keys)
            {
                FieldInfo fieldInfo = type.GetField(key);
                fieldInfo.SetValue(obj, Convert.ChangeType(propertys[key], fieldInfo.FieldType));
            }
        }
    }
}
