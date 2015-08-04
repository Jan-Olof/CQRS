namespace Tests.WriteTests.UnitTests
{
    using Common.DataTransferObjects;
    using Common.Enums;
    using Common.Exceptions;
    using Common.Utilities;
    using Domain.Write.Entities;
    using Domain.Write.Store;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using TestCommon;
    using TestCommon.SampleObjects;

    /// <summary>
    /// The create store object tests.
    /// </summary>
    [TestClass]
    public class CreateStoreObjectTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
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
        /// The test should create event store and throw payload null exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCreateEventStoreAndThrowPayloadNullException()
        {
            // Arrange
            var sut = new CreateStoreObject<BookModel>();

            // Act & Assert
            MyAssert.Throws<PayloadNullException>(() => sut.CreateWriteEvent(null, CommandType.Insert));
        }

        /// <summary>
        /// The test should create event store from book.
        /// </summary>
        [TestMethod]
        public void TestShouldCreateEventStoreFromBook()
        {
            // Arrange
            var sut = new CreateStoreObject<BookModel>();

            // Act
            var result = sut.CreateWriteEvent(SampleBooks.CreateBook(), CommandType.Insert);

            // Assert
            var payload = JsonConvert.DeserializeObject<BookModel>(result.Payload);
            Assert.AreEqual("Yuval Noah Harari", payload.Author);
            Assert.AreEqual(CommandType.Insert, result.CommandType);
        }

        /// <summary>
        /// The test should create event store from gdto.
        /// </summary>
        [TestMethod]
        public void TestShouldCreateEventStoreFromGdto()
        {
            // Arrange
            var sut = new CreateStoreObject<Gdto>();

            // Act
            var result = sut.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Insert);

            // Assert
            var payload = JsonConvert.DeserializeObject<Gdto>(result.Payload);
            Assert.AreEqual("Yuval Noah Harari", payload.Properties.Single(p => p.Key == "Author").Value);
            Assert.AreEqual(CommandType.Insert, result.CommandType);
            Assert.AreEqual(TimeStamp, result.Timestamp);
        }
    }
}