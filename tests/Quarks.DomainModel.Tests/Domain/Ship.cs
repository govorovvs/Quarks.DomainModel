using JetBrains.Annotations;
using Quarks.DomainModel.Events;
using Quarks.DomainModel.Tests.Domain.EntityEvents;

namespace Quarks.DomainModel.Tests.Domain
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Ship : Aggregate, IEntity
    {
        public Port Port { get; private set; }

        public string Name { get; private set; }

        public void ArriveTo(Port port)
        {
            var evnt = new ArrivalEvent(port);
            RiseEvent(evnt);
        }

        public void Rename(string name)
        {
            var evnt = new RenameEvent(name);
            RiseEvent(evnt);
        }

        protected override void ApplyEvent(IEntityEvent evnt)
        {
            ApplyEvent((dynamic)evnt);
        }

        private void ApplyEvent(ArrivalEvent evnt)
        {
            Port = evnt.Port;
        }

        private void ApplyEvent(RenameEvent evnt)
        {
            Name = evnt.Name;
        }
    }
}