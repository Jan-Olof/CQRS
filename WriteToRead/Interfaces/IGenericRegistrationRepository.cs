namespace WriteToRead.Interfaces
{
    using System;
    using System.Collections.Generic;

    using Common.DataTransferObjects;

    using Domain.Read.Entities;

    /// <summary>
    /// The GenericRegistrationRepository interface.
    /// </summary>
    public interface IGenericRegistrationRepository
    {
        /// <summary>
        /// Get RegistrationType from a Gdto.
        /// </summary>
        RegistrationType GetRegistrationType(Gdto gdto);

        /// <summary>
        /// Add properties to a registration.
        /// </summary>
        void AddProperties(Gdto gdto, Registration registration);

        /// <summary>
        /// Add a property to a registration.
        /// </summary>
        void AddProperty(PropertyType propertyType, KeyValuePair<string, string> property, Registration registration);

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
        Registration AddRegistrationToDbSet(RegistrationType type, DateTime timestamp, string name, int originalWriteEventId);

        /// <summary>
        /// Check which property type a property is and return the type.
        /// </summary>
        PropertyType CheckPropertyType(string type);

        /// <summary>
        /// Add property type to db set.
        /// </summary>
        PropertyType AddPropertyTypeToDbSet(string type);

        /// <summary>
        /// Check if a property is already registered and return the id.
        /// </summary>
        Property CheckProperty(PropertyType type, string value);

        /// <summary>
        /// Add property to db set.
        /// </summary>
        Property AddPropertyToDbSet(PropertyType type, string value, Registration registration);

        /// <summary>
        /// Add a registration to a property.
        /// </summary>
        bool AddRegistrationToProperty(Property property, Registration registration);

        /// <summary>
        /// Get registration from OriginalWriteEventId.
        /// </summary>
        Registration GetRegistration(int originalWriteEventId);

        /// <summary>
        /// Update a registration.
        /// </summary>
        Registration UpdateRegistration(Registration registration, RegistrationType type, DateTime timestamp, string name);

        /// <summary>
        /// Update all properties for a registration. Also inserting new and removing old properties.
        /// </summary>
        Registration UpdateProperties(Registration registration, Gdto gdto);

        /// <summary>
        /// Delete a registration.
        /// </summary>
        bool DeleteRegistration(string namePropertyValue);

        /// <summary>
        /// Save all changes in DbContext to the database
        /// </summary>
        bool SaveAllChanges();
    }
}