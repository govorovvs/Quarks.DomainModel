using JetBrains.Annotations;
using Quarks.DomainModel.EventSourcing;
using Quarks.DomainModel.Tests.EventSourcing.Domain.EntityEvents;

namespace Quarks.DomainModel.Tests.EventSourcing.Domain
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Ship : EventSourced, IEntity, IAggregate
    {
        public Port Port { get; private set; }

        public string Name { get; private set; }

        public void ArriveTo(Port port)
        {
            var evnt = new ArrivalEvent(port);
            ConsumeWithTracking(evnt);
        }

        public void Rename(string name)
        {
            var evnt = new RenameEvent(name);
            ConsumeWithTracking(evnt);
        }

        protected override void ConsumeWithNoTracking(IEntityEvent entityEvent)
        {
            DoConsume((dynamic)entityEvent);
        }

        private void DoConsume(ArrivalEvent evnt)
        {
            Port = evnt.Port;
        }

        private void DoConsume(RenameEvent evnt)
        {
            Name = evnt.Name;
        }
    }
}