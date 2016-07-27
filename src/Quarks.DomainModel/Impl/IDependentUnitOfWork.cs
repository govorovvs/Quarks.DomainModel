using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel.Impl
{
	public interface IDependentUnitOfWork : IDisposable
	{
		Task CommitAsync(CancellationToken cancellationToken);
	}
}