using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Quarks.DomainModel.Events;

namespace Quarks.DomainModel.Tests.Events
{
    [TestFixture]
    public class DomainEventsTests
    {
        private CancellationToken _cancellationToken;
        private Mock<IDomainEventDispatcher> _mockDispatcher;

        [SetUp]
        public void SetUp()
        {
            _mockDispatcher = new Mock<IDomainEventDispatcher>();
            _cancellationToken = new CancellationTokenSource().Token;
        }

        [Test]
        public void RiseEvent_With_Null_One_Throws_An_Exception()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => DomainEvents.RiseAsync<DomainEvent>(null, _cancellationToken));
        }

        [Test]
        public void RiseEvent_Do_Nothig_If_No_Dispatcher_Is_Specified()
        {
            DomainEvents.Dispatcher = null;

            var evnt = new DomainEvent();
            var task = DomainEvents.RiseAsync(evnt, _cancellationToken);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.IsCompleted, Is.True);
        }

        [Test]
        public async Task RiseEvent_Dispatches_Event()
        {
            DomainEvents.Dispatcher = _mockDispatcher.Object;
            var evnt = new DomainEvent();

            _mockDispatcher
                .Setup(x => x.DispatchAsync(evnt, _cancellationToken))
                .Returns(Task.CompletedTask);

            await DomainEvents.RiseAsync(evnt, _cancellationToken);

            _mockDispatcher.VerifyAll();
        }
    }

    public class DomainEvent : IDomainEvent
    {
    }
}