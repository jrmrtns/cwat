using System;
using Prism.Events;

namespace Cellent.Template.Client.Core.Events
{
    /// <summary>
    /// wird gesendet, wenn der User informiert werden soll
    /// </summary>
    public class NotifyUserPubSubEvent : PubSubEvent<Exception>
    {
    }
}
