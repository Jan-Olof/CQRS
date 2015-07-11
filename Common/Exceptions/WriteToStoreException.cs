namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The write to store exception.
    /// </summary>
    public class WriteToStoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToStoreException"/> class.
        /// </summary>
        public WriteToStoreException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToStoreException"/> class.
        /// </summary>
        public WriteToStoreException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToStoreException"/> class.
        /// </summary>
        public WriteToStoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}