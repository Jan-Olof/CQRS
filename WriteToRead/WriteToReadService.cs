namespace WriteToRead
{
    using Common.DataTransferObjects;
    using Common.Enums;
    using Domain.Write.Interfaces;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IWriteToReadRepository writeToReadRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToReadService"/> class.
        /// </summary>
        public WriteToReadService(IWriteEventService writeEventService, IWriteToReadRepository writeToReadRepository)
        {
            if (writeEventService == null)
            {
                throw new ArgumentNullException(nameof(writeEventService));
            }

            if (writeToReadRepository == null)
            {
                throw new ArgumentNullException(nameof(writeToReadRepository));
            }

            this.writeEventService = writeEventService;
            this.writeToReadRepository = writeToReadRepository;
        }

        /// <summary>
        /// Add to the generic registration read database.
        /// </summary>
        public int EtlFromWriteDbToReadDb(DateTime timestamp)
        {
            try
            {
                var writeEvents = this.writeEventService.GetWriteEventsToProcess(0);

                return writeEvents?
                    .Aggregate(0, (current, writeEvent) => this.RegisterOneEvent(timestamp, writeEvent, current))
                    ?? 0;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        private void DeleteRegistration(IList<KeyValuePair<string, string>> gdtoProperties)
        {
            int originalWriteEventId = this.writeEventService.GetOriginalWriteEventId(gdtoProperties);

            var registration = this.writeToReadRepository.GetRegistration(originalWriteEventId);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a new Registration into the database.
        /// </summary>
        private void InsertRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue, int originalWriteEventId)
        {
            var registrationType = this.writeToReadRepository.GetRegistrationType(gdto);

            var registration = this.writeToReadRepository.AddRegistrationToDbSet(
                registrationType, timestamp, namePropertyValue, originalWriteEventId);

            this.writeToReadRepository.AddRegistrationProperties(gdto, registration);
        }

        /// <summary>
        /// Register one event in the read database.
        /// </summary>
        private int RegisterOneEvent(DateTime timestamp, IWriteEvent writeEvent, int result)
        {
            var gdto = this.writeEventService.DeserializeGdto(writeEvent.Payload);

            var namePropertyValue = this.writeEventService.GetPropertyValue(gdto.Properties, "Name");

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
                    this.DeleteRegistration(gdto.Properties);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (this.writeToReadRepository.SaveAllChanges())
            {
                result++;
                this.writeEventService.SetSentToRead(writeEvent, 1);
            }

            return result;
        }

        private void UpdateRegistration(DateTime timestamp, Gdto gdto, string namePropertyValue)
        {
            int originalWriteEventId = this.writeEventService.GetOriginalWriteEventId(gdto.Properties);

            var registration = this.writeToReadRepository.GetRegistration(originalWriteEventId);
            var registrationType = this.writeToReadRepository.GetRegistrationType(gdto);

            registration = this.writeToReadRepository.UpdateRegistration(
                registration, registrationType, timestamp, namePropertyValue);

            this.writeToReadRepository.UpdateProperties(registration, gdto);
        }
    }
}