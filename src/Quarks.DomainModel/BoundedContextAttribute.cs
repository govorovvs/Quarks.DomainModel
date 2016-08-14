using System;

namespace Quarks.DomainModel
{
	/// <summary>
	/// The delimited applicability of a particular model.
	/// </summary>
	/// <see href="http://martinfowler.com/bliki/BoundedContext.html"/>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class BoundedContextAttribute : Attribute
	{
		/// <summary>
		/// Initialize a new instance of <see cref="BoundedContextAttribute"/> class with name.
		/// </summary>
		/// <param name="name">The name of bounded context.</param>
		public BoundedContextAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

			Name = name;
		}

		/// <summary>
		/// The name of bounded context.
		/// </summary>
		public string Name { get; }
	}
}