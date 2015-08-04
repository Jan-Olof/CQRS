namespace Api.WebApi
{
    using Common.DataAccess;
    using Common.DataTransferObjects;
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using DataAccess.Write.Dal.CodeFirst.DbContext;
    using Domain.Read.Entities;
    using Domain.Read.Interfaces;
    using Domain.Read.Queries;
    using Domain.Write.Commands;
    using Domain.Write.Entities;
    using Domain.Write.Interfaces;
    using Domain.Write.Store;
    using Microsoft.Practices.Unity;
    using System.Data.Entity;
    using System.Web.Http;

    /// <summary>
    /// The web api config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// The register.
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config = ConfigureDependencyInjection(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        /// <summary>
        /// The configur dependency injection.
        /// </summary>
        private static HttpConfiguration ConfigureDependencyInjection(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<ICommandService, CommandService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICreateStoreObject<Gdto>, CreateStoreObject<Gdto>>(new HierarchicalLifetimeManager());
            container.RegisterType<IWriteToStore, WriteToStore>(new HierarchicalLifetimeManager());

            container.RegisterType<IRegistrationService, RegistrationService>(new HierarchicalLifetimeManager());

            container.RegisterType<DbContext, ReadContext>("Read", new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, WriteContext>("Write", new HierarchicalLifetimeManager());

            container.RegisterType<IRepository<WriteEvent>, Repository<WriteEvent>>(
                new HierarchicalLifetimeManager(), new InjectionConstructor(new ResolvedParameter<DbContext>("Write")));
            container.RegisterType<IRepository<Registration>, Repository<Registration>>(
                new HierarchicalLifetimeManager(), new InjectionConstructor(new ResolvedParameter<DbContext>("Read")));

            config.DependencyResolver = new UnityResolver(container);

            return config;
        }
    }
}