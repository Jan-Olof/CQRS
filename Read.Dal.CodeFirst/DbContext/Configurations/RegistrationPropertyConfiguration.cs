namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Read.Entities;

    /// <summary>
    /// The registration property configuration.
    /// </summary>
    public class RegistrationPropertyConfiguration : EntityTypeConfiguration<RegistrationProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationPropertyConfiguration"/> class.
        /// </summary>
        public RegistrationPropertyConfiguration()
        {
            this.Property(registrationProperty => registrationProperty.Value).HasMaxLength(250);
        }
    }
}