using System;
using System.IdentityModel.Policy;
using System.ServiceModel;
using Cellent.Template.WCF.Authorization;
using Microsoft.Practices.Unity;

namespace Cellent.Template.WCF.Behaviors
{
    /// <summary>
    /// ServiceHost, der Unity verwendet
    /// </summary>
    public class UnityServiceHost : ServiceHost
    {
        private readonly IUnityContainer _container;
        private readonly Type _serviceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityServiceHost"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <exception cref="ArgumentNullException">container</exception>
        public UnityServiceHost(IUnityContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
            _serviceType = serviceType;

            if (Authorization.ExternalAuthorizationPolicies != null)
            {
                foreach (IAuthorizationPolicy item in Authorization.ExternalAuthorizationPolicies)
                {
                    AuthorizationPolicy policy = item as AuthorizationPolicy;
                    if (policy != null)
                        policy.Container = _container;
                }
            }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public Type ServiceType
        {
            get { return _serviceType; }
        }

        /// <summary>
        /// Invoked during the transition of a communication object into the opening state.
        /// </summary>
        protected override void OnOpening()
        {
            if (Description.Behaviors.Find<UnityServiceBehavior>() == null)
                Description.Behaviors.Add(new UnityServiceBehavior(Container, ServiceType));

            base.OnOpening();
        }
    }
}
