using System.Collections.Generic;

namespace Quarks.DomainModel.EventSourcing
{
    /// <summary>
    /// Represents an entity that records all its changes as events 
    /// and uses them to materialize its state by effectively “playing back” 
    /// and consuming all the events related to that entity.
    /// </summary>
    /// <typeparam name="TEventType">Type of events.</typeparam>
    public interface IEventSourced<TEventType> where TEventType : IEntityEvent
    {
        /// <summary>
        /// Applies the specified collection of events to the entity.
        /// </summary>
        /// <param name="entityEvents">Collection of events should be applied.</param>
        void Consume(IEnumerable<TEventType> entityEvents);

        /// <summary>
        /// Collection of events in the sequence they were applied for the same lifetime as the entity state itself.
        /// </summary>
        IEnumerable<TEventType> Events { get; }

        /// <summary>
        /// Clears all events.
        /// </summary>
        void ClearEvents();
    }

    /// <summary>
    /// Represents an entity that records all its changes as events 
    /// and uses them to materialize its state by effectively “playing back” 
    /// and consuming all the events related to that entity.
    /// </summary>
    public interface IEventSourced : IEventSourced<IEntityEvent>
    { 
    }
}