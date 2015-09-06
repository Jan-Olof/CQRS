namespace Tests.WriteToReadTests.UnitTests
{
    using Common.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using WriteToRead;
    using WriteToRead.Interfaces;

    [TestClass]
    public class WriteToReadServiceTests
    {
        //private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        private IWriteEventService writeEventService;

        private IWriteToReadRepository writeToReadRepository;

        [TestInitialize]
        public void SetUp()
        {
            this.writeEventService = Substitute.For<IWriteEventService>();
            this.writeToReadRepository = Substitute.For<IWriteToReadRepository>();

            //SystemTime.Set(TimeStamp);
        }

        [TestCleanup]
        public void TearDown()
        {
            SystemTime.Reset();
        }

        [TestMethod]
        public void TestShouldDeserializeGdto()
        {
            // Arrange
            var sut = this.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(new DateTime(2015, 9, 6, 21, 14, 15));

            // Assert
            Assert.AreEqual(1, result);
        }

        private WriteToReadService CreateWriteToReadService()
        {
            return new WriteToReadService(this.writeEventService, this.writeToReadRepository);
        }
    }
}