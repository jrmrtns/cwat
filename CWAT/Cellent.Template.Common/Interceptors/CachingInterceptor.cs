using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// Looks up a type by key in cache and returns if there
    /// </summary>
    public class CachingInterceptor : ICallHandler
    {
        #region Fields (5) 

        private readonly IUnityContainer _container;
        private readonly string _key;
        private readonly string[] _paramsToIgnore;
        private readonly int _timeSpan;
        private readonly Type _type;

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingInterceptor"/> class.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="paramsToIgnore"></param>
        /// <param name="key">The key.</param>
        /// <param name="timeSpan">Timespan in minutes</param>
        /// <param name="type"></param>
        public CachingInterceptor(IUnityContainer container, string[] paramsToIgnore, string key, int timeSpan, Type type)
        {
            _container = container;
            _paramsToIgnore = paramsToIgnore;
            _key = key;
            _timeSpan = timeSpan;
            _type = type;
        }

        #endregion Constructors 

        #region Properties (5) 

        /// <summary>
        /// Gets the IUnityContainer.
        /// </summary>
        /// <value>
        /// The IUnityContainer.
        /// </value>
        public IUnityContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets the parameters to ignore.
        /// </summary>
        /// <value>
        /// The parameters to ignore.
        /// </value>
        public string[] ParamsToIgnore
        {
            get { return _paramsToIgnore; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type
        {
            get { return _type; }
        }

        #endregion Properties 

        #region Methods (3) 

        #region Public Methods (1) 

        /// <summary>
        /// Implement this method to execute your handler processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler
        /// chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            ObjectCache cache = MemoryCache.Default;

            var key = GenerateKey(input);

            object item = cache.Get(key);
            if (item != null)
            {
                return input.CreateMethodReturn(item);
            }

            IMethodReturn result = getNext()(input, getNext);
            if (result.ReturnValue != null)
            {
                AddToCache(cache, key, result);
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods (2) 

        /// <summary>
        /// Adds the MethodResult to cache.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="result">The result.</param>
        private void AddToCache(ObjectCache cache, string key, IMethodReturn result)
        {
            CacheItemPolicy policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(_timeSpan) };
            if (Type != null)
            {
                Type t = typeof(EntityChangeMonitor<>);
                t = t.MakeGenericType(Type);
                ChangeMonitor changeMonitor = Container.Resolve(t) as ChangeMonitor;

                policy.ChangeMonitors.Add(changeMonitor);
            }
            cache.Add(key, result.ReturnValue, policy);
        }

        /// <summary>
        /// Generates the key for the cache.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="MissingCacheKey"></exception>
        private string GenerateKey(IMethodInvocation input)
        {
            string values = "";
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                string parameterName = input.Arguments.ParameterName(i);
                if (ParamsToIgnore != null && ParamsToIgnore.Contains(parameterName))
                    continue;

                object argument = input.Arguments[i];
                IBaseEntity entity = argument as IBaseEntity;
                if (entity == null)
                    values += "@@" + parameterName + "==" + argument;
                else
                    values += "@@" + parameterName + "==" + entity.Id;
            }

            if (string.IsNullOrEmpty(values))
                throw new MissingCacheKey();

            return string.Format("{0}@{1}", ((MethodInfo)input.MethodBase).ReturnType.FullName, Key ?? values);
        }

        #endregion Private Methods 

        #endregion Methods 
    }
}