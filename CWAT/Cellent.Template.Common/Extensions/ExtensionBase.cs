using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cellent.Template.Common.Extensions
{
    /// <summary>
    /// Extension, die für alle Objekte den Namen einer Eigenschaft/Methode als String zurückliefert
    /// </summary>
    public static class ExtensionBase
    {
        /// <summary>
        /// Den Member der aufgelöst werden soll
        /// <example>NotifyPropertyChanged(this.Member(c => c.Cultures));</example>
        /// <code>NotifyPropertyChanged(this.Member(c => c.Cultures));</code>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static string Member<TSource, TKey>(this TSource source, Expression<Func<TSource, TKey>> keySelector)
        {
            return ((MemberExpression)keySelector.Body).Member.Name;
        }

        /// <summary>
        /// Distincts the by.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
