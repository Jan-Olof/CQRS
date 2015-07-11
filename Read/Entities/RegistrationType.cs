namespace Domain.Read.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The registration type.
    /// </summary>
    public class RegistrationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationType"/> class.
        /// </summary>
        public RegistrationType()
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
        /// Gets or sets the registrations.
        /// </summary>
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}