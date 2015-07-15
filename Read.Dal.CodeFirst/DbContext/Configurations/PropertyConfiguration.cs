namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Read.Entities;

    /// <summary>
    /// The property configuration.
    /// </summary>
    public class PropertyConfiguration : EntityTypeConfiguration<Property>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConfiguration"/> class.
        /// </summary>
        public PropertyConfiguration()
        {
            this.Property(property => property.Value).HasMaxLength(250);
        }
    }
}