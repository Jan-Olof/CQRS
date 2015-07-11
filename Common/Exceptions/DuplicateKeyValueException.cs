namespace Common.Exceptions
{
    using System;

    /// <summary>
    /// The duplicate key value exception is thrown when you try to insert a duplicate value of a unique key in the db.
    /// </summary>
    public class DuplicateKeyValueException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyValueException"/> class.
        /// </summary>
        public DuplicateKeyValueException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyValueException"/> class.
        /// </summary>
        public DuplicateKeyValueException(string message)
            : base(message)
        {
        }
    }
}