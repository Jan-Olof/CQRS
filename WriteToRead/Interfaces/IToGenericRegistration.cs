namespace WriteToRead.Interfaces
{
    using System.Collections.Generic;

    using Domain.Write.Entities;

    /// <summary>
    /// The ToGenericRegistration interface.
    /// </summary>
    public interface IToGenericRegistration
    {
        /// <summary>
        /// Add to generic registration database from a list of WriteEvents.
        /// </summary>
        bool AddToGenericRegistrationDatabase(IList<WriteEvent> writeEvents);
    }
}