namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The update exception.
    /// </summary>
    public class UpdateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        public UpdateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        public UpdateException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        public UpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}