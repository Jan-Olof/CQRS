namespace Tests.TestCommon
{
    using System;

    /// <summary>
    /// The my assert.
    /// </summary>
    public static class MyAssert
    {
        /// <summary>
        /// Assert that a specific exception is thrown.
        /// </summary>
        public static void Throws<T>(Action func) where T : Exception
        {
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
            {
                throw new AssertFailedException(string.Format("An exception of type {0} was expected, but not thrown", typeof(T)));
            }
        }
    }
}