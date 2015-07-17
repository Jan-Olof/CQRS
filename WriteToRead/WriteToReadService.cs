namespace WriteToRead
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataTransferObjects;

    using Domain.Read.Entities;
    using Domain.Write.Interfaces;

    using NLog;

    using WriteToRead.Interfaces;

    /// <summary>
    /// The write to read service is like an ETL from the write database to one or more read databases.
    /// </summary>
    public class WriteToReadService : IWriteToReadService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The write event service.
        /// </summary>
        private readonly IWriteEventService writeEventService;

        /// <summary>
        /// The generic registration service.
        /// </summary>
        private readonly IGenericRegistrationRepository genericRegistrationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToReadService"/> class.
        /// </summary>
        public WriteToReadService(IWriteEventService writeEventService, IGenericRegistrationRepository genericRegistrationRepository)
        {
            if (writeEventService == null)
            {
                throw new ArgumentNullException("writeEventService");
            }

            if (genericRegistrationRepository == null)
            {
                throw new ArgumentNullException("genericRegistrationRepository");
            }

            this.writeEventService = writeEventService;
            this.genericRegistrationRepository = genericRegistrationRepository;
        }

        /// <summary>
        /// Add to the generic registration read database.
        /// </summary>
        public int AddToGenericRegistrationReadDb(DateTime timestamp)
        {
            try
            {
                var writeEvents = this.writeEventService.GetWriteEventsToProcess(0);

                return writeEvents == null
                    ? 0
                    : writeEvents.Aggregate(0, (current, writeEvent) => this.RegisterOneEvent(timestamp, writeEvent, current));
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Register one event in the read database.
        /// </summary>
        private int RegisterOneEvent(DateTime timestamp, IWriteEvent writeEvent, int result)
        {
            var gdto = this.writeEventService.DeserializeGdto(writeEvent.Payload);

            var namePropertyValue = this.writeEventService.GetNamePropertyValue(gdto);

            if (string.IsNullOrWhiteSpace(namePropertyValue))
            {
                return result;
            }

            var registrationType = this.genericRegistrationRepository.CheckRegistrationType(gdto.EntityType);

            if (registrationType.Id == 0)
            {
                registrationType = this.genericRegistrationRepository.AddRegistrationTypeToDbSet(gdto.EntityType);
            }

            var registration = this.genericRegistrationRepository.AddRegistrationToDbSet(
                registrationType, timestamp, namePropertyValue);

            this.AddProperties(gdto, registration);

            if (this.genericRegistrationRepository.SaveAllChanges())
            {
                result++;

                this.writeEventService.SetSentToRead(writeEvent, 1);
            }

            return result;
        }

        /// <summary>
        /// Add properties to a registration.
        /// </summary>
        private void AddProperties(Gdto gdto, Registration registration)
        {
            foreach (var property in gdto.Properties)
            {
                var propertyType = this.genericRegistrationRepository.CheckPropertyType(property.Key);

                if (propertyType.Id == 0)
                {
                    propertyType = this.genericRegistrationRepository.AddPropertyTypeToDbSet(property.Key);
                }

                if (propertyType.Id > -1)
                {
                    this.AddProperty(propertyType, property, registration);
                }
            }
        }

        /// <summary>
        /// Add a property to a registration.
        /// </summary>
        private void AddProperty(PropertyType propertyType, KeyValuePair<string, string> property, Registration registration)
        {
            var registrationProperty = this.genericRegistrationRepository.CheckProperty(propertyType, property.Value);

            if (registrationProperty.Id == 0)
            {
                this.genericRegistrationRepository.AddPropertyToDbSet(propertyType, property.Value, registration);
            }
            else
            {
                this.genericRegistrationRepository.AddRegistrationToProperty(registrationProperty, registration);
            }
        }
    }
}