using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Quarks.DomainModel.Impl;

namespace Quarks.DomainModel.Tests.Impl
{
	[TestFixture]
	public class UnitOfWorkTests
	{
		private CancellationToken _cancellationToken;
		private UnitOfWork _unitOfWork;

		[SetUp]
		public void SetUp()
		{
			_unitOfWork = new UnitOfWork();
			_cancellationToken = new CancellationToken();
		}

		[TearDown]
		public void TearDown()
		{
			if (_unitOfWork != null)
			{
				_unitOfWork.Dispose();
				_unitOfWork = null;
			}
		}

		[Test]
		public async Task Commits_Dependent_Unit_Of_Works_On_Commit()
		{
			var dependentUnitOfWork =
				new Mock<IDependentUnitOfWork>();
			dependentUnitOfWork
				.Setup(x => x.CommitAsync(_cancellationToken))
				.Returns(Task.CompletedTask);
			_unitOfWork.Enlist("key", dependentUnitOfWork.Object);

			await _unitOfWork.CommitAsync(_cancellationToken);

			dependentUnitOfWork.VerifyAll();
		}

		[Test]
		public void Disposes_Dependent_Unit_Of_Works_On_Dispose()
		{
			var dependentUnitOfWork =
				new Mock<IDependentUnitOfWork>();
			dependentUnitOfWork
				.Setup(x => x.Dispose());
			_unitOfWork.Enlist("key", dependentUnitOfWork.Object);

			_unitOfWork.Dispose();

			dependentUnitOfWork.VerifyAll();
		}

		[Test]
		public void Dispose_Catches_Dependent_Unit_Of_Work_Exception()
		{
			var dependentUnitOfWork =
				new Mock<IDependentUnitOfWork>();
			dependentUnitOfWork
				.Setup(x => x.Dispose())
				.Throws<Exception>();

			_unitOfWork.Enlist("key", dependentUnitOfWork.Object);

			Assert.DoesNotThrow(() => _unitOfWork.Dispose());
		}

		[Test]
		public void Creating_New_UnitOfWork_In_Scope_Of_Other_One_Throws_An_Exception()
		{
			Assert.That(() => new UnitOfWork(), Throws.TypeOf<InvalidOperationException>());
		}
	}
}