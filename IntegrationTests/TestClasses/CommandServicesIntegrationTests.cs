namespace Tests.IntegrationTests.TestClasses
{
    using System.Linq;

    using Common.DataTransferObjects;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    using Tests.IntegrationTests.Initialize;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The command services tests.
    /// </summary>
    [TestClass]
    public class CommandServicesIntegrationTests : BaseTestDb
    {
        /// <summary>
        /// The test should insert gdto.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertGdto()
        {
            // Arrange
            var sut = Factory.CreateCommandServices();

            // Act
            var result = sut.Insert(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(1, result);

            var repository = Factory.CreateWriteEventRepository();
            var writeEvent = repository.GetAll().Single();
            var gdto = JsonConvert.DeserializeObject<Gdto>(writeEvent.Payload);

            Assert.AreEqual("Yuval Noah Harari", gdto.Properties.Single(p => p.Key == "Author").Value);
        }
    }
}