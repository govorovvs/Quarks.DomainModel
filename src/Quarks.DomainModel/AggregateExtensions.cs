using System;
using System.Collections.Generic;
using Quarks.DomainModel.Events;

namespace Quarks.DomainModel
{
    /// <summary>
    /// Extensions for aggregates.
    /// </summary>
    public static class AggregateExtensions
    {
        /// <summary>
        /// Convert an aggregate object to <see cref="IAggregate{T}"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of events.</typeparam>
        /// <param name="aggregate">A aggregate to convert.</param>
        /// <returns>An <see cref="IAggregate{T}"/> representation of the source aggregate.</returns>
        public static IAggregate<TEvent> AsAggregate<TEvent>(this IAggregate<TEvent> aggregate)
            where TEvent : IEntityEvent
        {
            return aggregate;
        }

        /// <summary>
        /// Applies the specified events to the aggregate.
        /// </summary>
        /// <param name="aggregate">The aggregate events should be applied to.</param>
        /// <param name="events">Events should be applied.</param>
        public static void ApplyEvents<TEvent>(this IAggregate<TEvent> aggregate, IEnumerable<TEvent> events) 
            where TEvent : IEntityEvent
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (events == null) throw new ArgumentNullException(nameof(events));

            foreach (TEvent evnt in events)
            {
                aggregate.ApplyEvent(evnt);
            }
        }
    }
}