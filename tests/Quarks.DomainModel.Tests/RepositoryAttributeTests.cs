using System;
using NUnit.Framework;

namespace Quarks.DomainModel.Tests
{
	[TestFixture]
	public class RepositoryAttributeTests
	{
		[Test]
		public void Can_Be_Constructed_With_EntityType_Marker_With_Interface()
		{
			RepositoryAttribute attribute = new RepositoryAttribute(typeof(EntityMarkerWithInterface));

			Assert.That(attribute.EntityType, Is.EqualTo(typeof(EntityMarkerWithInterface)));
		}

		[Test]
		public void Can_Be_Constructed_With_EntityType_Marker_With_Attribute()
		{
			RepositoryAttribute attribute = new RepositoryAttribute(typeof(EntityMarkerWithAttribute));

			Assert.That(attribute.EntityType, Is.EqualTo(typeof(EntityMarkerWithAttribute)));
		}

		[Test]
		public void Construction_With_Non_Entity_Throws_An_Exception()
		{
			Assert.Throws<ArgumentException>(
				() => new RepositoryAttribute(typeof(NonEntity)));
		}

		public class EntityMarkerWithInterface : IEntity
		{
		}

		[Entity]
		public class EntityMarkerWithAttribute
		{
		}

		public class NonEntity
		{
		}
	}
}