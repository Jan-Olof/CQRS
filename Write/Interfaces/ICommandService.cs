namespace Domain.Write.Interfaces
{
    using Common.DataTransferObjects;

    /// <summary>
    /// The CommandService interface.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Delete a registration by inserting a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        int Delete(Gdto dto);

        /// <summary>
        /// Insert a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        int Insert(Gdto dto);

        /// <summary>
        /// Update a registration by inserting a Gdto into the event store.
        /// Return an int with the regitered rvents id.
        /// </summary>
        int Update(Gdto dto);
    }
}