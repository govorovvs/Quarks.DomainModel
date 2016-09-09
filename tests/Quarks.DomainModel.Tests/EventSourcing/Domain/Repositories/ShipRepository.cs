using System;
using Quarks.DomainModel.Building;
using Quarks.DomainModel.EventSourcing;

namespace Quarks.DomainModel.Tests.EventSourcing.Domain.Repositories
{
    public class ShipRepository : IRepository<Ship>
    {
        private readonly ShipEventRepository _eventRepository;

        public ShipRepository(ShipEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Ship Find()
        {
            return Find(DateTime.UtcNow);
        }

        public Ship Find(DateTime time)
        {
            var ship = new Builder<Ship>().Create();
            var events = _eventRepository.Find(time);
            ((IEventSourced)ship).Consume(events);
            return ship;
        }

        public void Modify(Ship ship)
        {
            var sourced = (IEventSourced) ship;
            _eventRepository.Save(sourced.Events);
            sourced.ClearEvents();
        }
    }
}