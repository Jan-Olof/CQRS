namespace Domain.Read.Entities
{
    using Common.DataAccess;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The property type.
    /// </summary>
    public class PropertyType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyType"/> class.
        /// </summary>
        public PropertyType()
        {
            this.Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public virtual ICollection<Property> Properties { get; set; }

        /// <summary>
        /// Create a new property type object.
        /// </summary>
        public static PropertyType CreatePropertyType(int id, string propertyType)
        {
            return new PropertyType
            {
                Id = id,
                Name = propertyType
            };
        }

        /// <summary>
        /// Get property type id from a type string. Return zero if not found.
        /// </summary>
        public static int GetPropertTypeId(IRepository<PropertyType> repository, string type)
        {
            if (string.Equals(type, "Name", StringComparison.CurrentCultureIgnoreCase))
            {
                return -1;
            }

            var types = repository.GetAll();

            if (types == null)
            {
                return 0;
            }

            foreach (var propertyType in types)
            {
                if (string.Equals(type, propertyType.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return propertyType.Id;
                }
            }

            return 0;
        }
    }
}