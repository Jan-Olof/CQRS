namespace WriteToRead.Interfaces
{
    using System.Collections.Generic;

    using Common.DataTransferObjects;

    using Domain.Write.Entities;
    using Domain.Write.Interfaces;

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
        
        /// <summary>
        /// Get the value of the name property in a Gdto.
        /// </summary>
        string GetNamePropertyValue(Gdto gdto);

        /// <summary>
        /// Set sent to read to a new number.
        /// </summary>
        bool SetSentToRead(IWriteEvent writeEvent, int sentToRead);
    }
}