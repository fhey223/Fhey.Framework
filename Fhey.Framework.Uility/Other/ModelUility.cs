using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fhey.Framework.Uility
{
    public class ModelUility
    {  
         public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        ///     深拷贝
        /// </summary>
        /// <typeparam name="T">被拷贝对象类型</typeparam>
        /// <param name="obj">被拷贝对象</param>
        /// <returns></returns>
        public T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        ///     对象拷贝(反射，第三方控件AtuoMapper可替代)
        /// </summary>
        /// <typeparam name="TFromObj">拷贝参照对象类型</typeparam>
        /// <typeparam name="TToObj">被拷贝对象类型</typeparam>
        /// <param name="fromObj">拷贝参照对象</param>
        /// <param name="toObj">被拷贝对象</param>
        public void Copy<TFromObj, TToObj>(TFromObj fromObj, TToObj toObj)
        {
            var fromObjType = fromObj.GetType();
            var fromObjPropertys = fromObjType.GetProperties();
            var toObjType = toObj.GetType();

            foreach (var propertyItem in fromObjPropertys)
            {
                try
                {
                    var toObjProperty = toObjType.GetProperty(propertyItem.Name);
                    if (toObjProperty == null)
                        continue;
                    var fromValue = propertyItem.GetValue(fromObj, null);
                    toObjProperty.SetValue(toObj, fromValue, null);
                }
                catch
                {
                    continue;
                }
            }
        }


        /// <summary>
        /// 根据名称（属性或字段）获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object GetValue<T>(string fieldName,T obj)
        {
            object value;
            var property = obj.GetType().GetProperty(fieldName);
            var field = obj.GetType().GetField(fieldName);
            if (property != null)
                value = property.GetValue(obj, null);
            else if (field != null)
                value = field.GetValue(obj);
            else
                value = null;
            return value;
        }

    }

}
