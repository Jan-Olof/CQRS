namespace DataAccess.Write.Dal.CodeFirst.Initialize
{
    using System.Data.Entity;

    using DataAccess.Write.Dal.CodeFirst.DbContext;

    /// <summary>
    /// The write initializer.
    /// </summary>
    public class WriteInitializer : DropCreateDatabaseAlways<WriteContext>
    {
         // Seed the data here.
    }
}