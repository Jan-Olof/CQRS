namespace Tests.IntegrationTests.Initialize
{
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using System.Data.Entity;

    /// <summary>
    /// The read initializer.
    /// </summary>
    public class ReadInitializer : DropCreateDatabaseAlways<ReadContext>
    {
        // Seed the data here.
    }
}