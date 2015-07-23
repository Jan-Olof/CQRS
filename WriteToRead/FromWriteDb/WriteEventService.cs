﻿namespace WriteToRead.FromWriteDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataAccess;
    using Common.DataTransferObjects;
    using Common.Exceptions;

    using Domain.Write.Entities;
    using Domain.Write.Interfaces;

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

        /// <summary>
        /// Get the value of a property in a Gdto.
        /// </summary>
        public string GetPropertyValue(Gdto gdto, string propertyName)
        {
            try
            {
                if (gdto.Properties == null)
                {
                    return string.Empty;
                }

                var namePropertyValue = gdto.Properties
                    .SingleOrDefault(p => string.Equals(p.Key, propertyName, StringComparison.CurrentCultureIgnoreCase))
                    .Value;

                return namePropertyValue ?? string.Empty;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Set sent to read to a new number.
        /// </summary>
        public bool SetSentToRead(IWriteEvent writeEvent, int sentToRead)
        {
            try
            {
                writeEvent.SentToRead = sentToRead;

                return this.repository.SaveAllChanges();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get the OriginalWriteEventId from the Gdto and parse it.
        /// </summary>
        public int GetOriginalWriteEventId(Gdto gdto)
        {
            string originalWriteEventIdString = this.GetPropertyValue(gdto, "OriginalWriteEventId");

            int originalWriteEventId;

            bool isOk = int.TryParse(originalWriteEventIdString, out originalWriteEventId);

            if (!isOk || originalWriteEventId < 1)
            {
                throw new NoWriteEventIdException("Could not parse OriginalWriteEventId.");
            }

            return originalWriteEventId;
        }
    }
}