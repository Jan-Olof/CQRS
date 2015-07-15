namespace WriteToRead.Interfaces
{
    using System;

    using Domain.Read.Entities;

    /// <summary>
    /// The GenericRegistrationService interface.
    /// </summary>
    public interface IGenericRegistrationService
    {
        /// <summary>
        /// Check which registration type a registration is and return the type id.
        /// </summary>
        int CheckRegistrationType(string entityType);

        /// <summary>
        /// Insert a new RegistrationType into the database.
        /// </summary>
        int InsertRegistrationType(string entityType);

        /// <summary>
        /// Insert a new Registration into the database.
        /// </summary>
        Registration InsertRegistration(int typeId, DateTime timestamp, string name);

        /// <summary>
        /// Check which property type a property is and return the type id.
        /// </summary>
        int CheckPropertyType(string type);

        /// <summary>
        /// Insert a new PropertyType into the database.
        /// </summary>
        int InsertPropertType(string type);

        /// <summary>
        /// Check if a property is already registered and return the id.
        /// </summary>
        Property CheckProperty(int type, string value);

        /// <summary>
        /// Insert a new Property into the database.
        /// </summary>
        Property InsertProperty(int typeId, string value, Registration registration);

        /// <summary>
        /// Add a registration to a property.
        /// </summary>
        bool AddRegistrationToProperty(Property property, Registration registration);
    }
}