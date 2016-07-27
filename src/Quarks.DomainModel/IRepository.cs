namespace Quarks.DomainModel
{
	public interface IRepository
	{
	}

	public interface IRepository<TEntity> where TEntity : IEntity
	{
	}
}