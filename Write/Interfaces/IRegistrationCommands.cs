namespace Domain.Write.Interfaces
{
    using Common.DataTransferObjects;

    /// <summary>
    /// The RegistrationCommands interface.
    /// </summary>
    public interface IRegistrationCommands<T>
    {
        /// <summary>
        /// The delete.
        /// </summary>
        T Delete(Gdto dto);

        /// <summary>
        /// The insert.
        /// </summary>
        T Insert(Gdto dto);

        /// <summary>
        /// The update.
        /// </summary>
        T Update(Gdto dto);
    }
}