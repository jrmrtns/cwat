using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Repository.Context;
using Microsoft.Practices.Unity;

namespace Cellent.Template.Repository
{
    /// <summary>
    /// Module für Cellent.Template.Repository
    /// </summary>
    public class Module : BaseModule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public Module(IUnityContainer container) : base(container)
        { }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Container.RegisterType<IContextFactory, ContextFactory>();
        }

        #endregion Public Methods
    }
}