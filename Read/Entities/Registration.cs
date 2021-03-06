﻿namespace Domain.Read.Entities
{
    using Common.DataAccess;
    using Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Registration"/> class.
        /// </summary>
        public Registration()
        {
            this.Name = string.Empty;
            this.Created = SystemTime.DateTimeDefault;
            this.Updated = SystemTime.DateTimeDefault;
        }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets the original write event id.
        /// </summary>
        public int OriginalWriteEventId { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public virtual ICollection<Property> Properties { get; set; }

        /// <summary>
        /// Gets or sets the registration type.
        /// </summary>
        public virtual RegistrationType RegistrationType { get; set; }

        /// <summary>
        /// Gets or sets the registration type id.
        /// </summary>
        public int RegistrationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Create a registration object.
        /// </summary>
        public static Registration CreateRegistration(RegistrationType type, DateTime timestamp, string name, int originalWriteEventId)
        {
            return new Registration
            {
                RegistrationTypeId = type.Id,
                RegistrationType = type,
                Created = timestamp,
                Updated = timestamp,
                OriginalWriteEventId = originalWriteEventId,
                Name = name
            };
        }

        /// <summary>
        /// Get registration from OriginalWriteEventId.
        /// </summary>
        public static Registration GetRegistration(IQueryable<Registration> dataSet, int originalWriteEventId)
        {
            return dataSet
                .SingleOrDefault(r => r.OriginalWriteEventId == originalWriteEventId);
        }

        /// <summary>
        /// Get registrations of one type.
        /// </summary>
        public static IQueryable<Registration> GetRegistrationsOfOneType(IRepository<Registration> repository, int type)
        {
            return repository
                .GetAll()
                .Where(r => r.RegistrationType.Id == type);
        }

        /// <summary>
        /// Update a registration.
        /// </summary>
        public static Registration UpdateRegistration(Registration registration, RegistrationType type, DateTime timestamp, string name)
        {
            registration.RegistrationType = type;
            registration.RegistrationTypeId = type.Id;
            registration.Name = name;
            registration.Updated = timestamp;

            return registration;
        }
    }
}