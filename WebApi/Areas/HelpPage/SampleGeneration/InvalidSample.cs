namespace Api.WebApi.Areas.HelpPage
{
    using System;

    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
    /// </summary>
    public class InvalidSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSample"/> class. 
        /// </summary>
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }

            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The equals.
        /// </summary>
        public override bool Equals(object obj)
        {
            InvalidSample other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}