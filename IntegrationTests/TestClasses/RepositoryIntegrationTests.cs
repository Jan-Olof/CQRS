namespace Tests.IntegrationTests.TestClasses
{
    using Domain.Write.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using Tests.IntegrationTests.Initialize;
    using Tests.TestCommon;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The repository tests.
    /// </summary>
    [TestClass]
    public class RepositoryIntegrationTests : BaseTestDb
    {
        /// <summary>
        /// The test should get all write events.
        /// </summary>
        [TestMethod]
        public void TestShouldGetAllWriteEvents()
        {
            // Arrange
            var repository = Factory.CreateWriteEventRepository();
            var insert = repository.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"));
            Assert.IsTrue(insert.Id > 0);

            var sut = Factory.CreateWriteEventRepository();

            // Act
            var result = sut.GetAll();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Sample payload", result.Single().Payload);
        }

        /// <summary>
        /// The test should get one write event.
        /// </summary>
        [TestMethod]
        public void TestShouldGetOneWriteEvent()
        {
            // Arrange
            var repository = Factory.CreateWriteEventRepository();
            var insert = repository.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"));
            Assert.IsTrue(insert.Id > 0);

            var sut = Factory.CreateWriteEventRepository();

            // Act
            var result = sut.GetOne(insert.Id);

            // Assert
            Assert.AreEqual("Sample payload", result.Payload);
        }

        /// <summary>
        /// The test should insert write event.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertWriteEvent()
        {
            // Arrange
            var sut = Factory.CreateWriteEventRepository();

            // Act
            var result = sut.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"));

            // Assert
            Assert.IsTrue(result.Id > 0);
        }

        /// <summary>
        /// The test should insert write event and throw argument null exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertWriteEventAndThrowArgumentNullException()
        {
            // Arrange
            var sut = Factory.CreateWriteEventRepository();

            // Act & Assert
            MyAssert.Throws<ArgumentNullException>(() => sut.Insert((WriteEvent)null));
        }

        /// <summary>
        /// The test should save all changes.
        /// </summary>
        [TestMethod]
        public void TestShouldSaveAllChanges()
        {
            // Arrange
            var arr = Factory.CreateWriteEventRepository();
            var writeEvent = arr.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"));
            writeEvent.Payload = "Updated payload";

            var sut = Factory.CreateWriteEventRepository();

            // Act
            var result = sut.SaveAllChanges();

            // Assert
            Assert.IsTrue(result);
        }
    }
}