namespace Common.DataAccess
{
    using System;
    using System.Linq;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository<T> : IDisposable
    {
        /// <summary>
        /// Get all data objects in the model.
        /// </summary>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get one data model object.
        /// </summary>
        T GetOne(int id);

        /// <summary>
        /// Insert one data model object.
        /// </summary>
        T Insert(T dataModel);
    }
}