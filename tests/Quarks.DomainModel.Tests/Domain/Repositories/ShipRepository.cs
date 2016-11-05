using System;
using Quarks.Runtime;

namespace Quarks.DomainModel.Tests.Domain.Repositories
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
            ship.ApplyEvents(events);
            return ship;
        }

        public void Modify(Ship ship)
        {
            _eventRepository.Save(ship.AsAggregate().OccurredEvents);
        }
    }
}