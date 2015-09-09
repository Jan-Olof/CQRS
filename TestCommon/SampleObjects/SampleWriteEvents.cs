namespace Tests.TestCommon.SampleObjects
{
    using Common.Enums;
    using Common.Utilities;
    using Domain.Write.Entities;
    using System;
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
                Timestamp = new DateTime(2015, 9, 1),
                Version = 1,
                Payload = Payload2001Delete()
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
                Timestamp = new DateTime(2015, 8, 1),
                Version = 1,
                Payload = PayloadSapiens2Update()
            };
        }

        public static string Payload2001()
        {
            return "{\"EntityType\": \"Movie\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"2001: A Space Odyssey\"},{\"Key\": \"Author\",\"Value\": \"Stanley Kubrick\"},{\"Key\": \"Published\",\"Value\": \"1968\"}]}";
        }

        public static string Payload2001Delete()
        {
            return "{\"EntityType\": \"Movie\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"2001: A Space Odyssey\"},{\"Key\": \"Author\",\"Value\": \"Stanley Kubrick\"},{\"Key\": \"Published\",\"Value\": \"1968\"},{\"Key\": \"OriginalWriteEventId\",\"Value\": \"2\"}]}";
        }

        public static string PayloadSapiens()
        {
            return "{\"EntityType\": \"Book\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"Sapiens\"},{\"Key\": \"Author\",\"Value\": \"Yuval Noah Harari\"},{\"Key\": \"Published\",\"Value\": \"2014\"}]}";
        }

        public static string PayloadSapiens2Update()
        {
            return "{\"EntityType\": \"Book\",\"Properties\": [{\"Key\": \"Name\",\"Value\": \"Sapiens 2\"},{\"Key\": \"Author\",\"Value\": \"Yuval Noah Harari\"},{\"Key\": \"Published\",\"Value\": \"2016\"},{\"Key\": \"OriginalWriteEventId\",\"Value\": \"1\"}]}";
        }
    }
}