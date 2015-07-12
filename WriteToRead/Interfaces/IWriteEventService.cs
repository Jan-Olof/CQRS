namespace WriteToRead.Interfaces
{
    using System.Collections.Generic;

    using Common.DataTransferObjects;

    using Domain.Write.Entities;

    /// <summary>
    /// The WriteEventService interface.
    /// </summary>
    public interface IWriteEventService
    {
        /// <summary>
        /// Get the write events that needs to be processed.
        /// </summary>
        IList<WriteEvent> GetWriteEventsToProcess(int timesSent);

        /// <summary>
        /// Deserialize a payload to a Gdto.
        /// </summary>
        Gdto DeserializeGdto(string payload);
    }
}