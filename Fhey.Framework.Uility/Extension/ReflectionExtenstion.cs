using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Fhey.Framework.Uility.FileOperation
{
    public static class ReflectionExtenstions
    {
        private static readonly IDictionary<string, Attribute> attributeCache = new ConcurrentDictionary<string, Attribute>();
        private static readonly IDictionary<string, PropertyInfo[]> propertiesCache = new ConcurrentDictionary<string, PropertyInfo[]>();

        public static PropertyInfo[] GetPropertieArray(this Type obj)
        {
            string name = Unique(obj);
            PropertyInfo[] propertyInfos = null;
            if (!propertiesCache.TryGetValue(name, out propertyInfos))
            {
                propertyInfos = obj.GetProperties();
                propertiesCache[name]=propertyInfos;
            }
            return propertyInfos;
        }

        public static T GetAttribute<T>(this Type obj) where T : Attribute
        {
            return GetAttbribute<T, Type>(string.Format("{0}.{1}", Unique(obj), typeof(T).Name), obj, (o) =>
            {
                return o.GetCustomAttribute<T>();
            });
        }

        public static T GetAttribute<T>(this MethodBase obj) where T : Attribute
        {
            return GetAttbribute<T, MethodBase>(string.Format("{0}.{1}", Unique(obj), typeof(T).Name), obj, (o) =>
            {
                return o.GetCustomAttribute<T>();
            });
        }

        public static T GetAttribute<T>(this PropertyInfo obj) where T : Attribute
        {
            return GetAttbribute<T, PropertyInfo>(string.Format("{0}.{1}", Unique(obj), typeof(T).Name), obj, (o) =>
            {
                return o.GetCustomAttribute<T>();
            });
        }

        private static TAtttribute GetAttbribute<TAtttribute, TObject>(string name, TObject obj, Func<TObject, TAtttribute> getter) where TAtttribute : Attribute
        {
            Attribute attribute = null;
            if (!attributeCache.TryGetValue(name, out attribute))
            {
                attribute = getter(obj);
                attributeCache[name] = attribute;
            }
            return (TAtttribute)attribute;
        }

        public static string Unique(this Type obj)
        {
            return string.Format("{0}[{1}]", obj.Name, obj.GUID);
        }

        public static string Unique(this MethodBase obj)
        {
            return string.Format("{0}[{1}].{2}", obj.DeclaringType.Name, obj.DeclaringType.GUID,obj.Name);
        }

        public static string Unique(this PropertyInfo obj)
        {
            return string.Format("{0}[{1}].{2}", obj.DeclaringType.Name,obj.DeclaringType.GUID, obj.Name);
        }
    }
}
