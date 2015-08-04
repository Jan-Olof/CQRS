namespace Tests.TestCommon.SampleObjects
{
    using Domain.Read.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The sample registration properties.
    /// </summary>
    public static class SampleProperties
    {
        /// <summary>
        /// The create registration properties.
        /// </summary>
        public static ICollection<Property> CreateProperties()
        {
            return new Collection<Property>
                       {
                           CreatePropertyHarari(),
                           CreateProperty2014(),
                           CreatePropertyKubrick(),
                           CreateProperty1968()
                       };
        }

        /// <summary>
        /// The create registration properties 2001.
        /// </summary>
        public static ICollection<Property> CreateProperties2001()
        {
            return new Collection<Property>
                       {
                           CreatePropertyKubrick(),
                           CreateProperty1968()
                       };
        }

        /// <summary>
        /// The create registration properties sapiens.
        /// </summary>
        public static ICollection<Property> CreatePropertiesSapiens()
        {
            return new Collection<Property>
                       {
                           CreatePropertyHarari(),
                           CreateProperty2014()
                       };
        }

        /// <summary>
        /// The create registration property 1968.
        /// </summary>
        public static Property CreateProperty1968()
        {
            return new Property
            {
                Id = 4,
                PropertyTypeId = 2,
                PropertyType = SamplePropertyTypes.CreatePropertyTypePublished(),
                Value = "1968"
            };
        }

        /// <summary>
        /// The create registration property 1968.
        /// </summary>
        public static Property CreateProperty1968Db()
        {
            return new Property
            {
                Id = 4,
                PropertyTypeId = 2,
                PropertyType = null,
                Value = "1968"
            };
        }

        /// <summary>
        /// The create registration property 2014.
        /// </summary>
        public static Property CreateProperty2014()
        {
            return new Property
            {
                Id = 2,
                PropertyTypeId = 2,
                PropertyType = SamplePropertyTypes.CreatePropertyTypePublished(),
                Value = "2014"
            };
        }

        /// <summary>
        /// The create registration property 2014.
        /// </summary>
        public static Property CreateProperty2014Db()
        {
            return new Property
            {
                Id = 2,
                PropertyTypeId = 2,
                PropertyType = null,
                Value = "2014"
            };
        }

        /// <summary>
        /// The create registration property harari.
        /// </summary>
        public static Property CreatePropertyHarari()
        {
            return new Property
            {
                Id = 1,
                PropertyTypeId = 1,
                PropertyType = SamplePropertyTypes.CreatePropertyTypeAuthor(),
                Value = "Yuval Noah Harari"
            };
        }

        /// <summary>
        /// The create registration property harari.
        /// </summary>
        public static Property CreatePropertyHarariDb()
        {
            return new Property
            {
                Id = 1,
                PropertyTypeId = 1,
                PropertyType = null,
                Value = "Yuval Noah Harari"
            };
        }

        /// <summary>
        /// The create registration property kubrick.
        /// </summary>
        public static Property CreatePropertyKubrick()
        {
            return new Property
            {
                Id = 3,
                PropertyTypeId = 1,
                PropertyType = SamplePropertyTypes.CreatePropertyTypeAuthor(),
                Value = "Stanley Kubrick"
            };
        }

        /// <summary>
        /// The create registration property kubrick.
        /// </summary>
        public static Property CreatePropertyKubrickDb()
        {
            return new Property
            {
                Id = 3,
                PropertyTypeId = 1,
                PropertyType = null,
                Value = "Stanley Kubrick"
            };
        }
    }
}