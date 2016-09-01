using System.Collections.Generic;

namespace Quarks.DomainModel.EventSourcing
{
    /// <summary>
    /// Represents an entity that records all its changes as events 
    /// and uses them to materialize its state by effectively “playing back” 
    /// and consuming all the events related to that entity.
    /// </summary>
    public interface IEventSourced
    {
        /// <summary>
        /// Applies the specified event to the entity.
        /// </summary>
        /// <param name="entityEvent">Event should be applied.</param>
        void Consume(IEntityEvent entityEvent);

        /// <summary>
        /// Collection of events in the sequence they were applied for the same lifetime as the entity state itself.
        /// </summary>
        IEnumerable<IEntityEvent> Events { get; }

        /// <summary>
        /// Clears all events.
        /// </summary>
        void ClearEvents();
    }
}