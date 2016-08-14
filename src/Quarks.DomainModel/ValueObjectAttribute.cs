using System;

namespace Quarks.DomainModel
{
	/// <summary>
	/// An object that contains attributes but has no conceptual identity. They should be treated as immutable.
	/// </summary>
	/// <see href="http://martinfowler.com/bliki/ValueObject.html"/>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class ValueObjectAttribute : Attribute
	{
	}
}