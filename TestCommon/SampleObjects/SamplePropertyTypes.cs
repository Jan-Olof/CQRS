namespace Tests.TestCommon.SampleObjects
{
    using Domain.Read.Entities;

    /// <summary>
    /// The sample property types.
    /// </summary>
    public static class SamplePropertyTypes
    {
        /// <summary>
        /// The create property type author.
        /// </summary>
        public static PropertyType CreatePropertyTypeAuthor()
        {
            return new PropertyType
                       {
                           Id = 1,
                           Name = "Author",
                       };
        }

        /// <summary>
        /// The create property type published.
        /// </summary>
        public static PropertyType CreatePropertyTypePublished()
        {
            return new PropertyType
                    {
                        Id = 2,
                        Name = "Published",
                    };
        }
    }
}