namespace DataAccess.Write.Dal.CodeFirst.DbContext
{
    using Domain.Write.Entities;
    using System.Data.Entity;

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