using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCWOnline.COC.Common
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// 反射获取实体类的属性
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实例化</param>
        /// <returns>属性字符串集合</returns>
        public static List<string> GetProperties<T>(T t)
        {
            //如果实力对象为空则返回null
            if (t == null)
                return null;

            //遍历对象中的属性（表中的字段）
            //BindingFlags筛选标志：
            //为了获取返回值，必须指定 BindingFlags.Instance
            //指定 BindingFlags.Public 可在搜索中包含公共成员
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            //如果结果中中不包含字段则返回null
            if (properties.Length <= 0)
                return null;

            //定义字符串集合存储字段名
            List<string> columnList = new List<string>();

            //遍历PropertyInfo数组对象，放入字符串集合
            foreach (PropertyInfo item in properties)
            {
                columnList.Add(item.Name);
            }

            //返回结果
            return columnList;
        }

        /// <summary>
        /// 获取泛型类的属性值
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="t">泛型类实体</param>
        /// <param name="sourceColumn">属性名称</param>
        /// <returns></returns>
        public static object GetValueFromT<T>(T t, string sourceColumn) where T : class
        {
            object result = null;

            if (t != null)
            {
                Type type = t.GetType();

                if (type != null)
                {
                    PropertyInfo propertyInfo = type.GetProperty(sourceColumn);

                    if (propertyInfo != null)
                    {
                        if (propertyInfo.GetValue(t, null) != null)
                            result = propertyInfo.GetValue(t, null);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 为泛型类绑定值
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="info">泛型类对象</param>
        /// <param name="result">object值</param>
        /// <param name="resultColumn">列名</param>
        /// <returns></returns>
        public static T BindT<T>(T info, object result, string resultColumn) where T : new()
        {
            Type type = typeof(T);
            var p = type.GetProperty(resultColumn);

            try
            {
                if (p.PropertyType == typeof(string))
                    p.SetValue(info, result, null);
                else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(Nullable<int>))
                    p.SetValue(info, int.Parse(result.ToString()), null);
                else if (p.PropertyType == typeof(bool))
                    p.SetValue(info, bool.Parse(result.ToString()), null);
                else if (p.PropertyType == typeof(Nullable<System.DateTime>) || p.PropertyType == typeof(DateTime))
                    p.SetValue(info, DateTime.Parse(result.ToString()), null);
                else if (p.PropertyType == typeof(float) || p.PropertyType == typeof(Nullable<float>))
                    p.SetValue(info, float.Parse(result.ToString()), null);
                else if (p.PropertyType == typeof(double) || p.PropertyType == typeof(Nullable<double>))
                    p.SetValue(info, double.Parse(result.ToString()), null);
                else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(Nullable<decimal>))
                    p.SetValue(info, decimal.Parse(result.ToString()), null);
                else
                    p.SetValue(info, result, null);
            }
            catch
            {
                return info;
            }

            return info;
        }
    }
}
