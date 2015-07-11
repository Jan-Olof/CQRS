namespace Tests.TestCommon.SampleObjects
{
    using System;

    using Domain.Write.Entities;

    /// <summary>
    /// The sample books.
    /// </summary>
    public static class SampleBooks
    {
        /// <summary>
        /// The create book.
        /// </summary>
        public static BookModel CreateBook()
        {
            return new BookModel
                       {
                           Id = 11,
                           Name = "Sapiens",
                           Author = "Yuval Noah Harari",
                           Created = new DateTime(2015, 2, 5),
                           Updated = new DateTime(2015, 4, 1)
                       };
        }
    }
}