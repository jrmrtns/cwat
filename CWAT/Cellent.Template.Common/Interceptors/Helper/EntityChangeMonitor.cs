using System;
using System.Runtime.Caching;
using Cellent.Template.Common.Events;
using Cellent.Template.Common.Interfaces.Core;
using Prism.Events;

namespace Cellent.Template.Common.Interceptors.Helper
{
    /// <summary>
    /// EntityChangeMonitor. Sobald ein "EntityUpdated" des Types gefeuert wird, werden die gecachten Einträge die mit diesen ChangeMonitor überwacht werden weggeworfen
    /// </summary>
    public class EntityChangeMonitor<T> : ChangeMonitor where T : IBaseEntity
    {
        #region Fields (3) 

        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _subscriptionToken;
        private readonly string _uniqueId;

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeMonitor{T}"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public EntityChangeMonitor(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _subscriptionToken = eventAggregator.GetEvent<EntityUpdated>().Subscribe(entity =>
                {
                    if (entity is T)
                        OnChanged(entity);
                });

            _uniqueId = Guid.NewGuid().ToString();
            InitializationComplete();
        }

        #endregion Constructors 

        #region Properties (2) 

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public override string UniqueId
        {
            get { return _uniqueId; }
        }

        #endregion Properties 

        #region Methods (1) 

        #region Protected Methods (1) 

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventAggregator.GetEvent<EntityUpdated>().Unsubscribe(_subscriptionToken);
                _subscriptionToken.Dispose();
            }
        }

        #endregion Protected Methods 

        #endregion Methods 
    }
}
