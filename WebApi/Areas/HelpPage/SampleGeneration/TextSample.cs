namespace Api.WebApi.Areas.HelpPage
{
    using System;

    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSample"/> class.
        /// </summary>
        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            Text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The equals.
        /// </summary>
        public override bool Equals(object obj)
        {
            TextSample other = obj as TextSample;
            return other != null && Text == other.Text;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        public override string ToString()
        {
            return Text;
        }
    }
}