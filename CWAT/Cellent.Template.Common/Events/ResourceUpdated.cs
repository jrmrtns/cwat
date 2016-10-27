using Cellent.Template.Common.Interfaces.Core;
using Prism.Events;
using System.Collections.Generic;

namespace Cellent.Template.Common.Events
{
    /// <summary>
    /// PubSubEvent für ein Update bei Entities
    /// </summary>
    public class EntityUpdated : PubSubEvent<IBaseEntity>
    {
    }

    /// <summary>
    /// PubSubEvent für ein Update bei Entities
    /// </summary>
    public class EntitiesUpdated : PubSubEvent<IEnumerable<IBaseEntity>>
    {
    }
}