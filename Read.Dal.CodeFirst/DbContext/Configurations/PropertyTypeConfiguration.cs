namespace DataAccess.Read.Dal.CodeFirst.DbContext.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Read.Entities;

    /// <summary>
    /// The property type configuration.
    /// </summary>
    public class PropertyTypeConfiguration : EntityTypeConfiguration<PropertyType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTypeConfiguration"/> class.
        /// </summary>
        public PropertyTypeConfiguration()
        {
            this.Property(propertyType => propertyType.Name).HasMaxLength(50);
        }
    }
}