namespace Domain.Write.Interfaces
{
    using Common.Enums;

    /// <summary>
    /// The CreateStoreObject interface.
    /// </summary>
    public interface ICreateStoreObject<T> where T : class
    {
        /// <summary>
        /// The create write event.
        /// </summary>
        IWriteEvent CreateWriteEvent(T payload, CommandType commandType);
    }
}