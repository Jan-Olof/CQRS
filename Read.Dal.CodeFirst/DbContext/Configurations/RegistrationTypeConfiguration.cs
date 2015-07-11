namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Read.Entities;

    /// <summary>
    /// The registration type configuration.
    /// </summary>
    public class RegistrationTypeConfiguration : EntityTypeConfiguration<RegistrationType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationTypeConfiguration"/> class.
        /// </summary>
        public RegistrationTypeConfiguration()
        {
            this.Property(registrationType => registrationType.Name).HasMaxLength(50);
        }
    }
}