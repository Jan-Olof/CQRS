namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using Domain.Read.Entities;
    using System.Data.Entity.ModelConfiguration;

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