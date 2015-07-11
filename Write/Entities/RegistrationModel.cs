namespace Domain.Write.Entities
{
    using System;

    using Common.Utilities;

    /// <summary>
    /// The RegistrationModel.
    /// </summary>
    public abstract class RegistrationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationModel"/> class.
        /// </summary>
        public RegistrationModel()
        {
            this.Name = string.Empty;
            this.Created = SystemTime.DateTimeDefault;
            this.Updated = SystemTime.DateTimeDefault;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationModel"/> class.
        /// Copy constructor.
        /// </summary>
        public RegistrationModel(RegistrationModel registrationModel)
        {
            this.Id = registrationModel.Id;
            this.Name = registrationModel.Name;
            this.Created = registrationModel.Created;
            this.Updated = registrationModel.Updated;
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
    }
}