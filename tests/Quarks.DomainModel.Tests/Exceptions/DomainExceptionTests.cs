using System;
using NUnit.Framework;
using Quarks.DomainModel.Exceptions;

namespace Quarks.DomainModel.Tests.Exceptions
{
	[TestFixture]
	public class DomainExceptionTests
	{
		[Test]
		public void Can_Be_Constructed_With_No_Parameters()
		{
			DomainException exception = new DomainException();

			Assert.That(exception, Is.Not.Null);
		}

		[Test]
		public void Can_Be_Constructed_With_Message()
		{
			const string message = "message";

			DomainException exception = new DomainException(message);

			Assert.That(exception.Message, Is.EqualTo(message));
		}

		[Test]
		public void Can_Be_Constructed_With_Message_And_InnerException()
		{
			const string message = "message";
			Exception innerException = new Exception();

			DomainException exception = new DomainException(message, innerException);

			Assert.That(exception.Message, Is.EqualTo(message));
			Assert.That(exception.InnerException, Is.EqualTo(innerException));
		}
	}
}