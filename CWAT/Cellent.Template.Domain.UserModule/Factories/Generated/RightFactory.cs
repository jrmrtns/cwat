//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region T4 generated code

using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Domain.Core.Interceptors;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.UserModule.Entities;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Prism.Events;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cellent.Template.Domain.UserModule.Factories
{
    /// <summary>
    /// Factory für die Right
    /// </summary>
    class RightFactory : IDomainFactory<IRight>
    {
        #region Fields (1) 

        private readonly IBaseRepository<IRight> _rightRepository;
        private readonly IEventAggregator _eventAggregator;
		private readonly IUnityContainer _container;

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="RightFactory"/> class.
        /// </summary>
        /// <param name="rightRepository">The role repository.</param>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public RightFactory(IBaseRepository<IRight> rightRepository, IUnityContainer container, IEventAggregator eventAggregator)
        {
		    _container = container;
            _rightRepository = rightRepository;
            _eventAggregator = eventAggregator;
        }

        #endregion Constructors 

        #region Properties (1) 

        /// <summary>
        /// Gets the right repository.
        /// </summary>
        /// <value>
        /// The right repository.
        /// </value>
        public IBaseRepository<IRight> RightRepository
        {
            get { return _rightRepository; }
        }

		/// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
		public IUnityContainer Container
		{
		    get { return _container;}
		}

        #endregion Properties 

        #region Methods (1) 

        #region Public Methods (1) 

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public IRight Create()
        {
            return Intercept.NewInstanceWithAdditionalInterfaces<Right>(new VirtualMethodInterceptor(),
                new IInterceptionBehavior[]
                    {
                        //new LoggingBehavior(TraceEventType.Verbose),
                        new UnitOfWorkBehavior(),
                        new ValidationBehavior()
                    },
                new[] 
				{                     
                    typeof(IValidatable),
                    typeof(IUnitOfWork)
                },
                new object[]{_container, RightRepository, _eventAggregator});
        }

        #endregion Public Methods 

        #endregion Methods 
    }
}

#endregion
