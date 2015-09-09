namespace Domain.Write.Entities
{
    using Common.DataAccess;
    using Common.Enums;
    using Domain.Write.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// The write event.
    /// </summary>
    public class WriteEvent : IWriteEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEvent"/> class.
        /// </summary>
        public WriteEvent()
        {
            this.Payload = string.Empty;
        }

        /// <summary>
        /// Gets or sets the command type.
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets the sent to read.
        /// </summary>
        public int SentToRead { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Get write events not sent to read.
        /// </summary>
        public static IQueryable<WriteEvent> GetWriteEventsNotSentToRead(IRepository<WriteEvent> repository, int timesSent)
        {
            return repository
                .GetAll()
                .Where(w => w.SentToRead <= timesSent)
                .OrderBy(w => w.SentToRead);
        }
    }
}