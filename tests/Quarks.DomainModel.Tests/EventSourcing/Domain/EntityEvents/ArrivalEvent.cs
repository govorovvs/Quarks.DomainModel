using Quarks.DomainModel.Events;
using Quarks.DomainModel.EventSourcing;

namespace Quarks.DomainModel.Tests.EventSourcing.Domain.EntityEvents
{
    public class ArrivalEvent : EntityEvent
    {
        internal ArrivalEvent(Port port)
        {
            Port = port;
        }

        public Port Port { get; private set; }
    }
}