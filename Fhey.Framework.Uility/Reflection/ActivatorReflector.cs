using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhey.Framework.Uility.Reflection.Interface;
using System.Reflection;
using System.Xml;

namespace Fhey.Framework.Uility.Reflection
{
    public class ActivatorReflector:IObjectReflector

    {
        private void GetAssemblyInfo(string type, out Assembly assembly, out Type t, out string classType)
        {
            t = null;
            assembly = null;
            classType = null;
            int index = type.IndexOf(",");
            if (index > 0)
            {
                string nameSpace = type.Substring(0, index);
                classType = type.Substring(index + 1);
                assembly = Assembly.Load(nameSpace);
            }
            else
            {
                classType = type;
                assembly = Assembly.GetExecutingAssembly();
            }
            t = assembly.GetType(classType);
        }

        public object Create(string type, IDictionary<string, object> propertys)
        {
            Type t = null;
            Assembly assembly = null;
            string classType = null;
            GetAssemblyInfo(type, out assembly, out t, out classType);
            object obj = System.Activator.CreateInstance(t);
            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertys.Keys.Contains(propertyInfo.Name))
                {
                    propertyInfo.SetValue(obj,
                        Convert.ChangeType(propertys[propertyInfo.Name], propertyInfo.PropertyType));
                }
            }
            return obj;
        }

        public object Create(string type)
        {
            Type t = null;
            Assembly assembly = null;
            string classType = null;
            GetAssemblyInfo(type, out assembly, out t, out classType);
            object obj = System.Activator.CreateInstance(t);
            PropertyInfo[] propertyInfos = t.GetProperties();
            return obj;
        }

        public object Create(string type, object[] args, IDictionary<string, object> propertys)
        {
            Type t = null;
            Assembly assembly = null;
            string classType = null;
            GetAssemblyInfo(type, out assembly, out t, out classType);
            object obj = System.Activator.CreateInstance(t, args);
            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertys.Keys.Contains(propertyInfo.Name))
                {
                    propertyInfo.SetValue(obj,
                        Convert.ChangeType(propertys[propertyInfo.Name], propertyInfo.PropertyType));
                }
            }
         
            return obj;
        }

        public TResult Create<TResult>(Type type, IDictionary<string, object> propertys) where TResult : class
        {
            return Create(type.FullName, propertys) as TResult;
        }

        public TResult Create<TResult>(Type type, object[] args, IDictionary<string, object> propertys) where TResult : class
        {
            return Create(type.FullName, args, propertys) as TResult;
        }

    }
}
