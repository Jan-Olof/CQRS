namespace Tests.IntegrationTests.TestClasses
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using Tests.IntegrationTests.Initialize;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The write to read service integration tests.
    /// </summary>
    [TestClass]
    public class WriteToReadServiceIntegrationTests : BaseTestDb
    {
        /// <summary>
        /// The test should perform ETL from Write db to Read db.
        /// </summary>
        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDb()
        {
            // Arrange
            InsertWriteEventsIntoDb();

            var sut = Factory.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(new DateTime(2015, 7, 15, 17, 37, 17));

            // Assert
            Assert.AreEqual(2, result);

            var registrations = Factory.CreateRegistrationRepository().GetAll().ToList();
            var sapiens = registrations.Single(r => r.Name == "Sapiens");
            var two001 = registrations.Single(r => r.Name == "2001: A Space Odyssey");

            Assert.AreEqual("Book", sapiens.RegistrationType.Name);
            Assert.AreEqual("Movie", two001.RegistrationType.Name);

            Assert.AreEqual("Yuval Noah Harari", sapiens.Properties.Single(p => p.PropertyType.Name == "Author").Value);
            Assert.AreEqual("Stanley Kubrick", two001.Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDbDelete()
        {
            // Arrange
            DoInsertEtl();
            InsertWriteEventsIntoDbForDelete();
            var sut = Factory.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(new DateTime(2015, 9, 15, 17, 37, 17));

            // Assert
            Assert.AreEqual(1, result);

            var registrations = Factory.CreateRegistrationRepository().GetAll().ToList();
            var two001 = registrations.SingleOrDefault(r => r.Name == "2001: A Space Odyssey");

            Assert.IsNull(two001);
        }

        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDbUpdate()
        {
            // Arrange
            DoInsertEtl();
            InsertWriteEventsIntoDbForUpdate();
            var sut = Factory.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(new DateTime(2015, 8, 15, 17, 37, 17));

            // Assert
            Assert.AreEqual(1, result);

            var registrations = Factory.CreateRegistrationRepository().GetAll().ToList();
            var sapiens = registrations.Single(r => r.Name == "Sapiens 2");

            Assert.AreEqual("Book", sapiens.RegistrationType.Name);
            Assert.AreEqual("Yuval Noah Harari", sapiens.Properties.Single(p => p.PropertyType.Name == "Author").Value);
            Assert.AreEqual("2016", sapiens.Properties.Single(p => p.PropertyType.Name == "Published").Value);
        }

        private static void DoInsertEtl()
        {
            InsertWriteEventsIntoDb();

            Factory.CreateWriteToReadService().EtlFromWriteDbToReadDb(new DateTime(2015, 7, 15, 17, 37, 17));
        }

        private static void InsertWriteEventsIntoDb()
        {
            Factory.CreateWriteEventRepository().Insert(SampleWriteEvents.CreateWriteEvents());
        }

        private static void InsertWriteEventsIntoDbForDelete()
        {
            Factory.CreateWriteEventRepository().Insert(SampleWriteEvents.CreateWriteEventDeleteMovie());
        }

        private static void InsertWriteEventsIntoDbForUpdate()
        {
            Factory.CreateWriteEventRepository().Insert(SampleWriteEvents.CreateWriteEventUpdateBook());
        }
    }
}