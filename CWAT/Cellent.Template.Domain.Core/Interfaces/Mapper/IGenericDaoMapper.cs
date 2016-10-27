using System.Collections.Generic;
using Cellent.Template.Domain.Core.Interfaces.Factories;

namespace Cellent.Template.Domain.Core.Interfaces.Mapper
{
    /// <summary>
    /// Interface für GenericDaoMapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public interface IGenericDaoMapper<T, TK>
    {
        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        IDomainFactory<T> Factory { get; }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TK Convert(T source);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        T Convert(TK source);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        IEnumerable<T> Convert(IEnumerable<TK> source);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        IEnumerable<TK> Convert(IEnumerable<T> source);
    }
}