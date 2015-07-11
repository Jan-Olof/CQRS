namespace Domain.Read.Entities
{
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
        /// Gets or sets the registration properties.
        /// </summary>
        public virtual ICollection<RegistrationProperty> RegistrationProperties { get; set; }
    }
}