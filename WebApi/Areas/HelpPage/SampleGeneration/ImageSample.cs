namespace Api.WebApi.Areas.HelpPage
{
    using System;

    /// <summary>
    /// This represents an image sample on the help page. There's a display template named ImageSample associated with this class.
    /// </summary>
    public class ImageSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSample"/> class.
        /// </summary>
        /// <param name="src">The URL of an image.</param>
        public ImageSample(string src)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }
            Src = src;
        }

        /// <summary>
        /// Gets the src.
        /// </summary>
        public string Src { get; private set; }

        /// <summary>
        /// The equals.
        /// </summary>
        public override bool Equals(object obj)
        {
            ImageSample other = obj as ImageSample;
            return other != null && Src == other.Src;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Src.GetHashCode();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        public override string ToString()
        {
            return Src;
        }
    }
}