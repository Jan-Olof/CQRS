namespace DataAccess.Write.Dal.CodeFirst.DbContext
{
    using System.Data.Entity;

    using Domain.Write.Entities;

    /// <summary>
    /// The write context.
    /// </summary>
    public class WriteContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteContext"/> class.
        /// </summary>
        public WriteContext()
            : base("WriteContext")
        {
        }

        /// <summary>
        /// Gets or sets the write events.
        /// </summary>
        public DbSet<WriteEvent> WriteEvents { get; set; }
    }
}