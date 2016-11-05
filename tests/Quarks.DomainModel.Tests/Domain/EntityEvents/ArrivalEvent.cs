using Quarks.DomainModel.Events;

namespace Quarks.DomainModel.Tests.Domain.EntityEvents
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