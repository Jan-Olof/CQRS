namespace Domain.Write.Commands
{
    using System;

    using Common.DataTransferObjects;
    using Common.Enums;
    using Common.Exceptions;

    using Domain.Write.Interfaces;

    using NLog;

    /// <summary>
    /// The command service.
    /// </summary>
    public class CommandService : ICommandService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The create store object.
        /// </summary>
        private readonly ICreateStoreObject<Gdto> createStoreObject;

        /// <summary>
        /// The write to store.
        /// </summary>
        private readonly IWriteToStore writeToStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        public CommandService(ICreateStoreObject<Gdto> createStoreObject, IWriteToStore writeToStore)
        {
            if (createStoreObject == null)
            {
                throw new ArgumentNullException("createStoreObject");
            }

            if (writeToStore == null)
            {
                throw new ArgumentNullException("writeToStore");
            }

            this.createStoreObject = createStoreObject;
            this.writeToStore = writeToStore;
        }

        /// <summary>
        /// Insert a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        public int Insert(Gdto dto)
        {
            try
            {
                var eventStore = this.createStoreObject.CreateWriteEvent(dto, CommandType.Insert);

                return this.writeToStore.InsertIntoEventStore(eventStore).Id;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw new InsertException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update a registration by inserting a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        public int Update(Gdto dto)
        {
            try
            {
                var eventStore = this.createStoreObject.CreateWriteEvent(dto, CommandType.Update);

                return this.writeToStore.InsertIntoEventStore(eventStore).Id;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw new UpdateException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete a registration by inserting a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        public int Delete(Gdto dto)
        {
            try
            {
                var eventStore = this.createStoreObject.CreateWriteEvent(dto, CommandType.Delete);

                return this.writeToStore.InsertIntoEventStore(eventStore).Id;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw new DeleteException(ex.Message, ex);
            }
        }
    }
}