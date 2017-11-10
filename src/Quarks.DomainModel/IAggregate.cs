using System.Collections.Generic;
using Quarks.DomainModel.Events;

namespace Quarks.DomainModel
{
    /// <summary>
	/// Aggregate is a cluster of domain objects that can be treated as a single unit. 
	/// </summary>
	/// <see href="http://martinfowler.com/bliki/DDD_Aggregate.html"/>
	public interface IAggregate : IEntity
	{
	}

    /// <summary>
    /// Aggregate is a cluster of domain objects that can be treated as a single unit. 
    /// At any time, the aggregate contains the list of occurred events. 
    /// </summary>
    /// <typeparam name="TEvent">Type of events.</typeparam>
    /// <see href="http://martinfowler.com/bliki/DDD_Aggregate.html"/>
    /// <see href="https://msdn.microsoft.com/magazine/mt767692"/>
    public interface IAggregate<TEvent> : IAggregate where TEvent : IEntityEvent
    {
        /// <summary>
        /// Collection of events in the sequence they were applied to the aggregate.
        /// </summary>
        IEnumerable<TEvent> OccurredEvents { get; }
        
        /// <summary>
        /// Applies the specified event to the aggregate.
        /// </summary>
        /// <param name="evnt">Event should be applied.</param>
        void ApplyEvent(TEvent evnt);
    }
}