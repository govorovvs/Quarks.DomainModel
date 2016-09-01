using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel.Events
{
    /// <summary>
    /// Captures something interesting which affects the domain.
    /// </summary>
    /// <see href="http://martinfowler.com/eaaDev/DomainEvent.html"/>
    public static class DomainEvents
    {
        /// <summary>
        /// Instance of domain event dispatcher.
        /// </summary>
        public static IDomainEventDispatcher Dispatcher { get; set; }

        /// <summary>
        /// Rises the specified domain event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="evnt">The instance of the domain event.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous rise operation.</returns>
        public static Task RiseAsync<TEvent>(TEvent evnt, CancellationToken cancellationToken)
        {
            if (evnt == null) throw new ArgumentNullException(nameof(evnt));

            if (Dispatcher == null)
                return Task.FromResult(0);

            return Dispatcher.DispatchAsync(evnt, cancellationToken);
        }
    }
}