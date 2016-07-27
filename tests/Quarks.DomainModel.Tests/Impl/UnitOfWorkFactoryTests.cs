using NUnit.Framework;
using Quarks.DomainModel.Impl;

namespace Quarks.DomainModel.Tests.Impl
{
	[TestFixture]
	public class UnitOfWorkFactoryTests
	{
		private UnitOfWorkFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new UnitOfWorkFactory();
		}

		[Test]
		public void Creates_UnitOfWork()
		{
			using (IUnitOfWork unitOfWork = _factory.Create())
			{
				Assert.IsInstanceOf<UnitOfWork>(unitOfWork);
			}
		}
	}
}