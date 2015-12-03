
using System;
using System.Runtime.Serialization;

namespace HomeRunner.Foundation.ExceptionManagement
{
    /// <summary>
    /// The base class for ApplicationModel exceptions.
    /// </summary>
    public abstract class ApplicationException 
        : Exception
    {
        #region - Constructors -

        /// <summary>
        /// Initializes a new instance of the WrappedException class.
        /// </summary>
        protected ApplicationException()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class with a specified error message.
        /// </summary>
        /// <param name="message"> The message that describes the error.</param>
        protected ApplicationException(string message)
            : base(message)
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class with a specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        protected ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class with serialized
        /// data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Id = Guid.NewGuid();
        }

        #endregion

        /// <summary>
        /// The exception ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The exception number.
        /// </summary>
        public string Code { get { return String.Format("{0:X}", this.GetType().Name.GetHashCode()); } }
    }
}
