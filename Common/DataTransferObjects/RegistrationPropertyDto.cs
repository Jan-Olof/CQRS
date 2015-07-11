namespace Common.DataTransferObjects
{
    /// <summary>
    /// The registration property.
    /// </summary>
    public class RegistrationPropertyDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationPropertyDto"/> class.
        /// </summary>
        public RegistrationPropertyDto()
        {
            this.Value = string.Empty;
            this.PropertyTypeName = string.Empty;
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
        /// Gets or sets the property type name.
        /// </summary>
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}