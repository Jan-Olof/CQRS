namespace Tests.TestCommon.SampleObjects
{
    using Domain.Read.Entities;

    /// <summary>
    /// The sample registration types.
    /// </summary>
    public static class SampleRegistrationTypes
    {
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
    }
}