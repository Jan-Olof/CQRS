namespace WriteToRead
{
    using System;
    using System.Collections.Generic;

    using Domain.Write.Entities;

    using NLog;

    using WriteToRead.Interfaces;

    /// <summary>
    /// Class that is used to transfer data from the Write database to the Read database.
    /// </summary>
    public class ToGenericRegistration : IToGenericRegistration
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Add to generic registration database from a list of WriteEvents.
        /// </summary>
        public bool AddToGenericRegistrationDatabase(IList<WriteEvent> writeEvents)
        {
            throw new NotImplementedException();
        }
    }
}