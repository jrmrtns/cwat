using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    ///
    /// </summary>
    public static class OrderByExtension
    {
        private static Func<T, TReturnType> GetLambda<T, TReturnType>(IEnumerable<string> propertyNames)
        {
            var rootParameterExression = Expression.Parameter(typeof(T));
            Expression expression = propertyNames.Aggregate<string, Expression>(rootParameterExression, Expression.Property);
            return Expression.Lambda<Func<T, TReturnType>>(expression, rootParameterExression).Compile();
        }

        /// <summary>
        /// Orders by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="propertyPath">The property path.</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyPath)
        {
            var propertyPathList = propertyPath.Split(Convert.ToChar("."));
            Type propertyType = typeof(T);
            foreach (var propertyName in propertyPathList)
            {
                propertyType = propertyType.GetProperty(propertyName).PropertyType;
            }

            if (propertyType == typeof(decimal))
            {
                var lambda = GetLambda<T, Decimal>(propertyPathList);
                return queryable.OrderBy(lambda);
            }
            var lamda = GetLambda<T, object>(propertyPathList);
            return queryable.OrderBy(lamda);
        }
    }
}