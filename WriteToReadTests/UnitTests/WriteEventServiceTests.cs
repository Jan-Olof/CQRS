namespace Tests.WriteToReadTests.UnitTests
{
    using Common.DataAccess;
    using Common.Exceptions;
    using Common.Utilities;
    using Domain.Write.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using System;
    using System.Linq;
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

        [TestMethod]
        public void TestShouldGetOriginalWriteEventId()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            // Act
            var result = sut.GetOriginalWriteEventId(SampleGdto.CreateGdtoWithWriteEventId().Properties);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestShouldGetOriginalWriteEventIdAndThrowException()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            // Act & Assert
            MyAssert.Throws<NoWriteEventIdException>(() => sut.GetOriginalWriteEventId(SampleGdto.CreateGdtoWithPublished().Properties));
        }

        /// <summary>
        /// The test should get property value.
        /// </summary>
        [TestMethod]
        public void TestShouldGetPropertyValue()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            // Act
            var result = sut.GetPropertyValue(SampleGdto.CreateGdtoWithPublished().Properties, "Name");

            // Assert
            Assert.AreEqual("Sapiens", result);
        }

        /// <summary>
        /// The test should get property value not finding any.
        /// </summary>
        [TestMethod]
        public void TestShouldGetPropertyValueNotFindingAny()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            // Act
            var result = sut.GetPropertyValue(SampleGdto.CreateGdtoWithoutName().Properties, "Name");

            // Assert
            Assert.AreEqual(string.Empty, result);
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
        /// The test should set sent to read.
        /// </summary>
        [TestMethod]
        public void TestShouldSetSentToRead()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            this.repository.SaveAllChanges().ReturnsForAnyArgs(true);

            // Act
            var result = sut.SetSentToRead(SampleWriteEvents.CreateWriteEvent(1, "Payload"), 1);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// The test should set sent to read and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldSetSentToReadAndThrowException()
        {
            // Arrange
            var sut = this.CreateWriteEventService();

            this.repository.SaveAllChanges().ThrowsForAnyArgs<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.SetSentToRead(SampleWriteEvents.CreateWriteEvent(1, "Payload"), 1));
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