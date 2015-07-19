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
    /// The work class.
    /// </summary>
    public static class WorkClass
    {
        /// <summary>
        /// The do the work.
        /// </summary>
        public static string DoTheWork()
        {
            var logger = LogManager.GetCurrentClassLogger();

            var writeToReadService = new WriteToReadService(
                new WriteEventService(new Repository<WriteEvent>(new WriteContext())),
                new GenericRegistrationRepository(new ReadContext()));

            var eventsRegistered = writeToReadService.AddToGenericRegistrationReadDb(SystemTime.UtcNow);

            string infoMsg = string.Format("Saved {0} new events to ReadDb.", eventsRegistered);
            logger.Info(infoMsg);
            return infoMsg;
        }
    }
}