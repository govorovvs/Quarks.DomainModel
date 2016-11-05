using System;

namespace Quarks.DomainModel.Events
{
    /// <summary>
    /// Represents a base class for entity events.
    /// </summary>
    public abstract class EntityEvent : IEntityEvent
    {
        /// <summary>
        /// Initialize a new instance of <see cref="EntityEvent"/> class.
        /// </summary>
        protected EntityEvent()
        {
            Guid = Guid.NewGuid();
            Occurred = DateTime.UtcNow;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="EntityEvent"/> class with Guid and occurr time.
        /// </summary>
        /// <param name="guid">The Guid of the event</param>
        /// <param name="occurred">The time the event occured.</param>
        protected EntityEvent(Guid guid, DateTime occurred)
        {
            Guid = guid;
            Occurred = occurred;
        }

        /// <summary>
        /// The Guid of the event.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// The time the event occured.
        /// </summary>
        public DateTime Occurred { get; private set; }
    }
}