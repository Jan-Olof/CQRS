namespace Domain.Read.Entities
{
    using System;
    using System.Linq;

    using Common.DataAccess;

    /// <summary>
    /// The registration property.
    /// </summary>
    public class RegistrationProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProperty"/> class.
        /// </summary>
        public RegistrationProperty()
        {
            this.Value = string.Empty;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the property type id.
        /// </summary>
        public int PropertyTypeId { get; set; }

        /// <summary>
        /// Gets or sets the property type.
        /// </summary>
        public virtual PropertyType PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the registration id.
        /// </summary>
        public int RegistrationId { get; set; }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        public virtual Registration Registration { get; set; }

        /// <summary>
        /// Get property id for a RegistrationProperty.
        /// </summary>
        public static int GetPropertyId(IRepository<RegistrationProperty> repository, int typeId, string value)
        {
            var registrationProperty = repository.GetAll()
                .SingleOrDefault(p => p.PropertyTypeId == typeId
                    && string.Equals(p.Value, value, StringComparison.CurrentCultureIgnoreCase));

            return registrationProperty == null 
                ? 0 
                : registrationProperty.Id;
        }

    }
}