using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Fhey.Framework.Conversion
{
    public class Collections
    {
        /// <summary>
        /// 模型转字典集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IDictionary<string, string> ModelToDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties().ToDictionary(t => t.Name, v => v.GetValue(obj)?.ToString());
        }


        /// <summary>
        ///     DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public  List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            var t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach(t.GetProperties(), p =>
            {
                if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p);
            });

            //创建返回的集合    

            var oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                var ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p =>
                {
                    if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null);
                });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        ///     将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            var propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            var result = new DataTable();
            if (list.Count > 0)
            {
                var propertys = list[0].GetType().GetProperties();
                foreach (var pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (var i = 0; i < list.Count; i++)
                {
                    var tempList = new ArrayList();
                    foreach (var pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            var obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                var obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    var array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        public DataTable ListToDataTable<T>(IList<T> list)
        {
            return ToDataTable(list, null);
        }
    }
}
