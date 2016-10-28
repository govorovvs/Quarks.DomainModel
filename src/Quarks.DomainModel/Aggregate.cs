using System;
using System.Collections.Generic;
using Quarks.DomainModel.Events;

namespace Quarks.DomainModel
{
    /// <summary>
	/// Aggregate is a cluster of domain objects that can be treated as a single unit. 
	/// </summary>
    public abstract class Aggregate : Aggregate<IEntityEvent>
    {
    }

    /// <summary>
	/// Aggregate is a cluster of domain objects that can be treated as a single unit. 
	/// </summary>
	/// <typeparam name="TEvent">Type of events.</typeparam>
    public abstract class Aggregate<TEvent> : IAggregate<TEvent> 
        where TEvent : IEntityEvent
    {
        private readonly List<TEvent> _events = new List<TEvent>();

        IEnumerable<TEvent> IAggregate<TEvent>.OccurredEvents => _events;

        void IAggregate<TEvent>.ApplyEvent(TEvent evnt)
        {
            ApplyEvent(evnt);
        }

        /// <summary>
        /// Applies the specified event to the aggregate.
        /// </summary>
        /// <param name="evnt">Event should be applied.</param>
        protected abstract void ApplyEvent(TEvent evnt);

        /// <summary>
        /// Applies the specified event to the aggregate and adds it to the internal list of events.
        /// </summary>
        /// <param name="evnt">Event should be rised.</param>
        protected virtual void RiseEvent(TEvent evnt)
        {
            if (evnt == null) throw new ArgumentNullException(nameof(evnt));

            ApplyEvent(evnt);
            _events.Add(evnt);
        }
    }
}