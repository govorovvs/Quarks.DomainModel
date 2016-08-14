using System;

namespace Quarks.DomainModel
{
	/// <summary>
	/// A mechanism for encapsulating complex creation logic and abstracting the type of a created object for the sake of a client.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class FactoryAttribute : Attribute
	{	
	}
}