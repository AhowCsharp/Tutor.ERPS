using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>判斷是否為Null或空列舉</summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return !(items?.Any() == true);
        }
    }
}