namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The insert exception.
    /// </summary>
    public class InsertException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class.
        /// </summary>
        public InsertException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class.
        /// </summary>
        public InsertException(string message)
            : base(message)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class.
        /// </summary>
        public InsertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}