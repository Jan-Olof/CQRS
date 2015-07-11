namespace Tests.WriteTests.UnitTests
{
    using System;

    using Common.DataTransferObjects;
    using Common.Enums;
    using Common.Exceptions;
    using Common.Utilities;

    using Domain.Write.Commands;
    using Domain.Write.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;

    using TestCommon;
    using TestCommon.SampleObjects;

    /// <summary>
    /// The command services tests.
    /// </summary>
    [TestClass]
    public class CommandServicesTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The create store object.
        /// </summary>
        private ICreateStoreObject<Gdto> createStoreObject;

        /// <summary>
        /// The write to store.
        /// </summary>
        private IWriteToStore writeToStore;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.writeToStore = Substitute.For<IWriteToStore>();
            this.createStoreObject = Substitute.For<ICreateStoreObject<Gdto>>();

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
        /// The test should insert.
        /// </summary>
        [TestMethod]
        public void TestShouldInsert()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Insert)
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(0, "sample payload"));

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(12, "sample payload"));

            // Act
            var result = sut.Insert(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(12, result);
            this.createStoreObject.Received().CreateWriteEvent(Arg.Any<Gdto>(), CommandType.Insert);
        }

        /// <summary>
        /// The test should insert and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertAndThrowException()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Insert)
                .ReturnsForAnyArgs(x => { throw new Exception(); });

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(12, "sample payload"));

            // Act & Assert
            MyAssert.Throws<InsertException>(() => sut.Insert(SampleGdto.CreateGdto()));
        }

        /// <summary>
        /// The test should update.
        /// </summary>
        [TestMethod]
        public void TestShouldUpdate()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Update)
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(0, "sample payload"));

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(13, "sample payload"));

            // Act
            var result = sut.Update(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(13, result);
            this.createStoreObject.Received().CreateWriteEvent(Arg.Any<Gdto>(), CommandType.Update);
        }

        /// <summary>
        /// The test should update and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldUpdateAndThrowException()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Update)
                .ReturnsForAnyArgs(x => { throw new Exception(); });

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(12, "sample payload"));

            // Act & Assert
            MyAssert.Throws<UpdateException>(() => sut.Update(SampleGdto.CreateGdto()));
        }

        /// <summary>
        /// The test should delete.
        /// </summary>
        [TestMethod]
        public void TestShouldDelete()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Delete)
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(0, "sample payload"));

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(14, "sample payload"));

            // Act
            var result = sut.Delete(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(14, result);
            this.createStoreObject.Received().CreateWriteEvent(Arg.Any<Gdto>(), CommandType.Delete);
        }

        /// <summary>
        /// The test should delete and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldDeleteAndThrowException()
        {
            // Arrange
            var sut = this.CreateCommandServices();

            this.createStoreObject.CreateWriteEvent(SampleGdto.CreateGdto(), CommandType.Delete)
                .ReturnsForAnyArgs(x => { throw new Exception(); });

            this.writeToStore.InsertIntoEventStore(SampleWriteEvents.CreateWriteEvent(0, "sample payload"))
                .ReturnsForAnyArgs(SampleWriteEvents.CreateWriteEvent(12, "sample payload"));

            // Act & Assert
            MyAssert.Throws<DeleteException>(() => sut.Delete(SampleGdto.CreateGdto()));
        }

        /// <summary>
        /// The create command services.
        /// </summary>
        private CommandService CreateCommandServices()
        {
            return new CommandService(this.createStoreObject, this.writeToStore);
        }
    }
}