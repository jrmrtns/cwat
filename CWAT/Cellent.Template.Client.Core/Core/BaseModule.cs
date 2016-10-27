using System;
using System.Linq;
using System.Reflection;
using Cellent.Template.Client.Core.Interfaces.Factories;
using Cellent.Template.Common.Interfaces.Core;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Basisklasse für ClientModule
    /// </summary>
    public class BaseModule : IModule
    {
        private readonly IUnityContainer _container;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public BaseModule(IUnityContainer container)
        {
            _container = container;
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
        /// Notifies the module that it has be initialized.
        /// </summary>
        public virtual void Initialize()
        {
            Assembly assembly = Assembly.GetAssembly(GetType());
            Type[] types = assembly.GetTypes();

            Type[] models = types.Where(d => d.GetInterfaces()
                .Any(i => i == typeof(IBaseModel))).ToArray();

            Type[] factories = types.Where(d => d.GetInterfaces()
                .Any(
                    i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IModelFactory<,>)))
                .ToArray();

            Type[] mapper = types
                .Where(
                    d => d.BaseType != null &&
                    d.BaseType.IsGenericType &&
                    d.BaseType.GetGenericTypeDefinition() == typeof(GenericFactory<,>))
                .ToArray();

            foreach (Type type in mapper)
            {
                if (type.BaseType != null)
                {
                    Type baseType = type.BaseType;
                    Container.RegisterType(baseType, type);
                }
            }

            foreach (Type type in factories)
            {
                if (type.BaseType != null)
                    Container.RegisterType(type.GetInterfaces().First(d => d.IsGenericType &&
                    d.GetGenericTypeDefinition() == typeof(IModelFactory<,>)), type);
            }

            foreach (Type type in models)
            {
                if (type.BaseType != null)
                    Container.RegisterType(type.BaseType, type);
            }
        }
    }
}
