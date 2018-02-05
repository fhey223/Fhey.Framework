using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class EnumHelper 
    {
        public string GetDescription(Enum obj)
        {
            var description = ((DescriptionAttribute[])obj
                .GetType()
                .GetField(obj.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false))
                .First()
                .Description;
            return description;
        }

        /// <summary>
        ///     获取Description,Value列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetItems(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException();
            var list = new Dictionary<int, string>();

            // 获取Description特性 
            var typeDescription = typeof(DescriptionAttribute);

            foreach (var field in enumType.GetFields())// enumType.GetFields()获取枚举字段
            {
                if (!field.FieldType.IsEnum)
                    continue;
                // 获取枚举值
                var value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                // 不包括空项
                if (value > 0)
                {
                    var text = string.Empty;
                    var array = field.GetCustomAttributes(typeDescription, false);
                    if (array.Length > 0) text = ((DescriptionAttribute)array[0]).Description;
                    else text = field.Name; //没有描述，直接取值
                    //添加到列表
                    list.Add(value, text);
                }
            }
            return list;
        }
    }
}
