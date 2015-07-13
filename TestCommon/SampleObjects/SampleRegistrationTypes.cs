namespace Tests.TestCommon.SampleObjects
{
    using System.Collections.Generic;

    using Domain.Read.Entities;

    /// <summary>
    /// The sample registration types.
    /// </summary>
    public static class SampleRegistrationTypes
    {
        /// <summary>
        /// The create registration types.
        /// </summary>
        public static IList<RegistrationType> CreateRegistrationTypes()
        {
            return new List<RegistrationType>
                       {
                           CreateRegistrationTypeBook(),
                           CreateRegistrationTypeMovie()
                       };
        }

        /// <summary>
        /// The create registration type book.
        /// </summary>
        public static RegistrationType CreateRegistrationTypeBook()
        {
            return new RegistrationType
                       {
                           Id = 1,
                           Name = "Book"
                       };
        }

        /// <summary>
        /// The create registration type movie.
        /// </summary>
        public static RegistrationType CreateRegistrationTypeMovie()
        {
            return new RegistrationType
                        {
                            Id = 2,
                            Name = "Movie"
                        };
        }

        /// <summary>
        /// The create registration type comic.
        /// </summary>
        public static RegistrationType CreateRegistrationTypeComic()
        {
            return new RegistrationType
                        {
                            Id = 3,
                            Name = "Comic"
                        };
        }
    }
}