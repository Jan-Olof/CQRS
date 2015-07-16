﻿namespace WriteToRead.Interfaces
{
    using System;

    using Domain.Read.Entities;

    /// <summary>
    /// The GenericRegistrationRepository interface.
    /// </summary>
    public interface IGenericRegistrationRepository
    {
        /// <summary>
        /// Check which registration type a registration is and return the type.
        /// </summary>
        RegistrationType CheckRegistrationType(string entityType);

        /// <summary>
        /// Add registration type to db set.
        /// </summary>
        RegistrationType AddRegistrationTypeToDbSet(string entityType);

        /// <summary>
        /// Add registration to db set.
        /// </summary>
        Registration AddRegistrationToDbSet(RegistrationType type, DateTime timestamp, string name);

        /// <summary>
        /// Check which property type a property is and return the type.
        /// </summary>
        PropertyType CheckPropertyType(string type);

        /// <summary>
        /// Insert a new PropertyType into the database.
        /// </summary>
        PropertyType InsertPropertType(string type);

        /// <summary>
        /// Check if a property is already registered and return the id.
        /// </summary>
        Property CheckProperty(PropertyType type, string value);

        /// <summary>
        /// Insert a new Property into the database.
        /// </summary>
        Property InsertProperty(PropertyType type, string value, Registration registration);

        /// <summary>
        /// Add a registration to a property.
        /// </summary>
        bool AddRegistrationToProperty(Property property, Registration registration);

        /// <summary>
        /// Save all changes in DbContext to the database
        /// </summary>
        bool SaveAllChanges();
    }
}