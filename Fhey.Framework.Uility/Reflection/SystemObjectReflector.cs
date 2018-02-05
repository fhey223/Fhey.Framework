using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhey.Framework.Uility.Reflection.Interface;

namespace Fhey.Framework.Uility.Reflection
{
    public class SystemObjectReflector : IObjectReflector
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
            object obj = assembly.CreateInstance(classType);
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
            object obj = assembly.CreateInstance(classType);
            PropertyInfo[] propertyInfos = t.GetProperties();
            return obj;
        }

        public object Create(string type, object[] args, IDictionary<string, object> propertys)
        {
            Type t = null;
            Assembly assembly = null;
            string classType = null;
            GetAssemblyInfo(type, out assembly, out t, out classType);
            object obj = null;
            ConstructorInfo[] cis = t.GetConstructors();
            if (null != args && args.Length > 0 && cis.Length > 0)
            {
                for (int i = 0; i < cis.Length; i++)
                {
                    if (args.Length == cis[i].GetParameters().Length)
                    {
                        obj = cis[i].Invoke(args);
                        foreach (PropertyInfo propertyInfo in t.GetProperties())
                        {
                            if (propertys.Keys.Contains(propertyInfo.Name))
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(propertys[propertyInfo.Name], propertyInfo.PropertyType));
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                obj = Create(type, propertys);
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
