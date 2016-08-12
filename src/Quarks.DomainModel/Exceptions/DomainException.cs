using System;

namespace Quarks.DomainModel.Exceptions
{
	/// <summary>
	/// Base class for all exceptions in domain.
	/// </summary>
	public class DomainException : Exception
	{
		/// <summary>
		/// Initialize a new instance of <see cref="DomainException"/>.
		/// </summary>
		public DomainException()
		{
		}

		/// <summary>
		/// Initialize a new instance of <see cref="DomainException"/> with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes an error.</param>
		public DomainException(string message) 
			: base(message)
		{
		}

		/// <summary>
		/// Initialize a new instance of <see cref="DomainException"/> with a specified error message
		/// and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes an error.</param>
		/// <param name="innerException">The exception that is the cause of the exception or null if no inner exception is specified.</param>
		public DomainException(string message, Exception innerException) 
			: base(message, innerException)
		{
		}
	}
}