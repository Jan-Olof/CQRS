namespace Tests.TestCommon.SampleObjects
{
    using Domain.Read.Entities;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The sample registrations.
    /// </summary>
    public static class SampleRegistrations
    {
        /// <summary>
        /// The create registration 2001.
        /// </summary>
        public static Registration CreateRegistration2001()
        {
            return new Registration
            {
                Id = 2,
                Name = "2001: A Space Odyssey",
                Created = new DateTime(2015, 7, 9, 15, 52, 51),
                Updated = new DateTime(2015, 7, 9, 15, 52, 51),
                OriginalWriteEventId = 2,
                RegistrationTypeId = 2,
                RegistrationType = SampleRegistrationTypes.CreateRegistrationTypeMovie(),
                Properties = SampleProperties.CreateProperties2001()
            };
        }

        /// <summary>
        /// The create registration 2001.
        /// </summary>
        public static Registration CreateRegistration2001Db(Collection<Property> properties)
        {
            return new Registration
            {
                Id = 2,
                Name = "2001: A Space Odyssey",
                Created = new DateTime(2015, 7, 9, 15, 52, 51),
                Updated = new DateTime(2015, 7, 9, 15, 52, 51),
                OriginalWriteEventId = 2,
                RegistrationTypeId = 2,
                RegistrationType = null,
                Properties = properties
            };
        }

        /// <summary>
        /// The create registrations.
        /// </summary>
        public static IList<Registration> CreateRegistrations()
        {
            return new List<Registration> { CreateRegistrationSapiens(), CreateRegistration2001() };
        }

        /// <summary>
        /// The create registration.
        /// </summary>
        public static Registration CreateRegistrationSapiens()
        {
            return new Registration
            {
                Id = 1,
                Name = "Sapiens",
                Created = new DateTime(2015, 7, 9, 15, 41, 5),
                Updated = new DateTime(2015, 7, 9, 15, 41, 5),
                OriginalWriteEventId = 1,
                RegistrationTypeId = 1,
                RegistrationType = SampleRegistrationTypes.CreateRegistrationTypeBook(),
                Properties = SampleProperties.CreatePropertiesSapiens()
            };
        }

        /// <summary>
        /// The create registration.
        /// </summary>
        public static Registration CreateRegistrationSapiensDb(Collection<Property> properties)
        {
            return new Registration
            {
                Id = 1,
                Name = "Sapiens",
                Created = new DateTime(2015, 7, 9, 15, 41, 5),
                Updated = new DateTime(2015, 7, 9, 15, 41, 5),
                OriginalWriteEventId = 1,
                RegistrationTypeId = 1,
                RegistrationType = null,
                Properties = properties
            };
        }
    }
}