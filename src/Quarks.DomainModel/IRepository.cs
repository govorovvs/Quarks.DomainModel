namespace Quarks.DomainModel
{
	/// <summary>
	/// Marker interface.
	/// </summary>
	public interface IRepository
	{
	}

	/// <summary>
	/// A mechanism for encapsulating storage, retrieval, and search behavior which emulates a collection of objects.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IRepository<TEntity> where TEntity : IEntity, IAggregate
	{
	}
}