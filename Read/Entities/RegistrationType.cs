namespace Domain.Read.Entities
{
    using System;
    using System.Collections.Generic;

    using Common.DataAccess;

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

        /// <summary>
        /// Create a registration type object.
        /// </summary>
        public static RegistrationType CreateRegistrationType(string type)
        {
            return new RegistrationType
                       {
                           Id = 0,
                           Name = type
                       };
        }

        /// <summary>
        /// Get registration type id from a type string. Return zero if not found.
        /// </summary>
        public static int GetRegistrationTypeId(IRepository<RegistrationType> repository, string type)
        {
            var types = repository.GetAll();

            if (types == null)
            {
                return 0;
            }

            foreach (var registrationType in types)
            {
                if (string.Equals(type, registrationType.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return registrationType.Id;
                }
            }

            return 0;
        }
    }
}