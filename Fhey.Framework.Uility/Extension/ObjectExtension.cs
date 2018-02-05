using System;
using System.Reflection;

namespace Fhey.Framework.Uility.FileOperation
{
    public static class ObjectExtensions
    {
        #region Class Methods
        /// <summary>
        /// 获取对象属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);

            if (property.IsNull())
            {
                return defaultValue;
            }

            object value = property.GetValue(obj, null);
            return value is T ? (T)value : defaultValue;
        }

        /// <summary>
        /// 设置对象属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);

            if (!property.IsNull())
            {
                property.SetValue(obj, value, null);
            }
        }

        /// <summary>
        /// 对象是否为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T @object)
        {
            return Equals(@object, null);
        }

        public static byte[] ToBytes(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj))
            {
                return null;
            }

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.GetBuffer();
            }
        }
        #endregion
    }
}
