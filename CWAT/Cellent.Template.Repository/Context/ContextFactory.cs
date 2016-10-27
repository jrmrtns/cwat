using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces;
using System;

namespace Cellent.Template.Repository.Context
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="IContextFactory" />
    public class ContextFactory : IContextFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public BaseContext Create()
        {
            return new CellentContext();
        }
    }
}