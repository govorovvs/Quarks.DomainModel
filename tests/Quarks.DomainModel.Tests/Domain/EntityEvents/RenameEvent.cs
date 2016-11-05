using Quarks.DomainModel.Events;

namespace Quarks.DomainModel.Tests.Domain.EntityEvents
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