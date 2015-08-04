namespace DataAccess.Write.Dal.CodeFirst.Initialize
{
    using DataAccess.Write.Dal.CodeFirst.DbContext;
    using System.Data.Entity;

    /// <summary>
    /// The write initializer.
    /// </summary>
    public class WriteInitializer : DropCreateDatabaseAlways<WriteContext>
    {
        // Seed the data here.
    }
}