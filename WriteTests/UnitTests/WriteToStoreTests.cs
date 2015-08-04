namespace Tests.WriteTests.UnitTests
{
    using Common.DataAccess;
    using Common.Exceptions;
    using Common.Utilities;
    using Domain.Write.Entities;
    using Domain.Write.Store;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using TestCommon;
    using TestCommon.SampleObjects;

    /// <summary>
    /// The write to store tests.
    /// </summary>
    [TestClass]
    public class WriteToStoreTests
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
        /// The test should insert into event store.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertIntoEventStore()
        {
            // Arrange
            var sut = this.CreateWriteToStore();

            this.repository.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(34, "Sample payload"));

            // Act
            var result = sut.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"));

            // Assert
            Assert.IsTrue(result.Id > 0);
        }

        /// <summary>
        /// The test should insert into event store and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertIntoEventStoreAndThrowException()
        {
            // Arrange
            var sut = new WriteToStore(this.repository);

            this.repository.Insert(SampleWriteEvents.CreateWriteEvent(0, "Sample payload"))
                .ReturnsForAnyArgs(x => { throw new Exception(); });

            // Act & Assert
            MyAssert.Throws<WriteToStoreException>(() => sut.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "Sample payload")));
        }

        /// <summary>
        /// The create write to store.
        /// </summary>
        private WriteToStore CreateWriteToStore()
        {
            return new WriteToStore(this.repository);
        }
    }
}