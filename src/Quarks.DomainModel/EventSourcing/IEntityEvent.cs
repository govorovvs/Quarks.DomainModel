using System;

namespace Quarks.DomainModel.EventSourcing
{
    /// <summary>
    /// Represents a change to the state of an entity.
    /// </summary>
    public interface IEntityEvent
    {
        /// <summary>
        /// The Guid of the event.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// The time the event occured.
        /// </summary>
        DateTime Occurred { get; }
    }
}