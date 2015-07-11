namespace Domain.Write.Entities
{
    /// <summary>
    /// The BookModel.
    /// </summary>
    public class BookModel : RegistrationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookModel"/> class.
        /// </summary>
        public BookModel()
        {
            this.Author = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookModel"/> class.
        /// Copy constructor.
        /// </summary>
        public BookModel(BookModel bookModel)
            : base(bookModel)
        {
            this.Author = bookModel.Author;
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }
    }
}