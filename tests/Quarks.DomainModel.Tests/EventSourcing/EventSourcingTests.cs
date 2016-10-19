using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Quarks.DomainModel.Building;
using Quarks.DomainModel.Events;
using Quarks.DomainModel.EventSourcing;
using Quarks.DomainModel.Tests.EventSourcing.Domain;
using Quarks.DomainModel.Tests.EventSourcing.Domain.EntityEvents;
using Quarks.DomainModel.Tests.EventSourcing.Domain.Repositories;

namespace Quarks.DomainModel.Tests.EventSourcing
{
    [TestFixture]
    public class EventSourcingTests
    {
        private ShipEventRepository _eventRepository;
        private ShipRepository _repository;
        private List<IEntityEvent> _persistedEvents;

        [SetUp]
        public void SetUp()
        {
            _eventRepository = new ShipEventRepository();
            _repository = new ShipRepository(_eventRepository);
            _persistedEvents = _eventRepository.Events;
        }

        [Test]
        public void Ship_Renaming_Is_Tracked_With_Event()
        {
            const string name = "name";
            Ship ship = new Builder<Ship>().Create();

            ship.Rename(name);

            Assert.That(ship.Name, Is.EqualTo(name));
            var evnt = (RenameEvent)((IEventSourced)ship).Events.First();
            Assert.That(evnt.Name, Is.EqualTo(name));
        }

        [Test]
        public void Ship_Consumes_RenameEvent()
        {
            const string name = "name";
            var evnt = new RenameEvent(name);
            var ship = new Builder<Ship>().With(x => x.Name, "oldName").Create();

            ((IEventSourced)ship).Consume(new[] { evnt});

            Assert.That(ship.Name, Is.EqualTo(name));
        }

        [Test]
        public void Ship_Arriving_Is_Tracked_With_Event()
        {
            Port port = new Builder<Port>().Create();
            Ship ship = new Builder<Ship>().Create();

            ship.ArriveTo(port);

            Assert.That(ship.Port, Is.EqualTo(port));
            var evnt = (ArrivalEvent)((IEventSourced)ship).Events.First();
            Assert.That(evnt.Port, Is.EqualTo(port));
        }

        [Test]
        public void Ship_Consumes_ArrivalEvent()
        {
            Port port = new Builder<Port>().Create();
            var evnt = new ArrivalEvent(port);
            var ship = new Builder<Ship>().With(x => x.Port, null).Create();

            ((IEventSourced)ship).Consume(new [] { evnt});

            Assert.That(ship.Port, Is.EqualTo(port));
        }

        [Test]
        public void Ship_Saving()
        {
            const string name = "name";
            Ship ship = new Builder<Ship>().Create();
            ship.Rename(name);

            _repository.Modify(ship);

            Assert.That(((IEventSourced) ship).Events, Is.Empty, "Entity persisting clears its events");
            var evnt = (RenameEvent)_eventRepository.Events.Single();
            Assert.That(evnt.Name, Is.EqualTo(name), "Entity persisting should save events");
        }

        [Test]
        public void Ship_Loading()
        {
            _persistedEvents.AddRange(
               new IEntityEvent[]
               {
                    new Builder<RenameEvent>()
                        .With(t => t.Occurred, DateTime.UtcNow.AddDays(-2))
                        .With(t => t.Name, "oldName")
                        .Create(),
                    new Builder<RenameEvent>()
                        .With(t => t.Occurred, DateTime.UtcNow.AddDays(-1))
                        .With(t => t.Name, "name")
                        .Create(),
               });

            var ship = _repository.Find();

            Assert.That(ship.Name, Is.EqualTo("name"));
        }

        [Test]
        public void Ship_Loading_For_Specified_Time()
        {
            var timeInThePast = DateTime.UtcNow.AddDays(-7);
            _persistedEvents.AddRange(
                new IEntityEvent[]
                {
                    new Builder<RenameEvent>()
                        .With(t => t.Occurred, timeInThePast.AddDays(-1))
                        .With(t => t.Name, "name")
                        .Create(),
                    new Builder<RenameEvent>()
                        .With(t => t.Occurred, timeInThePast.AddDays(1))
                        .With(t => t.Name, "newName")
                        .Create(),
                });

            var ship = _repository.Find(timeInThePast);

            Assert.That(ship.Name, Is.EqualTo("name"));
        }
    }
}