namespace WriteToRead.FromWriteDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataAccess;
    using Common.DataTransferObjects;

    using Domain.Write.Entities;

    using Newtonsoft.Json;

    using NLog;

    using WriteToRead.Interfaces;

    /// <summary>
    /// Communicate with the Write database.
    /// </summary>
    public class WriteEventService : IWriteEventService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<WriteEvent> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEventService"/> class.
        /// </summary>
        public WriteEventService(IRepository<WriteEvent> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Get the write events that needs to be processed.
        /// </summary>
        public IList<WriteEvent> GetWriteEventsToProcess(int timesSent)
        {
            try
            {
                var writeEvents = WriteEvent.GetWriteEventsNotSentToRead(this.repository, timesSent);

                return writeEvents == null 
                    ? new List<WriteEvent>() 
                    : writeEvents.ToList();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Deserialize a payload to a Gdto.
        /// </summary>
        public Gdto DeserializeGdto(string payload)
        {
            try
            {
                return JsonConvert.DeserializeObject<Gdto>(payload);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }
    }
}