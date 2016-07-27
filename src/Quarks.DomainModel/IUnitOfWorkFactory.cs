namespace Quarks.DomainModel
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
	}
}