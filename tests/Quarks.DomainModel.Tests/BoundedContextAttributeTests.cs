using System;
using NUnit.Framework;

namespace Quarks.DomainModel.Tests
{
	[TestFixture]
	public class BoundedContextAttributeTests
	{
		[Test]
		public void Can_Be_Constructed_With_Name()
		{
			const string name = "name";

			BoundedContextAttribute attribute = new BoundedContextAttribute(name);

			Assert.That(attribute.Name, Is.EqualTo(name));
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void Construction_With_Invalid_Name_Throws_An_Exception(string name)
		{
			Assert.Throws<ArgumentNullException>(
				() => new BoundedContextAttribute(name));
		}
	}
}