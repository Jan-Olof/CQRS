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
        /// The create property type repository.
        /// </summary>
        public static Repository<PropertyType> CreatePropertyTypeRepository()
        {
            var readContext = new ReadContext();

            return new Repository<PropertyType>(readContext);
        }

        /// <summary>
        /// The create property repository.
        /// </summary>
        public static Repository<Property> CreatePropertyRepository()
        {
            var readContext = new ReadContext();

            return new Repository<Property>(readContext);
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

        /// <summary>
        /// The create write event service.
        /// </summary>
        public static WriteEventService CreateWriteEventService()
        {
            return new WriteEventService(CreateWriteEventRepository());
        }

        /// <summary>
        /// The create generic registration service.
        /// </summary>
        public static GenericRegistrationRepository CreateGenericRegistrationService()
        {
            return new GenericRegistrationRepository(new ReadContext());
        }

        /// <summary>
        /// The create write to read service.
        /// </summary>
        public static WriteToReadService CreateWriteToReadService()
        {
            return new WriteToReadService(CreateWriteEventService(), CreateGenericRegistrationService());
        }
    }
}