namespace WriteToRead
{
    using System;

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
        /// Initializes a new instance of the <see cref="WriteToReadService"/> class.
        /// </summary>
        public WriteToReadService(IWriteEventService writeEventService)
        {
            if (writeEventService == null)
            {
                throw new ArgumentNullException("writeEventService");
            }

            this.writeEventService = writeEventService;
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

                    // TODO: Continue here.
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