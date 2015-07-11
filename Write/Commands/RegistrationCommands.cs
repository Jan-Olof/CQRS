namespace Domain.Write.Commands
{
    using Common.DataTransferObjects;

    using Domain.Write.Interfaces;

    /// <summary>
    /// The command.
    /// </summary>
    public abstract class RegistrationCommands<T> : IRegistrationCommands<T>
    {
        /// <summary>
        /// The insert.
        /// </summary>
        public abstract T Insert(Gdto dto);

        /// <summary>
        /// The update.
        /// </summary>
        public abstract T Update(Gdto dto);

        /// <summary>
        /// The delete.
        /// </summary>
        public abstract T Delete(Gdto dto);
    }
}