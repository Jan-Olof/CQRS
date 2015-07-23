namespace WriteToRead
{
    using System;
    using System.Linq;

    using Common.DataTransferObjects;
    using Common.Enums;

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
            var registrationType = this.genericRegistrationRepository.GetRegistrationType(gdto);

            var registration = this.genericRegistrationRepository.AddRegistrationToDbSet(
                registrationType, timestamp, namePropertyValue, originalWriteEventId);

            this.genericRegistrationRepository.AddProperties(gdto, registration);
        }

        private void UpdateRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue)
        {
            int originalWriteEventId = this.writeEventService.GetOriginalWriteEventId(gdto);

            var registration = this.genericRegistrationRepository.GetRegistration(originalWriteEventId);
            var registrationType = this.genericRegistrationRepository.GetRegistrationType(gdto);

            registration = this.genericRegistrationRepository.UpdateRegistration(
                registration, registrationType, timestamp, namePropertyValue);

            //TODO: Update properties!
        }

        private void DeleteRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue)
        {
            throw new NotImplementedException();
        }


    }
}