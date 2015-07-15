namespace Tests.IntegrationTests.Initialize
{
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Web.Http;

    using Api.WebApi.Controllers;

    using Common.DataAccess;
    using Common.DataTransferObjects;

    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using DataAccess.Write.Dal.CodeFirst.DbContext;

    using Domain.Read.Entities;
    using Domain.Read.Queries;
    using Domain.Write.Commands;
    using Domain.Write.Entities;
    using Domain.Write.Store;

    using WriteToRead;
    using WriteToRead.FromWriteDb;
    using WriteToRead.ToReadDb;

    /// <summary>
    /// The factory.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// The create write event repository.
        /// </summary>
        public static Repository<WriteEvent> CreateWriteEventRepository()
        {
            var writeContext = new WriteContext();

            return new Repository<WriteEvent>(writeContext);
        }

        /// <summary>
        /// The create registration repository.
        /// </summary>
        public static Repository<Registration> CreateRegistrationRepository()
        {
            var readContext = new ReadContext();

            return new Repository<Registration>(readContext);
        }

        /// <summary>
        /// The create registration type repository.
        /// </summary>
        public static Repository<RegistrationType> CreateRegistrationTypeRepository()
        {
            var readContext = new ReadContext();

            return new Repository<RegistrationType>(readContext);
        }


        /// <summary>
        /// The create command services.
        /// </summary>
        public static CommandService CreateCommandServices()
        {
            return new CommandService(
                new CreateStoreObject<Gdto>(),
                new WriteToStore(CreateWriteEventRepository()));
        }

        /// <summary>
        /// The create registration service.
        /// </summary>
        public static RegistrationService CreateRegistrationService()
        {
            return new RegistrationService(CreateRegistrationRepository());
        }

        /// <summary>
        /// The create registration controller.
        /// </summary>
        public static RegistrationController CreateRegistrationController()
        {
            var controller = new RegistrationController(CreateCommandServices(), CreateRegistrationService());

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();
            controller.Request.RequestUri = new Uri(string.Concat(ConfigurationManager.AppSettings.Get("WebApiAddress"), "/api/"));

            return controller;
        }

        public static WriteToReadService CreateWriteToReadService()
        {
           throw new System.NotImplementedException();
            //return new WriteToReadService(new WriteEventService(CreateWriteEventRepository()),new GenericRegistrationService() );
        }
    }
}