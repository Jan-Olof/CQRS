﻿namespace DataAccess.Read.Dal.CodeFirst.DbContext
{
    using DataAccess.Read.Dal.CodeFirst.DbContext.Configurations;
    using Domain.Read.Entities;
    using System.Data.Entity;

    /// <summary>
    /// The read context.
    /// </summary>
    public class ReadContext : DbContext, IReadContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadContext"/> class.
        /// </summary>
        public ReadContext()
            : base("ReadContext")
        {
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public IDbSet<Property> Properties { get; set; }

        /// <summary>
        /// Gets or sets the property types.
        /// </summary>
        public IDbSet<PropertyType> PropertyTypes { get; set; }

        /// <summary>
        /// Gets or sets the registrations.
        /// </summary>
        public IDbSet<Registration> Registrations { get; set; }

        /// <summary>
        /// Gets or sets the registration types.
        /// </summary>
        public IDbSet<RegistrationType> RegistrationTypes { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context.
        /// The default implementation of this method does nothing, but it can be overridden
        /// in a derived class such that the model can be further configured before it
        /// is locked down.
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RegistrationConfiguration());
            modelBuilder.Configurations.Add(new PropertyTypeConfiguration());
            modelBuilder.Configurations.Add(new PropertyConfiguration());
            modelBuilder.Configurations.Add(new RegistrationTypeConfiguration());
        }
    }
}