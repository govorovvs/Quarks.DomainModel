namespace Quarks.DomainModel.Impl
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork Create()
		{
			return new UnitOfWork();
		}
	}
}