namespace Tests.IntegrationTests.Initialize
{
    using System.Data.Entity;

    using DataAccess.Read.Dal.CodeFirst.DbContext;

    /// <summary>
    /// The read initializer.
    /// </summary>
    public class ReadInitializer : DropCreateDatabaseAlways<ReadContext>
    {
         // Seed the data here.
    }
}