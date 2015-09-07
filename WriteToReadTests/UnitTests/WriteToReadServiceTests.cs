namespace Tests.WriteToReadTests.UnitTests
{
    using Common.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;

    using Tests.TestCommon.SampleObjects;

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
        public void TestShouldEtlFromWriteDbToReadDb()
        {
            // Arrange
            this.writeEventService.GetWriteEventsToProcess(0).Returns(SampleWriteEvents.CreateWriteEvents());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.PayloadSapiens()).Returns(SampleGdto.CreateGdtoWithPublished());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.Payload2001()).Returns(SampleGdto.CreateGdtoWithWritePublishedMovie());
            this.writeEventService.GetPropertyValue(SampleGdto.CreatePropertiesWithPublished(), "Name").ReturnsForAnyArgs("Sapiens");

            // Stub this.writeToReadRepository.GetRegistrationType(gdto);

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