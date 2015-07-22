namespace WriteToRead
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataTransferObjects;
    using Common.Enums;

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

            var namePropertyValue = this.writeEventService.GetPropertyValue(gdto, "Name");

            if (string.IsNullOrWhiteSpace(namePropertyValue))
            {
                return result;
            }

            switch (writeEvent.CommandType)
            {
                case CommandType.Insert:
                    this.InsertRegistration(timestamp, gdto, namePropertyValue, writeEvent.Id);
                    break;
                case CommandType.Update:
                    this.UpdateRegistration(timestamp, gdto, namePropertyValue);
                    break;
                case CommandType.Delete:
                     this.DeleteRegistration(timestamp, gdto, namePropertyValue);
                   break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (this.genericRegistrationRepository.SaveAllChanges())
            {
                result++;

                this.writeEventService.SetSentToRead(writeEvent, 1);
            }

            return result;
        }

        /// <summary>
        /// Insert a new Registration into the database.
        /// </summary>
        private void InsertRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue, int originalWriteEventId)
        {
            var registrationType = this.GetRegistrationType(gdto);

            var registration = this.genericRegistrationRepository.AddRegistrationToDbSet(
                registrationType, timestamp, namePropertyValue, originalWriteEventId);

            this.AddProperties(gdto, registration);
        }

        private void UpdateRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue)
        {
            var originalWriteEventId = this.writeEventService.GetPropertyValue(gdto, "OriginalWriteEventId");

            throw new NotImplementedException();
        }

        private void DeleteRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        private RegistrationType GetRegistrationType(Gdto gdto)
        {
            var registrationType = this.genericRegistrationRepository.CheckRegistrationType(gdto.EntityType);

            if (registrationType.Id == 0)
            {
                registrationType = this.genericRegistrationRepository.AddRegistrationTypeToDbSet(gdto.EntityType);
            }
            return registrationType;
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