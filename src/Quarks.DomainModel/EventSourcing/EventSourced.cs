using System;
using System.Collections.Generic;
using System.Linq;

namespace Quarks.DomainModel.EventSourcing
{
    /// <summary>
    /// Represents an base class that records all its changes as events.
    /// </summary>
    /// <typeparam name="TEventType">Type of events.</typeparam>
    [Obsolete("Use Agregate instead")]
    public abstract class EventSourced<TEventType> : IEventSourced<TEventType> where TEventType : IEntityEvent
    {
        private readonly List<TEventType> _events = new List<TEventType>();

        IEnumerable<TEventType> IEventSourced<TEventType>.Events
        {
            get { return _events; }
        }

        void IEventSourced<TEventType>.ClearEvents()
        {
            _events.Clear();
        }

        void IEventSourced<TEventType>.Consume(IEnumerable<TEventType> entityEvents)
        {
            if (entityEvents == null) throw new ArgumentNullException(nameof(entityEvents));
            if (entityEvents.Any(x => x == null)) throw new ArgumentException("Some of entity event is null", nameof(entityEvents));

            foreach (TEventType entityEvent in entityEvents)
            {
                ConsumeWithNoTracking(entityEvent);
            }
        }

        /// <summary>
        /// Applies the specified event to the entity.
        /// </summary>
        /// <param name="entityEvent">Event should be applied.</param>
        protected abstract void ConsumeWithNoTracking(IEntityEvent entityEvent);

        /// <summary>
        /// Applies the specified event to the entity and track it.
        /// </summary>
        /// <param name="entityEvent">Event should be applied.</param>
        protected void ConsumeWithTracking(TEventType entityEvent)
        {
            if (entityEvent == null) throw new ArgumentNullException(nameof(entityEvent));

            ConsumeWithNoTracking(entityEvent);
            _events.Add(entityEvent);
        }
    }

    /// <summary>
    /// Represents an base class that records all its changes as events.
    /// </summary>
    public abstract class EventSourced : EventSourced<IEntityEvent>, IEventSourced
    {
    }
}