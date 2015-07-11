namespace Tests.TestCommon
{
    using System;

    /// <summary>
    /// The assert failed exception.
    /// </summary>
    public class AssertFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertFailedException"/> class.
        /// </summary>
        public AssertFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertFailedException"/> class.
        /// </summary>
        public AssertFailedException(string message)
            : base(message)
        {
        }
    }
}