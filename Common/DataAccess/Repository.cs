﻿namespace Common.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;

    using Common.Exceptions;

    using NLog;

    /// <summary>
    /// The generic repository for Entity Framework (DbContext).
    /// </summary>
    /// <typeparam name="T">
    /// The data model to use.
    /// </typeparam>
    public class Repository<T> : IRepository<T>
         where T : class
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        public Repository(DbContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            this.DbContext = dataContext;
            this.DbSet = dataContext.Set<T>();
        }

        /// <summary>
        /// Gets or sets the DbContext.
        /// </summary>
        private DbContext DbContext { get; set; }

        /// <summary>
        /// Gets or sets the DbSet.
        /// </summary>
        private DbSet<T> DbSet { get; set; }

        /// <summary>
        /// Get all data objects in the model.
        /// </summary>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> result = null;

            try
            {
                var retryCount = 3;
                while (retryCount > 0)
                {
                    try
                    {
                        result = this.DbSet;
                        break;
                    }
                    catch (EntityCommandExecutionException)
                    {
                        if (!this.HandleEntityCommandExecutionException(ref retryCount))
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Get one data model object.
        /// </summary>
        public T GetOne(int id)
        {
            T result = null;
            try
            {
                var retryCount = 3;
                while (retryCount > 0)
                {
                    try
                    {
                        result = this.DbSet.Find(id);
                        break;
                    }
                    catch (EntityCommandExecutionException)
                    {
                        if (!this.HandleEntityCommandExecutionException(ref retryCount))
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Insert one data model object.
        /// </summary>
        public T Insert(T dataModel)
        {
            var retryCount = 3;
            while (retryCount > 0)
            {
                using (var transaction = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        this.DbSet.Add(dataModel);
                        this.DbContext.SaveChanges();

                        transaction.Commit();
                        break;
                    }
                    catch (SqlException sqlException)
                    {
                        transaction.Rollback();

                        if (!this.HandleSqlException(sqlException, ref retryCount))
                        {
                            throw;
                        }
                    }
                    catch (DbUpdateException updateException)
                    {
                        transaction.Rollback();

                        HandleInnerExceptionDuplicateKey(updateException);

                        throw;
                    }
                    catch (DbEntityValidationException vex)
                    {
                        transaction.Rollback();

                        this.LogValidationException(vex);

                        this.logger.Error(vex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        this.logger.Error(ex);
                        throw;
                    }
                }
            }

            return dataModel;
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
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The handle inner exception duplicate key.
        /// </summary>
        private static void HandleInnerExceptionDuplicateKey(Exception updateException)
        {
            var inner = updateException.InnerException;
            if (inner != null)
            {
                if (inner.Message.Contains("Cannot insert duplicate key row"))
                {
                    throw new DuplicateKeyValueException(inner.Message);
                }

                HandleInnerExceptionDuplicateKey(inner);
            }
        }

        /// <summary>
        /// The handle entity command execution exception.
        /// </summary>
        private bool HandleEntityCommandExecutionException(ref int retryCount)
        {
            retryCount--;
            if (retryCount == 0)
            {
                this.logger.Warn("HandleEntityCommandExecutionException retryCount == 0");
                return false;
            }

            Thread.Sleep(1000);
            return true;
        }

        /// <summary>
        /// Log a DbEntityValidationExceptionn.
        /// </summary>
        private void LogValidationException(DbEntityValidationException vex)
        {
            foreach (var validationResult in vex.EntityValidationErrors)
            {
                foreach (var validationError in validationResult.ValidationErrors)
                {
                    var entity = validationResult.Entry.Entity.ToString();
                    var errorMessage = validationError.ErrorMessage;

                    this.logger.Error("Validation error in entity: {0} Error message: {1}", entity, errorMessage);
                }
            }
        }

        /// <summary>
        /// The handle sql exception.
        /// </summary>
        private bool HandleSqlException(SqlException exception, ref int retryCount)
        {
            if (exception.Number != 1205)
            {
                // A sql exception that is not a deadlock  
                this.logger.Error(exception);
                return false;
            }

            // Handle deadlock.
            retryCount--;
            if (retryCount == 0)
            {
                this.logger.Error(exception);
                return false;
            }

            Thread.Sleep(1000);
            return true;
        }
    }
}