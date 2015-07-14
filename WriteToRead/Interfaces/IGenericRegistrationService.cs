namespace WriteToRead.Interfaces
{
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
        int CheckProperty(string type);
    }
}