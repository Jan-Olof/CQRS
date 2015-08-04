namespace Tests.TestCommon.SampleObjects
{
    using Common.Enums;
    using Common.Utilities;
    using Domain.Write.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// The sample write events.
    /// </summary>
    public static class SampleWriteEvents
    {
        /// <summary>
        /// The create write event.
        /// </summary>
        public static WriteEvent CreateWriteEvent(int id, string payload)
        {
            return new WriteEvent
            {
                Id = id,
                CommandType = CommandType.Insert,
                Timestamp = SystemTime.UtcNow,
                Version = 1,
                Payload = payload
            };
        }

        /// <summary>
        /// The create write events.
        /// </summary>
        public static IList<WriteEvent> CreateWriteEvents()
        {
            return new List<WriteEvent>
                       {
                           CreateWriteEvent(1, "{\"EntityType\": \"Book\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"Sapiens\"},{\"Key\": \"Author\",\"Value\": \"Yuval Noah Harari\"},{\"Key\": \"Published\",\"Value\": \"2014\"}	]}"),
                           CreateWriteEvent(2, "{\"EntityType\": \"Movie\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"2001: A Space Odyssey\"},{\"Key\": \"Author\",\"Value\": \"Stanley Kubrick\"},{\"Key\": \"Published\",\"Value\": \"1968\"}	]}"),
                       };
        }
    }
}