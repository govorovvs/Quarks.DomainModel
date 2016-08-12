namespace Quarks.DomainModel
{
	/// <summary>
	/// The delimited applicability of a particular model.
	/// </summary>
	/// <see href="http://martinfowler.com/bliki/BoundedContext.html"/>
	public interface IBoundedContext
	{
		/// <summary>
		/// The name of this bounded context.
		/// </summary>
		string Name { get; }
	}
}