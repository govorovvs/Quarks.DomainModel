using System;

namespace Quarks.DomainModel
{
	/// <summary>
	/// An operation offered as an interface that stands alone in the model, with no encapsulated state.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class ServiceAttribute : Attribute
	{
	}
}