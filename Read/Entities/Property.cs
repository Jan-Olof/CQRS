namespace Domain.Read.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Common.DataAccess;

    /// <summary>
    /// The registration property.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
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
        /// Gets or sets the registrations.
        /// </summary>
        public virtual ICollection<Registration> Registrations { get; set; }

        /// <summary>
        /// Create a property object.
        /// </summary>
        public static Property CreateProperty(PropertyType propertyType, string value, Registration registration)
        {
            return new Property
                       {
                           PropertyTypeId = propertyType.Id,
                           PropertyType = propertyType,
                           Value = value,
                           Registrations = new Collection<Registration> { registration }
                       };
        }

        /// <summary>
        /// Get property from type and value.
        /// </summary>
        public static Property GetProperty(IRepository<Property> repository, int typeId, string value)
        {
            var properties = repository.GetAll();

            if (properties == null)
            {
                return new Property();
            }

            foreach (var property in properties)
            {
                if (property.PropertyTypeId != typeId)
                {
                    continue;
                }

                if (string.Equals(property.Value, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return property;
                }
            }

            return new Property();
        }
    }
}