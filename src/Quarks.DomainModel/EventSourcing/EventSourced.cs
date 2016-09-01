﻿using System;
using System.Collections.Generic;

namespace Quarks.DomainModel.EventSourcing
{
    /// <summary>
    /// Represents an base class that records all its changes as events.
    /// </summary>
    public abstract class EventSourced : IEventSourced
    {
        private readonly List<IEntityEvent> _events = new List<IEntityEvent>();

        IEnumerable<IEntityEvent> IEventSourced.Events
        {
            get { return _events; }
        }

        void IEventSourced.ClearEvents()
        {
            _events.Clear();
        }

        void IEventSourced.Consume(IEntityEvent entityEvent)
        {
            if (entityEvent == null) throw new ArgumentNullException(nameof(entityEvent));

            ConsumeWithNoTracking(entityEvent);
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
        protected void ConsumeWithTracking(IEntityEvent entityEvent)
        {
            if (entityEvent == null) throw new ArgumentNullException(nameof(entityEvent));

            ConsumeWithNoTracking(entityEvent);
            _events.Add(entityEvent);
        }
    }
}