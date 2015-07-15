namespace WriteToRead.ToReadDb
{
    using System;
    using System.Collections.ObjectModel;

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
        /// The registration repository.
        /// </summary>
        private readonly IRepository<Registration> registrationRepository;

        /// <summary>
        /// The property type repository.
        /// </summary>
        private readonly IRepository<PropertyType> propertyTypeRepository;

        /// <summary>
        /// The property repository.
        /// </summary>
        private readonly IRepository<Property> propertyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRegistrationService"/> class.
        /// </summary>
        public GenericRegistrationService(
            IRepository<RegistrationType> registrationTypeRepository,
            IRepository<Registration> registrationRepository,
            IRepository<PropertyType> propertyTypeRepository,
            IRepository<Property> propertyRepository)
        {
            if (registrationTypeRepository == null)
            {
                throw new ArgumentNullException("registrationTypeRepository");
            }

            if (registrationRepository == null)
            {
                throw new ArgumentNullException("registrationRepository");
            }

            if (propertyTypeRepository == null)
            {
                throw new ArgumentNullException("propertyTypeRepository");
            }

            if (propertyRepository == null)
            {
                throw new ArgumentNullException("propertyRepository");
            }

            this.registrationTypeRepository = registrationTypeRepository;
            this.propertyTypeRepository = propertyTypeRepository;
            this.propertyRepository = propertyRepository;
            this.registrationRepository = registrationRepository;
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
        /// Insert a new Registration into the database.
        /// </summary>
        public Registration InsertRegistration(int typeId, DateTime timestamp, string name)
        {
            try
            {
                var registration = Registration.CreateRegistration(typeId, timestamp, name);

                return this.registrationRepository.Insert(registration);
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

        /// <summary>
        /// Check if a property is already registered and return the id.
        /// </summary>
        public Property CheckProperty(int type, string value)
        {
            try
            {
                return Property.GetProperty(this.propertyRepository, type, value);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Insert a new Property into the database.
        /// </summary>
        public Property InsertProperty(int typeId, string value, Registration registration)
        {
            try
            {
                var property = Property.CreateProperty(typeId, value, registration);

                return this.propertyRepository.Insert(property);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Add a registration to a property.
        /// </summary>
        public bool AddRegistrationToProperty(Property property, Registration registration)
        {
            try
            {
                if (property.Registrations == null)
                {
                    property.Registrations = new Collection<Registration>();
                }

                property.Registrations.Add(registration);

                return this.propertyRepository.SaveAllChanges();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }
    }
}