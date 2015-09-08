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

            SystemTime.Set(new DateTime(2015, 9, 4, 19, 48, 11));
        }

        [TestCleanup]
        public void TearDown()
        {
            SystemTime.Reset();
        }

        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDbDelete()
        {
            // Arrange
            var timestamp = new DateTime(2015, 9, 6, 21, 14, 15);

            this.writeEventService.GetWriteEventsToProcess(0).Returns(SampleWriteEvents.CreateWriteEventsDelete());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.PayloadSapiens()).Returns(SampleGdto.CreateGdtoWithPublished());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.Payload2001()).Returns(SampleGdto.CreateGdtoWithWritePublishedMovie());
            this.writeEventService.GetPropertyValue(SampleGdto.CreatePropertiesWithPublished(), "Name").ReturnsForAnyArgs("Sapiens");
            this.writeEventService.GetOriginalWriteEventId(SampleGdto.CreatePropertiesWithPublished()).ReturnsForAnyArgs(1);
            this.writeToReadRepository.GetRegistration(1).ReturnsForAnyArgs(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.DeleteRegistration(SampleRegistrations.CreateRegistrationSapiens()).ReturnsForAnyArgs(true);
            this.writeToReadRepository.SaveAllChanges().Returns(true);

            var sut = this.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(timestamp);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDbInsert()
        {
            // Arrange
            var timestamp = new DateTime(2015, 9, 6, 21, 14, 15);

            this.writeEventService.GetWriteEventsToProcess(0).Returns(SampleWriteEvents.CreateWriteEvents());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.PayloadSapiens()).Returns(SampleGdto.CreateGdtoWithPublished());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.Payload2001()).Returns(SampleGdto.CreateGdtoWithWritePublishedMovie());
            this.writeEventService.GetPropertyValue(SampleGdto.CreatePropertiesWithPublished(), "Name").ReturnsForAnyArgs("Sapiens");
            this.writeToReadRepository.GetRegistrationType(SampleGdto.CreateGdtoWithPublished()).ReturnsForAnyArgs(SampleRegistrationTypes.CreateRegistrationTypeBook());
            this.writeToReadRepository.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeBook(), timestamp, "Sapiens", 1).ReturnsForAnyArgs(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.SaveAllChanges().Returns(true);

            var sut = this.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(timestamp);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestShouldEtlFromWriteDbToReadDbUpdate()
        {
            // Arrange
            var timestamp = new DateTime(2015, 9, 6, 21, 14, 15);

            this.writeEventService.GetWriteEventsToProcess(0).Returns(SampleWriteEvents.CreateWriteEventsUpdate());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.PayloadSapiens()).Returns(SampleGdto.CreateGdtoWithPublished());
            this.writeEventService.DeserializeGdto(SampleWriteEvents.Payload2001()).Returns(SampleGdto.CreateGdtoWithWritePublishedMovie());
            this.writeEventService.GetPropertyValue(SampleGdto.CreatePropertiesWithPublished(), "Name").ReturnsForAnyArgs("Sapiens");
            this.writeEventService.GetOriginalWriteEventId(SampleGdto.CreatePropertiesWithPublished()).ReturnsForAnyArgs(1);
            this.writeToReadRepository.GetRegistrationType(SampleGdto.CreateGdtoWithPublished()).ReturnsForAnyArgs(SampleRegistrationTypes.CreateRegistrationTypeBook());
            this.writeToReadRepository.GetRegistration(1).ReturnsForAnyArgs(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeBook(), timestamp, "Sapiens", 1).ReturnsForAnyArgs(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.UpdateRegistration(SampleRegistrations.CreateRegistrationSapiens(), SampleRegistrationTypes.CreateRegistrationTypeBook(), timestamp, "Sapiens").Returns(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.UpdateProperties(SampleRegistrations.CreateRegistrationSapiens(), SampleGdto.CreateGdtoWithPublished()).Returns(SampleRegistrations.CreateRegistrationSapiens());
            this.writeToReadRepository.SaveAllChanges().Returns(true);

            var sut = this.CreateWriteToReadService();

            // Act
            var result = sut.EtlFromWriteDbToReadDb(timestamp);

            // Assert
            Assert.AreEqual(1, result);
        }

        private WriteToReadService CreateWriteToReadService()
        {
            return new WriteToReadService(this.writeEventService, this.writeToReadRepository);
        }
    }
}