﻿namespace Tests.TestCommon.SampleObjects
{
    using Common.DataTransferObjects;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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
        /// The create gdto without name.
        /// </summary>
        public static Gdto CreateGdtoWithoutName()
        {
            return new Gdto
            {
                EntityType = "Book",
                Properties = CreatePropertiesWithoutName()
            };
        }

        /// <summary>
        /// The create gdto with published.
        /// </summary>
        public static Gdto CreateGdtoWithPublished()
        {
            return new Gdto
            {
                EntityType = "Book",
                Properties = CreatePropertiesWithPublished()
            };
        }

        /// <summary>
        /// The create gdto.
        /// </summary>
        public static Gdto CreateGdtoWithWriteEventId()
        {
            return new Gdto
            {
                EntityType = "Book",
                Properties = CreatePropertiesWithWriteEventId()
            };
        }

        /// <summary>
        /// The create gdto.
        /// </summary>
        public static Gdto CreateGdtoWithWriteEventIdMovie()
        {
            return new Gdto
            {
                EntityType = "Movie",
                Properties = CreatePropertiesWithWriteEventIdMovie()
            };
        }

        /// <summary>
        /// The create gdto.
        /// </summary>
        public static Gdto CreateGdtoWithWriteEventIdMovieNewProp()
        {
            return new Gdto
            {
                EntityType = "Movie",
                Properties = CreatePropertiesWithWriteEventIdMovieNewProp()
            };
        }

        /// <summary>
        /// The create gdto.
        /// </summary>
        public static Gdto CreateGdtoWithWriteEventIdMovieRemoveProp()
        {
            return new Gdto
            {
                EntityType = "Movie",
                Properties = CreatePropertiesWithWriteEventIdMovieRemoveProp()
            };
        }

        /// <summary>
        /// The create payload.
        /// </summary>
        public static string CreatePayload()
        {
            return "{\"EntityType\": \"Book\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"Sapiens\"},{\"Key\": \"Author\",\"Value\": \"Yuval Noah Harari\"},{\"Key\": \"Published\",\"Value\": \"2014\"}	]}";
        }

        /// <summary>
        /// The create properties.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreateProperties()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "Sapiens"),
                                        new KeyValuePair<string, string>("Author", "Yuval Noah Harari")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties without name.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithoutName()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Published", "2014"),
                                        new KeyValuePair<string, string>("Author", "Yuval Noah Harari")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties with published.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithPublished()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "Sapiens"),
                                        new KeyValuePair<string, string>("Author", "Yuval Noah Harari"),
                                        new KeyValuePair<string, string>("Published", "2014")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithWriteEventId()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "Sapiens"),
                                        new KeyValuePair<string, string>("OriginalWriteEventId", "1"),
                                        new KeyValuePair<string, string>("Author", "Yuval Noah Harari")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithWriteEventIdMovie()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "2001: A Space Odyssey"),
                                        new KeyValuePair<string, string>("OriginalWriteEventId", "2"),
                                        new KeyValuePair<string, string>("Author", "Stanley Kubrikk")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithWriteEventIdMovieNewProp()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "2001: A Space Odyssey"),
                                        new KeyValuePair<string, string>("OriginalWriteEventId", "2"),
                                        new KeyValuePair<string, string>("Author", "Stanley Kubrick"),
                                        new KeyValuePair<string, string>("Published", "1968"),
                                        new KeyValuePair<string, string>("Actor", "Keir Dullea")
                                    };

            return keyValuePairs;
        }

        /// <summary>
        /// The create properties.
        /// </summary>
        private static IList<KeyValuePair<string, string>> CreatePropertiesWithWriteEventIdMovieRemoveProp()
        {
            var keyValuePairs = new Collection<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("Name", "2001: A Space Odyssey"),
                                        new KeyValuePair<string, string>("OriginalWriteEventId", "2"),
                                        new KeyValuePair<string, string>("Author", "DeleteProperty")
                                    };

            return keyValuePairs;
        }
    }
}