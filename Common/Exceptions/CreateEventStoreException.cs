namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The create event store exception.
    /// </summary>
    public class CreateEventStoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventStoreException"/> class.
        /// </summary>
        public CreateEventStoreException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventStoreException"/> class.
        /// </summary>
        public CreateEventStoreException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventStoreException"/> class.
        /// </summary>
        public CreateEventStoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}