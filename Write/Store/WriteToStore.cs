namespace Domain.Write.Store
{
    using Common.DataAccess;
    using Common.Exceptions;
    using Domain.Write.Entities;
    using Domain.Write.Interfaces;
    using NLog;
    using System;

    /// <summary>
    /// The write to store.
    /// </summary>
    public class WriteToStore : IWriteToStore
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
        /// Initializes a new instance of the <see cref="WriteToStore"/> class.
        /// </summary>
        public WriteToStore(IRepository<WriteEvent> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// The insert into event store.
        /// </summary>
        public IWriteEvent InsertIntoEventStore(IWriteEvent writeEvent)
        {
            try
            {
                return this.repository.Insert((WriteEvent)writeEvent);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw new WriteToStoreException(ex.Message, ex);
            }
        }
    }
}