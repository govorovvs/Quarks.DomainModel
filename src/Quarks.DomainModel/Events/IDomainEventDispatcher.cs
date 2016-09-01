using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel.Events
{
    /// <summary>
    /// Represents a mediator that dispatches domain even to its handlers.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches domain even to its handlers.
        /// </summary>
        /// <typeparam name="TEvent">Type of dispatched domain event.</typeparam>
        /// <param name="evnt">Dispatched domain event instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous dispatch operation.</returns>
        Task DispatchAsync<TEvent>(TEvent evnt, CancellationToken cancellationToken);
    }
}