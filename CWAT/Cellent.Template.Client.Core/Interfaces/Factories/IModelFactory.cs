using System.Collections.Generic;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interfaces.Core;

namespace Cellent.Template.Client.Core.Interfaces.Factories
{
    /// <summary>
    /// Basisfactory für Models
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK"></typeparam>
    public interface IModelFactory<T, TK>
        where T : IBaseModel
        where TK : BaseDto
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>new Instance of T</returns>
        T Create();

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="withChangeNotification">if set to <c>true</c> the item is created with change notification.</param>
        /// <returns>
        /// new Instance of T
        /// </returns>
        T Create(bool withChangeNotification);

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
