namespace Domain.Read.Interfaces
{
    using Domain.Read.Entities;
    using System.Collections.Generic;

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
        /// Get one registration.
        /// </summary>
        Registration GetRegistration(int id);

        /// <summary>
        /// Get all registrations of a certain type.
        /// </summary>
        IList<Registration> GetRegistrations(int type);
    }
}