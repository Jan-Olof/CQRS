namespace Tests.TestCommon.SampleObjects
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Domain.Read.Entities;

    /// <summary>
    /// The sample registration properties.
    /// </summary>
    public static class SampleRegistrationProperties
    {
        /// <summary>
        /// The create registration properties sapiens.
        /// </summary>
        public static ICollection<RegistrationProperty> CreateRegistrationPropertiesSapiens()
        {
            return new Collection<RegistrationProperty>
                       {
                           CreateRegistrationPropertyHarari(),
                           CreateRegistrationProperty2014()
                       };
        }

        /// <summary>
        /// The create registration properties 2001.
        /// </summary>
        public static ICollection<RegistrationProperty> CreateRegistrationProperties2001()
        {
            return new Collection<RegistrationProperty>
                       {
                           CreateRegistrationPropertyKubrick(),
                           CreateRegistrationProperty1968()
                       };
        }

        /// <summary>
        /// The create registration property harari.
        /// </summary>
        public static RegistrationProperty CreateRegistrationPropertyHarari()
        {
            return new RegistrationProperty
                       {
                           Id = 1,
                           RegistrationId = 1,
                           PropertyTypeId = 1,
                           PropertyType = SamplePropertyTypes.CreatePropertyTypeAuthor(),
                           Value = "Yuval Noah Harari"
                       };
        }

        /// <summary>
        /// The create registration property kubrick.
        /// </summary>
        public static RegistrationProperty CreateRegistrationPropertyKubrick()
        {
            return new RegistrationProperty
                    {
                        Id = 3,
                        RegistrationId = 2,
                        PropertyTypeId = 1,
                        PropertyType = SamplePropertyTypes.CreatePropertyTypeAuthor(),
                        Value = "Stanley Kubrick"
                    };
        }

        /// <summary>
        /// The create registration property 2014.
        /// </summary>
        public static RegistrationProperty CreateRegistrationProperty2014()
        {
            return new RegistrationProperty
                    {
                        Id = 2,
                        RegistrationId = 1,
                        PropertyTypeId = 2,
                        PropertyType = SamplePropertyTypes.CreatePropertyTypePublished(),
                        Value = "2014"
                    };
        }

        /// <summary>
        /// The create registration property 1968.
        /// </summary>
        public static RegistrationProperty CreateRegistrationProperty1968()
        {
            return new RegistrationProperty
                    {
                        Id = 4,
                        RegistrationId = 2,
                        PropertyTypeId = 2,
                        PropertyType = SamplePropertyTypes.CreatePropertyTypePublished(),
                        Value = "1968"
                    };
        }

        /// <summary>
        /// The create registration property harari.
        /// </summary>
        public static RegistrationProperty CreateRegistrationPropertyHarariDb()
        {
            return new RegistrationProperty
            {
                Id = 1,
                RegistrationId = 1,
                PropertyTypeId = 1,
                PropertyType = null,
                Value = "Yuval Noah Harari"
            };
        }

        /// <summary>
        /// The create registration property kubrick.
        /// </summary>
        public static RegistrationProperty CreateRegistrationPropertyKubrickDb()
        {
            return new RegistrationProperty
            {
                Id = 3,
                RegistrationId = 2,
                PropertyTypeId = 1,
                PropertyType = null,
                Value = "Stanley Kubrick"
            };
        }

        /// <summary>
        /// The create registration property 2014.
        /// </summary>
        public static RegistrationProperty CreateRegistrationProperty2014Db()
        {
            return new RegistrationProperty
            {
                Id = 2,
                RegistrationId = 1,
                PropertyTypeId = 2,
                PropertyType = null,
                Value = "2014"
            };
        }

        /// <summary>
        /// The create registration property 1968.
        /// </summary>
        public static RegistrationProperty CreateRegistrationProperty1968Db()
        {
            return new RegistrationProperty
            {
                Id = 4,
                RegistrationId = 2,
                PropertyTypeId = 2,
                PropertyType = null,
                Value = "1968"
            };
        }
    }
}