
using System;
using System.Runtime.Serialization;

namespace HomeRunner.Foundation.ExceptionManagement
{
	public class DataEntityNotFoundException
		: BusinessException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataEntityNotFoundException"/> class.
		/// </summary>
		public DataEntityNotFoundException() : base() { }

		/// <summary>
		///  Initializes a new instance of the <see cref="DataEntityNotFoundException"/> class with a specified error message.
		///  </summary>
		/// <param name="message">The message that describes the error.</param>
		public DataEntityNotFoundException(string message)
			: base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="DataEntityNotFoundException"/> class with a specified
		/// error message and a reference to the inner exception that is the cause of
		/// this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public DataEntityNotFoundException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="DataEntityNotFoundException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
		protected DataEntityNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}

