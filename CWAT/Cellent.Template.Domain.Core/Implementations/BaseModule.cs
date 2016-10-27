using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Common.Interfaces.Modularity;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Core.Interfaces.Mapper;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cellent.Template.Domain.Core.Implementations
{
    /// <summary>
    /// Basismodul für die Serverkomponente
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
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            Assembly assembly = Assembly.GetAssembly(GetType());
            Type[] types = assembly.GetTypes();

            Type[] factories = types.Where(d => d.GetInterfaces()
                .Any(
                    i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IDomainFactory<>)))
                .ToArray();

            Type[] mapper = types
                .Where(
                    d => d.BaseType != null &&
                    d.BaseType.IsGenericType &&
                    d.BaseType.GetGenericTypeDefinition() == typeof(GenericDomainMapper<,>))
                .ToArray();

            Type[] genericDaoMapper = types.Where(d => d.GetInterfaces()
                .Any(
                    i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IGenericDaoMapper<,>)))
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
                {
                    Type baseType = type.GetInterfaces().First(d => d.IsGenericType &&
                                                                    d.GetGenericTypeDefinition() ==
                                                                    typeof(IDomainFactory<>));

                    Type repositoryType = typeof(IBaseRepository<>);
                    repositoryType = repositoryType.MakeGenericType(baseType.GenericTypeArguments[0]);

                    Container.RegisterType(baseType, type, new InjectionConstructor(
                        new ResolvedParameter(repositoryType),
                        new ResolvedParameter<IUnityContainer>(),
                        new ResolvedParameter<IEventAggregator>()));
                    Container.Configure<Interception>()
                        .SetInterceptorFor(baseType, new InterfaceInterceptor());
                }
            }

            foreach (Type type in genericDaoMapper)
            {
                if (type.BaseType == null)
                    continue;

                Type baseType = type.BaseType;
                Container.RegisterType(baseType, type);
            }

            RegisterDefaultRepositories();
        }

        #region Private Methods

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(Assembly.GetEntryAssembly());

            do
            {
                var asm = stack.Pop();

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                    if (!list.Contains(reference.FullName) && reference.FullName.StartsWith("Cellent.Template"))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }
            }
            while (stack.Count > 0);
        }

        private void RegisterDefaultRepositories()
        {
            Assembly assembly = Assembly.GetAssembly(GetType());

            Assembly[] assemblies = GetAssemblies().ToArray();

            Type[] daos = assemblies.SelectMany(d => d.GetTypes())
                .Where(d => d.IsInterface && d.GetInterfaces()
                .Any(i => i == typeof(IBaseEntity))).ToArray();

            Type[] types = assembly.GetTypes();

            Type[] repositories = assembly.GetTypes()
                .Where(
                    d => d.Name != "DefaultRepository`2" &&
                         d.BaseType != null &&
                         d.BaseType.IsGenericType &&
                         d.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<,>))
                .ToArray();

            Type[] entities = types.Where(d => d.GetInterfaces()
                .Any(i => i == typeof(IBaseDao))).ToArray();

            foreach (Type type in entities)
            {
                string baseEntityName = "I" + type.Name.Replace("Dao", "");

                Type baseEntity = daos.FirstOrDefault(d => d.Name == baseEntityName);
                if (baseEntity == null)
                    continue;

                Type repository = typeof(IBaseRepository<>);
                repository = repository.MakeGenericType(baseEntity);

                if (Container.IsRegistered(repository))
                    continue;

                Type repo = repositories
                        .FirstOrDefault(d => d.GetInterfaces()
                        .Any(e => e.IsGenericType && e.GenericTypeArguments.Contains(baseEntity)));

                if (repo == null)
                {
                    repo = typeof(DefaultRepository<,>);
                    repo = repo.MakeGenericType(baseEntity, type);
                }

                Container.RegisterType(repository, repo);
                Container.Configure<Interception>()
                    .SetInterceptorFor(repo, new VirtualMethodInterceptor());

                var all = repo.GetInterfaces();
                var baseInterface = all.Except(all.SelectMany(t => t.GetInterfaces())).FirstOrDefault(d => !d.IsGenericType);
                if (baseInterface != null)
                {
                    Container.RegisterType(baseInterface, repo);
                }
            }
        }

        #endregion Private Methods
    }
}