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
        private readonly IGenericRegistrationService genericRegistrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToReadService"/> class.
        /// </summary>
        public WriteToReadService(IWriteEventService writeEventService, IGenericRegistrationService genericRegistrationService)
        {
            if (writeEventService == null)
            {
                throw new ArgumentNullException("writeEventService");
            }

            if (genericRegistrationService == null)
            {
                throw new ArgumentNullException("genericRegistrationService");
            }

            this.writeEventService = writeEventService;
            this.genericRegistrationService = genericRegistrationService;
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

            var registrationTypeId = this.genericRegistrationService.CheckRegistrationType(gdto.EntityType);

            if (registrationTypeId == 0)
            {
                registrationTypeId = this.genericRegistrationService.InsertRegistrationType(gdto.EntityType);
            }

            var namePropertyValue = this.writeEventService.GetNamePropertyValue(gdto);

            if (string.IsNullOrWhiteSpace(namePropertyValue))
            {
                return result;
            }

            var registration = this.genericRegistrationService.InsertRegistration(
                registrationTypeId, timestamp, namePropertyValue);

            if (registration.Id > 0)
            {
                this.AddProperties(gdto, registration);
                result++;
            }

            this.writeEventService.SetSentToRead(writeEvent, 1);

            return result;
        }

        /// <summary>
        /// Add properties to a registration.
        /// </summary>
        private void AddProperties(Gdto gdto, Registration registration)
        {
            foreach (var property in gdto.Properties)
            {
                var propertyTypeId = this.genericRegistrationService.CheckPropertyType(property.Key);

                if (propertyTypeId == 0)
                {
                    propertyTypeId = this.genericRegistrationService.InsertPropertType(property.Key);
                }

                if (propertyTypeId > 0)
                {
                    this.AddProperty(propertyTypeId, property, registration);
                }
            }
        }

        /// <summary>
        /// Add a property to a registration.
        /// </summary>
        private void AddProperty(int propertyTypeId, KeyValuePair<string, string> property, Registration registration)
        {
            var registrationProperty = this.genericRegistrationService.CheckProperty(propertyTypeId, property.Value);

            if (registrationProperty.Id == 0)
            {
                this.genericRegistrationService.InsertProperty(propertyTypeId, registrationProperty.Value, registration);
            }
            else
            {
                this.genericRegistrationService.AddRegistrationToProperty(registrationProperty, registration);
            }
        }
    }
}