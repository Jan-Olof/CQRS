namespace Tests.IntegrationTests.Initialize
{
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using Domain.Read.Entities;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The seed database objects.
    /// </summary>
    public static class SeedDatabaseObjects
    {
        /// <summary>
        /// The create registrations in db.
        /// </summary>
        public static void CreateRegistrationsInDb()
        {
            var context = new ReadContext();

            context.RegistrationTypes.AddOrUpdate(r => r.Id, SampleRegistrationTypes.CreateRegistrationTypeBook());
            context.RegistrationTypes.AddOrUpdate(r => r.Id, SampleRegistrationTypes.CreateRegistrationTypeMovie());

            context.PropertyTypes.AddOrUpdate(p => p.Id, SamplePropertyTypes.CreatePropertyTypeAuthor());
            context.PropertyTypes.AddOrUpdate(p => p.Id, SamplePropertyTypes.CreatePropertyTypePublished());

            context.Properties.AddOrUpdate(r => r.Id, SampleProperties.CreatePropertyHarariDb());
            context.Properties.AddOrUpdate(r => r.Id, SampleProperties.CreateProperty2014Db());
            context.Properties.AddOrUpdate(r => r.Id, SampleProperties.CreatePropertyKubrickDb());
            context.Properties.AddOrUpdate(r => r.Id, SampleProperties.CreateProperty1968Db());

            var sapiensProperties = new Collection<Property> { context.Properties.Find(1), context.Properties.Find(2) };
            var properties2001 = new Collection<Property> { context.Properties.Find(3), context.Properties.Find(4) };

            context.Registrations.AddOrUpdate(r => r.Id, SampleRegistrations.CreateRegistrationSapiensDb(sapiensProperties));
            context.Registrations.AddOrUpdate(r => r.Id, SampleRegistrations.CreateRegistration2001Db(properties2001));

            context.SaveChanges();
        }
    }
}