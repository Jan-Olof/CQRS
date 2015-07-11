namespace Domain.Write.Interfaces
{
    /// <summary>
    /// The WriteToStore interface.
    /// </summary>
    public interface IWriteToStore
    {
        /// <summary>
        /// The insert into event store.
        /// </summary>
        IWriteEvent InsertIntoEventStore(IWriteEvent writeEvent);
    }
}