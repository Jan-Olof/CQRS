namespace Tests.TestCommon.SampleObjects
{
    using Common.Enums;
    using Common.Utilities;

    using Domain.Write.Entities;

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
    }
}