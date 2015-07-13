namespace WriteToRead.ToReadDb
{
    using System;

    using Common.DataAccess;

    using Domain.Read.Entities;

    using NLog;

    using WriteToRead.Interfaces;

    /// <summary>
    /// Class that is used to transfer data from the Write database to the Read database.
    /// </summary>
    public class GenericRegistrationService : IGenericRegistrationService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The registration type repository.
        /// </summary>
        private readonly IRepository<RegistrationType> registrationTypeRepository;

        /// <summary>
        /// The registration type repository.
        /// </summary>
        private readonly IRepository<PropertyType> propertyTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRegistrationService"/> class.
        /// </summary>
        public GenericRegistrationService(
            IRepository<RegistrationType> registrationTypeRepository, IRepository<PropertyType> propertyTypeRepository)
        {
            if (registrationTypeRepository == null)
            {
                throw new ArgumentNullException("registrationTypeRepository");
            }

            if (propertyTypeRepository == null)
            {
                throw new ArgumentNullException("propertyTypeRepository");
            }

            this.registrationTypeRepository = registrationTypeRepository;
            this.propertyTypeRepository = propertyTypeRepository;
        }

        /// <summary>
        /// Check which registration type a registration is and return the type id.
        /// </summary>
        public int CheckRegistrationType(string entityType)
        {
            try
            {
                return RegistrationType.GetRegistrationTypeId(this.registrationTypeRepository, entityType);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Insert a new RegistrationType into the database.
        /// </summary>
        public int InsertRegistrationType(string entityType)
        {
            try
            {
                var registrationType = RegistrationType.CreateRegistrationType(entityType);

                return this.registrationTypeRepository.Insert(registrationType).Id;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Check which property type a property is and return the type id.
        /// </summary>
        public int CheckPropertyType(string type)
        {
            try
            {
                return PropertyType.GetPropertTypeId(this.propertyTypeRepository, type);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Insert a new PropertyType into the database.
        /// </summary>
        public int InsertPropertType(string type)
        {
            try
            {
                var propertyType = PropertyType.CreatePropertyType(type);

                return this.propertyTypeRepository.Insert(propertyType).Id;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }
    }
}