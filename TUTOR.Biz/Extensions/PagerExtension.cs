using TUTOR.Biz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TUTOR.Biz.Extensions
{
    public static class PagerExtension
    {
        /// <summary>
        /// 資料分頁功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">資料</param>
        /// <param name="pager">TODO PARAM</param>
        /// <returns></returns>
        [Obsolete]
        public static IQueryable<T> Page<T>(this IQueryable<T> data, ref DefaultPager pager)
        {
            return data
                    .Skip(pager.DisplayCount * (pager.PageIndex - 1))
                    .Take(pager.DisplayCount);
        }

        /// <summary>
        /// 資料分頁功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">資料</param>
        /// <param name="pager">TODO PARAM</param>
        /// <returns></returns>
        [Obsolete]
        public static IEnumerable<T> Page<T>(this IEnumerable<T> data, ref DefaultPager pager)
        {
            return data
                    .Skip(pager.DisplayCount * (pager.PageIndex - 1))
                    .Take(pager.DisplayCount);
        }

        /// <summary>
        /// 資料分頁功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">資料</param>
        /// <param name="pager">TODO PARAM</param>
        /// <returns></returns>
        [Obsolete]
        public static List<T> Page<T>(this List<T> data, ref DefaultPager pager)
        {
            return data
                    .Skip(pager.DisplayCount * (pager.PageIndex - 1))
                    .Take(pager.DisplayCount).ToList();
        }

        /// <summary>
        /// 資料排序功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="orderType">排序方式(asc|desc)</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> data, string columnName, string orderType)
        {
            columnName = string.IsNullOrEmpty(columnName) ? "Id" : columnName;
            orderType = string.IsNullOrEmpty(orderType) ? "desc" : orderType;

            //這行可忽略大小寫
            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
            var info = typeof(T).GetProperty(columnName, flag);
            var param = Expression.Parameter(typeof(T));

            if (info.PropertyType == typeof(int))
            {
                var propExpression = Expression.Lambda<Func<T, int>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
            else if (info.PropertyType == typeof(decimal))
            {
                var propExpression = Expression.Lambda<Func<T, decimal>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
            else if (info.PropertyType == typeof(DateTime))
            {
                var propExpression = Expression.Lambda<Func<T, DateTime>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
            else if (info.PropertyType == typeof(DateTime?))
            {
                var propExpression = Expression.Lambda<Func<T, DateTime?>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
            else if (info.PropertyType == typeof(bool))
            {
                var propExpression = Expression.Lambda<Func<T, bool>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
            else
            {
                var propExpression = Expression.Lambda<Func<T, object>>(Expression.Property(param, columnName), param);

                return data.Order(orderType, propExpression);
            }
        }

        private static IOrderedQueryable<T1> Order<T1, T2>(this IQueryable<T1> data, string orderType, Expression<Func<T1, T2>> propExpression)
        {
            if (orderType?.ToLower() == "desc")
            {
                if (data.Expression.Type == typeof(IOrderedQueryable<T1>))
                {
                    return ((IOrderedQueryable<T1>)data).ThenByDescending(propExpression);
                }
                else
                {
                    return data.OrderByDescending(propExpression);
                }
            }
            else
            {
                if (data.Expression.Type == typeof(IOrderedQueryable<T1>))
                {
                    return ((IOrderedQueryable<T1>)data).ThenBy(propExpression);
                }
                else
                {
                    return data.OrderBy(propExpression);
                }
            }
        }

        [Obsolete]
        public static int TotalPage(this DefaultPager pager, int count)
        {
            if (count % pager.DisplayCount > 0)
            {
                return (count / pager.DisplayCount) + 1;
            }
            else
            {
                return count / pager.DisplayCount;
            }
        }
    }
}