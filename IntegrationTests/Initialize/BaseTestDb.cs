namespace Tests.IntegrationTests.Initialize
{
    using Common.Utilities;
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using DataAccess.Write.Dal.CodeFirst.DbContext;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Configuration;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;

    /// <summary>
    /// The base test db.
    /// </summary>
    public abstract class BaseTestDb
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private readonly DateTime timeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// Gets or sets the read context.
        /// </summary>
        private ReadContext ReadContext { get; set; }

        /// <summary>
        /// Gets or sets the write context.
        /// </summary>
        private WriteContext WriteContext { get; set; }

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.InitDatabaseBeforeTests();

            SystemTime.Set(this.timeStamp);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            this.WriteContext.Dispose();
            this.ReadContext.Dispose();

            SystemTime.Reset();
        }

        /// <summary>
        /// The check database.
        /// </summary>
        private static bool CheckDatabase(string database)
        {
            bool exists;
            string selectDatabase = string.Format("SELECT database_id FROM sys.databases WHERE Name=\'{0}\'", database);

            using (var masterConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Master"].ConnectionString))
            {
                masterConnection.Open();

                using (var sqlCmd = new SqlCommand(selectDatabase, masterConnection))
                {
                    try
                    {
                        var databaseId = (int)sqlCmd.ExecuteScalar();
                        exists = databaseId > 0;
                    }
                    catch (NullReferenceException)
                    {
                        exists = false;
                    }
                }

                masterConnection.Close();
            }

            return exists;
        }

        /// <summary>
        /// The fix ef provider services problem.
        /// </summary>
        private static void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded.
            // Make sure the provider assembly is available to the running application.
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            // ReSharper disable UnusedVariable
            var instance = SqlProviderServices.Instance;

            // ReSharper restore UnusedVariable
        }

        /// <summary>
        /// The remove existing database.
        /// </summary>
        private static void RemoveExistingDatabase(string database, string context)
        {
            if (CheckDatabase(database))
            {
                var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[context].ConnectionString);
                var serverConnection = new ServerConnection(sqlConnection);
                var server = new Server(serverConnection);

                server.KillDatabase(database);
            }
        }

        /// <summary>
        /// Initiates database before tests.
        /// </summary>
        private void InitDatabaseBeforeTests()
        {
            FixEfProviderServicesProblem();

            RemoveExistingDatabase("Write", "WriteContext");
            RemoveExistingDatabase("Read", "ReadContext");

            System.Data.Entity.Database.SetInitializer(new WriteInitializer());
            this.WriteContext = new WriteContext();
            this.WriteContext.Database.Initialize(true);

            System.Data.Entity.Database.SetInitializer(new ReadInitializer());
            this.ReadContext = new ReadContext();
            this.ReadContext.Database.Initialize(true);
        }
    }
}