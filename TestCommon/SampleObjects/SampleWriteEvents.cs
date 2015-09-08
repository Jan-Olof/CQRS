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

        public static WriteEvent CreateWriteEventDeleteMovie()
        {
            return new WriteEvent
            {
                Id = 4,
                CommandType = CommandType.Delete,
                Timestamp = SystemTime.UtcNow,
                Version = 1,
                Payload = Payload2001()
            };
        }

        /// <summary>
        /// The create write events.
        /// </summary>
        public static IList<WriteEvent> CreateWriteEvents()
        {
            return new List<WriteEvent>
                       {
                           CreateWriteEvent(1, PayloadSapiens()),
                           CreateWriteEvent(2, Payload2001()),
                       };
        }

        public static IList<WriteEvent> CreateWriteEventsDelete()
        {
            return new List<WriteEvent>
                       {
                          CreateWriteEventDeleteMovie()
                       };
        }

        public static IList<WriteEvent> CreateWriteEventsUpdate()
        {
            return new List<WriteEvent>
                       {
                          CreateWriteEventUpdateBook()
                       };
        }

        public static WriteEvent CreateWriteEventUpdateBook()
        {
            return new WriteEvent
            {
                Id = 3,
                CommandType = CommandType.Update,
                Timestamp = SystemTime.UtcNow,
                Version = 1,
                Payload = PayloadSapiens()
            };
        }

        public static string Payload2001()
        {
            return "{\"EntityType\": \"Movie\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"2001: A Space Odyssey\"},{\"Key\": \"Author\",\"Value\": \"Stanley Kubrick\"},{\"Key\": \"Published\",\"Value\": \"1968\"}	]}";
        }

        public static string PayloadSapiens()
        {
            return "{\"EntityType\": \"Book\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"Sapiens\"},{\"Key\": \"Author\",\"Value\": \"Yuval Noah Harari\"},{\"Key\": \"Published\",\"Value\": \"2014\"}	]}";
        }
    }
}