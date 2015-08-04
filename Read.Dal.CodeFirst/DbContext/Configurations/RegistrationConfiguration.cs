namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using Domain.Read.Entities;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// The registration configuration.
    /// </summary>
    public class RegistrationConfiguration : EntityTypeConfiguration<Registration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationConfiguration"/> class.
        /// </summary>
        public RegistrationConfiguration()
        {
            this.Property(registration => registration.Name).HasMaxLength(50);
        }
    }
}