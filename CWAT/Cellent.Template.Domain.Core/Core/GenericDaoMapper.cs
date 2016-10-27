using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Core.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    /// Generischer Mapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public abstract class GenericDaoMapper<T, TK> : IGenericDaoMapper<T, TK> where TK : IBaseDao where T : IEntity<T>
    {
        private readonly Lazy<IDomainFactory<T>> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDaoMapper{T,TK}"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        protected GenericDaoMapper(Lazy<IDomainFactory<T>> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public IDomainFactory<T> Factory
        {
            get { return _factory.Value; }
        }

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