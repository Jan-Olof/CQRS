namespace WriteToReadEtl
{
    using Common.DataAccess;
    using Common.Utilities;

    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using DataAccess.Write.Dal.CodeFirst.DbContext;

    using Domain.Write.Entities;

    using NLog;

    using WriteToRead;
    using WriteToRead.FromWriteDb;
    using WriteToRead.ToReadDb;

    /// <summary>
    /// Extract data from Write db, tranform it and load it in the Read db.
    /// </summary>
    public static class FromWriteDbToReadDb
    {
        /// <summary>
        /// Load the read db.
        /// </summary>
        public static string LoadTheReadDb()
        {
            var logger = LogManager.GetCurrentClassLogger();

            var writeToReadService = new WriteToReadService(
                new WriteEventService(new Repository<WriteEvent>(new WriteContext())),
                new WriteToReadRepository(new ReadContext()));

            var eventsRegistered = writeToReadService.EtlFromWriteDbToReadDb(SystemTime.UtcNow);

            string infoMsg = $"Saved {eventsRegistered} new events to ReadDb.";
            logger.Info(infoMsg);
            return infoMsg;
        }
    }
}