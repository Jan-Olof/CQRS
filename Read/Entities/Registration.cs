namespace Domain.Read.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataAccess;
    using Common.Utilities;

    /// <summary>
    /// The registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Registration"/> class.
        /// </summary>
        public Registration()
        {
            this.Name = string.Empty;
            this.Created = SystemTime.DateTimeDefault;
            this.Updated = SystemTime.DateTimeDefault;
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
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the registration type id.
        /// </summary>
        public int RegistrationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the registration type.
        /// </summary>
        public virtual RegistrationType RegistrationType { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public virtual ICollection<Property> Properties { get; set; }

        /// <summary>
        /// Create a registration object.
        /// </summary>
        public static Registration CreateRegistration(RegistrationType type, DateTime timestamp, string name)
        {
            return new Registration
                       {
                            RegistrationTypeId = type.Id,
                            RegistrationType = type,
                            Created = timestamp,
                            Updated = timestamp,
                            Name = name
                       }; 
        }

        /// <summary>
        /// The get registrations of one type.
        /// </summary>
        public static IQueryable<Registration> GetRegistrationsOfOneType(IRepository<Registration> repository, int type)
        {
            return repository
                .GetAll()
                .Where(r => r.RegistrationType.Id == type);
        }
    }
}