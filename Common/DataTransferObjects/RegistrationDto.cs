namespace Common.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    using Common.Utilities;

    /// <summary>
    /// The registration.
    /// </summary>
    public class RegistrationDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationDto"/> class.
        /// </summary>
        public RegistrationDto()
        {
            this.Name = string.Empty;
            this.RegistrationTypeName = string.Empty;
            this.Created = SystemTime.DateTimeDefault;
            this.Updated = SystemTime.DateTimeDefault;
            this.RegistrationProperties = new List<RegistrationPropertyDto>();
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
        ///  Gets or sets the original write event id.
        /// </summary>
        public int OriginalWriteEventId { get; set; }

        /// <summary>
        /// Gets or sets the registration type id.
        /// </summary>
        public int RegistrationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the registration type name.
        /// </summary>
        public string RegistrationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the registration properties.
        /// </summary>
        public IList<RegistrationPropertyDto> RegistrationProperties { get; set; }
    }
}