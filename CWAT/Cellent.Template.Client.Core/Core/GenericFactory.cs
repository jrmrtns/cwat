using System.Collections.Generic;
using System.Linq;
using Cellent.Template.Client.Core.Interfaces.Factories;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interfaces.Core;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Generischer Mapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public abstract class GenericFactory<T, TK> : IModelFactory<T, TK>
        where TK : BaseDto
        where T : IBaseModel
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public abstract T Create();

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="withChangeNotification">if set to <c>true</c> the item is created with change notification.</param>
        /// <returns>
        /// new Instance of T
        /// </returns>
        public abstract T Create(bool withChangeNotification);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public abstract TK Convert(T source);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public abstract T Convert(TK source);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public IEnumerable<T> Convert(IEnumerable<TK> source)
        {
            return source == null ? null : source.Select(Convert).ToList();
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public IEnumerable<TK> Convert(IEnumerable<T> source)
        {
            return source == null ? null : source.Select(Convert).ToList();
        }
    }
}