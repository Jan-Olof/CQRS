namespace Domain.Write.Store
{
    using System;

    using Common.Enums;
    using Common.Exceptions;
    using Common.Utilities;

    using Domain.Write.Entities;
    using Domain.Write.Interfaces;

    using Newtonsoft.Json;

    using NLog;

    /// <summary>
    /// Create a store object from a class.
    /// </summary>
    public class CreateStoreObject<T> : ICreateStoreObject<T> where T : class
    {
        /// <summary>
        /// The version.
        /// </summary>
        private const int CurrentVersion = 1;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStoreObject{T}"/> class.
        /// </summary>
        public CreateStoreObject()
        {
        }

        /// <summary>
        /// The create write event.
        /// </summary>
        public IWriteEvent CreateWriteEvent(T payload, CommandType commandType)
        {
            try
            {
                if (payload == null)
                {
                    throw new PayloadNullException("Payload is null.");
                }

                return new WriteEvent
                            {
                                Id = 0, 
                                CommandType = commandType, 
                                Version = CurrentVersion,
                                Timestamp = SystemTime.UtcNow,
                                Payload = CreatePayload(payload)
                            };
            }
            catch (PayloadNullException pex)
            {
                this.logger.Error(pex);
                throw;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw new CreateEventStoreException(ex.Message, ex);
            }
        }

        /// <summary>
        /// The create payload.
        /// </summary>
        private static string CreatePayload(T payload)
        {
            return JsonConvert.SerializeObject(payload);
        }
    }
}