using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// Caching eines Eintrags
    /// </summary>
    public class CacheAttribute : HandlerAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheAttribute"/> class.
        /// </summary>
        public CacheAttribute()
        {
            TimeSpan = 30;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the parameters to ignore.
        /// </summary>
        /// <value>
        /// The parameters to ignore.
        /// </value>
        public string[] ParamsToIgnore { get; set; }

        /// <summary>
        /// Timespan in Minutes. The default Timespan is 15 Minutes
        /// </summary>
        public int TimeSpan { get; set; }

        /// <summary>
        /// Gets or sets the type to monitor. Every write-action to the database concerning this type resets the cached items.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type TypeToMonitor { get; set; }

        /// <summary>
        /// Derived classes implement this method. When called, it
        /// creates a new call handler as specified in the attribute
        /// configuration.
        /// </summary>
        /// <param name="container">The <see cref="T:Microsoft.Practices.Unity.IUnityContainer" /> to use when creating handlers,
        /// if necessary.</param>
        /// <returns>
        /// A new call handler object.
        /// </returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CachingInterceptor(container, ParamsToIgnore, Key, TimeSpan, TypeToMonitor);
        }
    }
}