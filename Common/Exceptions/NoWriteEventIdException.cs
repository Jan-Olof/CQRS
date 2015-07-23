namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The NoWriteEventIdException.
    /// </summary>
    public class NoWriteEventIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoWriteEventIdException"/> class.
        /// </summary>
        public NoWriteEventIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoWriteEventIdException"/> class.
        /// </summary>
        public NoWriteEventIdException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoWriteEventIdException"/> class.
        /// </summary>
        public NoWriteEventIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}