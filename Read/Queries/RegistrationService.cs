namespace Domain.Read.Queries
{
    using Common.DataAccess;
    using Domain.Read.Entities;
    using Domain.Read.Interfaces;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The registration service.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<Registration> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService"/> class.
        /// </summary>
        public RegistrationService(IRepository<Registration> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Get all registrations.
        /// </summary>
        public IList<Registration> GetAllRegistrations()
        {
            try
            {
                return this.repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get one registration.
        /// </summary>
        public Registration GetRegistration(int id)
        {
            try
            {
                return this.repository.GetOne(id);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get all registrations of a certain type.
        /// </summary>
        public IList<Registration> GetRegistrations(int type)
        {
            try
            {
                return Registration.GetRegistrationsOfOneType(this.repository, type).ToList();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }
    }
}