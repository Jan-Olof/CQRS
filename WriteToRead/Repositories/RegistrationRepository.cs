//namespace WriteToRead.Repositories
//{
//    using System.Data.Entity;

//    using Common.DataAccess;

//    using Domain.Read.Entities;

//    using WriteToRead.Interfaces;

//    /// <summary>
//    /// The registration repository.
//    /// </summary>
//    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="RegistrationRepository"/> class.
//        /// </summary>
//        public RegistrationRepository(DbContext dataContext)
//            : base(dataContext)
//        {
//            this.RegistrationTypeDbSet = dataContext.Set<RegistrationType>();
//            this.PropertyTypeDbSet = dataContext.Set<PropertyType>();
//            this.PropertyDbSet = dataContext.Set<Property>();
//        }

//        /// <summary>
//        /// Gets or sets the registration type db set.
//        /// </summary>
//        private DbSet<RegistrationType> RegistrationTypeDbSet { get; set; }

//        /// <summary>
//        /// Gets or sets the property type db set.
//        /// </summary>
//        private DbSet<PropertyType> PropertyTypeDbSet { get; set; }

//        /// <summary>
//        /// Gets or sets the property db set.
//        /// </summary>
//        private DbSet<Property> PropertyDbSet { get; set; }

//    }
//}