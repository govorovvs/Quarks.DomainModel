using Quarks.DomainModel.EventSourcing;

namespace Quarks.DomainModel.Tests.EventSourcing.Domain.EntityEvents
{
    public class RenameEvent : EntityEvent
    {
        protected RenameEvent()
        {
        }

        public RenameEvent(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}