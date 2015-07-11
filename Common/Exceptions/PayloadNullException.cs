namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The payload null exception.
    /// </summary>
    public class PayloadNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadNullException"/> class.
        /// </summary>
        public PayloadNullException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadNullException"/> class.
        /// </summary>
        public PayloadNullException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadNullException"/> class.
        /// </summary>
        public PayloadNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}