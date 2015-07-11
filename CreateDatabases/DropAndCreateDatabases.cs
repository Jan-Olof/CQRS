namespace Utilities.CreateDatabases
{
    using System;
    using System.Diagnostics;

    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using DataAccess.Read.Dal.CodeFirst.Initialize;
    using DataAccess.Write.Dal.CodeFirst.DbContext;
    using DataAccess.Write.Dal.CodeFirst.Initialize;

    using NLog;

    /// <summary>
    /// The drop and create databases.
    /// </summary>
    public class DropAndCreateDatabases
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The drop and create write database.
        /// </summary>
        public bool DropAndCreateWriteDatabase()
        {
            try
            {
                System.Data.Entity.Database.SetInitializer(new WriteInitializer());
                var writeContext = new WriteContext();
                writeContext.Database.Initialize(true);

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// The drop and create read database.
        /// </summary>
        public bool DropAndCreateReadDatabase()
        {
            try
            {
                System.Data.Entity.Database.SetInitializer(new ReadInitializer());
                var readContext = new ReadContext();
                readContext.Database.Initialize(true);

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}