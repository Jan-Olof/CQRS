﻿namespace DataAccess.Read.Dal.CodeFirst.DbContext
{
    using System.Data.Entity;

    using Domain.Read.Entities;

    /// <summary>
    /// The ReadContext interface.
    /// </summary>
    public interface IReadContext
    {
        /// <summary>
        /// Gets or sets the registrations.
        /// </summary>
        IDbSet<Registration> Registrations { get; set; }

        /// <summary>
        /// Gets or sets the property types.
        /// </summary>
        IDbSet<PropertyType> PropertyTypes { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        IDbSet<Property> Properties { get; set; }

        /// <summary>
        /// Gets or sets the registration types.
        /// </summary>
        IDbSet<RegistrationType> RegistrationTypes { get; set; }

        /// <summary>
        /// Gets the database.
        /// Creates a Database instance for this context that allows for creation/deletion/existence
        /// checks for the underlying database.
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        int SaveChanges();
    }
}