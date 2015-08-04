namespace Domain.Write.Interfaces
{
    using Common.Enums;
    using System;

    /// <summary>
    /// The WriteEvent interface.
    /// </summary>
    public interface IWriteEvent
    {
        /// <summary>
        /// Gets or sets the command type.
        /// </summary>
        CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        string Payload { get; set; }

        /// <summary>
        /// Gets or sets the sent to read.
        /// </summary>
        int SentToRead { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        int Version { get; set; }
    }
}