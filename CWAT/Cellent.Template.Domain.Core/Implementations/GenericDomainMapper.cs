using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using System.Collections.Generic;
using System.Linq;

namespace Cellent.Template.Domain.Core.Implementations
{
    /// <summary>
    /// Generischer Mapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public abstract class GenericDomainMapper<T, TK> where TK : BaseDto where T : IEntity<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDomainMapper{T, TK}"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        protected GenericDomainMapper(IDomainFactory<T> factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public IDomainFactory<T> Factory { get; }

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