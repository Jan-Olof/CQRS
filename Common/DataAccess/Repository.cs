namespace Common.DataAccess
{
    using Common.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The generic repository for Entity Framework (DbContext).
    /// </summary>
    /// <typeparam name="T">
    /// The data model to use.
    /// </typeparam>
    public class Repository<T> : BaseRepository, IRepository<T>
         where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        public Repository(DbContext dataContext)
            : base(dataContext)
        {
            this.DbSet = dataContext.Set<T>();
        }

        /// <summary>
        /// Gets or sets the DbSet.
        /// </summary>
        protected DbSet<T> DbSet { get; set; }

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
                this.Logger.Error(ex);
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
                this.Logger.Error(ex);
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

            return dataModel;
        }

        /// <summary>
        /// Insert a list of data model objects.
        /// </summary>
        public IList<T> Insert(IList<T> dataModelObjects)
        {
            var retryCount = 3;
            while (retryCount > 0)
            {
                using (var transaction = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        this.AddListToDbSet(dataModelObjects);
                        this.DbContext.SaveChanges();

                        transaction.Commit();
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

            return dataModelObjects;
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
        /// Adds a list of generic objects to DbSet.
        /// </summary>
        private void AddListToDbSet(IEnumerable<T> dataModelObjects)
        {
            this.DbContext.Configuration.AutoDetectChangesEnabled = false;

            foreach (var dataModelObject in dataModelObjects)
            {
                this.DbSet.Add(dataModelObject);
            }

            this.DbContext.Configuration.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// The handle entity command execution exception.
        /// </summary>
        private bool HandleEntityCommandExecutionException(ref int retryCount)
        {
            retryCount--;
            if (retryCount == 0)
            {
                this.Logger.Warn("HandleEntityCommandExecutionException retryCount == 0");
                return false;
            }

            Thread.Sleep(1000);
            return true;
        }
    }
}