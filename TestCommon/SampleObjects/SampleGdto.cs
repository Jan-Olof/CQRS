namespace Tests.TestCommon.SampleObjects
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Common.DataTransferObjects;

    /// <summary>
    /// The sample gdto.
    /// </summary>
    public static class SampleGdto
    {
        /// <summary>
        /// The create gdto.
        /// </summary>
        public static Gdto CreateGdto()
        {
            return new Gdto
                       {
                           EntityType = "Book",
                           Properties = CreateProperties()
                       };
        }

        /// <summary>
        /// The create strings.
        /// </summary>
        private static ICollection<KeyValuePair<string, string>> CreateProperties()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "Sapiens"),
                                        new KeyValuePair<string, string>("Author", "Yuval Noah Harari")
                                    };

            return keyValuePairs;
        }
    }
}