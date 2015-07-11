namespace Domain.Read.Entities
{
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

    }
}