namespace WriteToRead
{
    using System;
    using System.Collections.Generic;

    using Domain.Read.Entities;

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
        public bool AddToGenericRegistrationReadDb()
        {
            try
            {
                var writeEvents = this.writeEventService.GetWriteEventsToProcess(0);

                if (writeEvents == null)
                {
                    return true;
                }

                foreach (var writeEvent in writeEvents)
                {
                    var gdto = this.writeEventService.DeserializeGdto(writeEvent.Payload);

                    var registrationTypeId = this.genericRegistrationService.CheckRegistrationType(gdto.EntityType);

                    if (registrationTypeId == 0)
                    {
                        registrationTypeId = this.genericRegistrationService.InsertRegistrationType(gdto.EntityType);
                    }

                    ICollection<RegistrationProperty> registrationProperties;
                    foreach (var property in gdto.Properties)
                    {
                        var propertyTypeId = this.genericRegistrationService.CheckPropertyType(property.Key);

                        if (propertyTypeId == 0)
                        {
                            propertyTypeId = this.genericRegistrationService.InsertPropertType(property.Key);
                        }

                        if (propertyTypeId > 0)
                        {
                            //var x
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }

            return false;
        }
    }
}