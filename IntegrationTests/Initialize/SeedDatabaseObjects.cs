namespace Tests.IntegrationTests.Initialize
{
    using System.Data.Entity.Migrations;

    using DataAccess.Read.Dal.CodeFirst.DbContext;

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

            context.RegistrationPropertys.AddOrUpdate(r => r.Id, SampleRegistrationProperties.CreateRegistrationPropertyHarariDb());
            context.RegistrationPropertys.AddOrUpdate(r => r.Id, SampleRegistrationProperties.CreateRegistrationProperty2014Db());
            context.RegistrationPropertys.AddOrUpdate(r => r.Id, SampleRegistrationProperties.CreateRegistrationPropertyKubrickDb());
            context.RegistrationPropertys.AddOrUpdate(r => r.Id, SampleRegistrationProperties.CreateRegistrationProperty1968Db());

            context.Registrations.AddOrUpdate(r => r.Id, SampleRegistrations.CreateRegistrationSapiensDb());
            context.Registrations.AddOrUpdate(r => r.Id, SampleRegistrations.CreateRegistration2001Db());

            context.SaveChanges();
        }
    }
}