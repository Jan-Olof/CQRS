namespace Domain.Read.Interfaces
{
    using System.Collections.Generic;

    using Domain.Read.Entities;

    /// <summary>
    /// The RegistrationService interface.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Get all registrations.
        /// </summary>
        IList<Registration> GetAllRegistrations();

        /// <summary>
        /// Get all registrations of a certain type.
        /// </summary>
        IList<Registration> GetRegistrations(int type);

        /// <summary>
        /// Get one registration.
        /// </summary>
        Registration GetRegistration(int id);
    }
}