using System.Collections.Generic;
using System.Linq;

namespace CCWOnline.COC.Common
{
    /// <summary>
    /// 集合扩展方法类
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 泛型集合是否为空
        /// </summary>
        /// <typeparam name="T">泛型集合实体Type</typeparam>
        /// <param name="source">泛型集合对象</param>
        /// <returns>是否为空</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true;

            return !source.Any();
        }

        /// <summary>
        /// 泛型集合是否不为空
        /// </summary>
        /// <typeparam name="T">泛型集合实体Type</typeparam>
        /// <param name="source">泛型集合对象</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return false;

            return source.Any();
        }
    }
}
