namespace Tests.WriteToReadTests.UnitTests
{
    using System;
    using System.Linq;

    using Common.DataAccess;
    using Common.Utilities;

    using Domain.Write.Entities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Tests.TestCommon;
    using Tests.TestCommon.SampleObjects;

    using WriteToRead.FromWriteDb;

    /// <summary>
    /// The write event service tests.
    /// </summary>
    [TestClass]
    public class WriteEventServiceTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The repository.
        /// </summary>
        private IRepository<WriteEvent> repository;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.repository = Substitute.For<IRepository<WriteEvent>>();

            SystemTime.Set(TimeStamp);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            SystemTime.Reset();
        }

        /// <summary>
        /// The test should get write events to process.
        /// </summary>
        [TestMethod]
        public void TestShouldGetWriteEventsToProcess()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            this.repository.GetAll().Returns(SampleWriteEvents.CreateWriteEvents().AsQueryable());

            // Act
            var result = sut.GetWriteEventsToProcess(0);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>
        /// The test should get write events to process and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetWriteEventsToProcessAndThrowException()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            this.repository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetWriteEventsToProcess(0));
        }

        /// <summary>
        /// The test should deserialize gdto.
        /// </summary>
        [TestMethod]
        public void TestShouldDeserializeGdto()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            // Act
            var result = sut.DeserializeGdto(SampleGdto.CreatePayload());

            // Assert
            Assert.AreEqual("Book", result.EntityType);
            Assert.AreEqual("Sapiens", result.Properties.Single(p => p.Key == "Name").Value);
        }

        /// <summary>
        /// The create write event service.
        /// </summary>
        private WriteEventService CreateWriteEventService()
        {
            return new WriteEventService(this.repository);
        }
    }
}