namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The delete exception.
    /// </summary>
    public class DeleteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        public DeleteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        public DeleteException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        public DeleteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}