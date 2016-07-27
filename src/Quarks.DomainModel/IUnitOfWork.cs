using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync(CancellationToken ct);
	}
}