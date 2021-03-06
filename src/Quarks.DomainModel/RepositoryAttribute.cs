using System;
using System.Linq;
using System.Reflection;

namespace Quarks.DomainModel
{
	/// <summary>
	/// A mechanism for encapsulating storage, retrieval, and search behavior which emulates a collection of objects.
	/// </summary>
	/// <see href="http://martinfowler.com/bliki/ValueObject.html"/>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class RepositoryAttribute : Attribute
	{
		/// <summary>
		/// Initialize a new instance of <see cref="RepositoryAttribute"/> class with entity type.
		/// </summary>
		/// <param name="entityType">Type of entity.</param>
		public RepositoryAttribute(Type entityType)
		{
			if (!IsEntityType(entityType))
				throw new ArgumentException("Only entity type can be used as repository's type", nameof(entityType));
			if (!IsAggregate(entityType))
				throw new ArgumentException("Only aggregate type can be used as repository's type", nameof(entityType));

			EntityType = entityType;
		}

		/// <summary>
		/// Type of entity.
		/// </summary>
		public Type EntityType { get; }

		private bool IsEntityType(Type type)
		{
			return type.GetTypeInfo().CustomAttributes.Any(t => t.AttributeType == typeof(EntityAttribute)) ||
			       typeof(IEntity).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
		}

		private bool IsAggregate(Type type)
		{
			return type.GetTypeInfo().CustomAttributes.Any(t => t.AttributeType == typeof(AggregateAttribute)) ||
				   typeof(IAggregate).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
		}
	}
}