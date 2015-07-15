namespace WriteToRead.Interfaces
{
    using System;

    /// <summary>
    /// The WriteToReadService interface.
    /// </summary>
    public interface IWriteToReadService
    {
        /// <summary>
        /// Add to the generic registration read database.
        /// </summary>
        int AddToGenericRegistrationReadDb(DateTime timestamp);
    }
}