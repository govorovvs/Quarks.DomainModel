using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel.Events
{
    /// <summary>
    /// Marker interface for domain event handlers.
    /// </summary>
    public interface IDomainEventHandler
    {
    }

    /// <summary>
    /// Represents a handler for a domain event.
    /// </summary>
    /// <typeparam name="TEvent">Type of domain event this handler handles.</typeparam>
    public interface IDomainEventHandler<in TEvent> : IDomainEventHandler where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="evnt">The instance of the domain event.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous handle operation.</returns>
        Task HandleAsync(TEvent evnt, CancellationToken cancellationToken);
    }
}