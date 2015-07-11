namespace Domain.Write.Entities
{
    using System;

    using Common.Enums;

    using Domain.Write.Interfaces;

    /// <summary>
    /// The write event.
    /// </summary>
    public class WriteEvent : IWriteEvent
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the command type.
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public string Payload { get; set; }
    }
}