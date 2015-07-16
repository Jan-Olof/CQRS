namespace Common.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;
    using System.Threading;

    using NLog;

    /// <summary>
    /// The base repository.
    /// </summary>
    public abstract class BaseRepository : IDisposable
    {
        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        protected BaseRepository(DbContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            this.DbContext = dataContext;
        }
        
        /// <summary>
        /// Gets or sets the DbContext.
        /// </summary>
        protected DbContext DbContext { get; set; }

        /// <summary>
        /// Save all changes in DbContext to the database
        /// </summary>
        public bool SaveAllChanges()
        {
            var result = false;
            var retryCount = 3;
            while (retryCount > 0)
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        this.DbContext.SaveChanges();

                        transaction.Commit();

                        result = true;
                        break;
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();

                        if (!this.HandleSqlException(exception, ref retryCount))
                        {
                            throw;
                        }
                    }
                    catch (DbEntityValidationException vex)
                    {
                        transaction.Rollback();

                        this.LogValidationException(vex);

                        this.Logger.Error(vex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        this.Logger.Error(ex);
                        throw;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (this.DbContext != null)
                {
                    this.DbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The handle sql exception.
        /// </summary>
        protected bool HandleSqlException(SqlException exception, ref int retryCount)
        {
            if (exception.Number != 1205)
            {
                // A sql exception that is not a deadlock  
                this.Logger.Error(exception);
                return false;
            }

            // Handle deadlock.
            retryCount--;
            if (retryCount == 0)
            {
                this.Logger.Error(exception);
                return false;
            }

            Thread.Sleep(1000);
            return true;
        }

        /// <summary>
        /// Log a DbEntityValidationExceptionn.
        /// </summary>
        protected void LogValidationException(DbEntityValidationException vex)
        {
            foreach (var validationResult in vex.EntityValidationErrors)
            {
                foreach (var validationError in validationResult.ValidationErrors)
                {
                    var entity = validationResult.Entry.Entity.ToString();
                    var errorMessage = validationError.ErrorMessage;

                    this.Logger.Error("Validation error in entity: {0} Error message: {1}", entity, errorMessage);
                }
            }
        }
    }
}