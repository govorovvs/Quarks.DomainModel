using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quarks.DomainModel.Impl
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private static readonly AsyncLocal<UnitOfWork> _current;

		static UnitOfWork()
		{
			_current = new AsyncLocal<UnitOfWork>();
		}

		public UnitOfWork()
		{
			if (Current != null)
			{
				throw new InvalidOperationException("Nested unit of works aren't supported");
			}

			DependentUnitOfWorks = new ConcurrentDictionary<string, IDependentUnitOfWork>();
			Current = this;
		}

		public IDictionary<string, IDependentUnitOfWork> DependentUnitOfWorks { get; }

		public void Dispose()
		{
			Current = null;

			foreach (var dependentUnitOfWork in DependentUnitOfWorks.Values)
				try
				{
					dependentUnitOfWork.Dispose();
				}
				catch
				{
				}
		}

		public async Task CommitAsync(CancellationToken cancellationToken)
		{
			foreach (IDependentUnitOfWork dependentUnitOfWork in DependentUnitOfWorks.Values)
			{
				await dependentUnitOfWork.CommitAsync(cancellationToken);
			}
		}

		public static UnitOfWork Current
		{
			get { return _current.Value; }
			private set { _current.Value = value; }
		}

		public void Enlist(string key, IDependentUnitOfWork dependentUnitOfWork)
		{
			if (dependentUnitOfWork == null)
				throw new ArgumentNullException(nameof(dependentUnitOfWork));

			DependentUnitOfWorks.Add(key, dependentUnitOfWork);
		}
	}
}