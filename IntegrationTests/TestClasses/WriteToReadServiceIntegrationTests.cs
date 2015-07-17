namespace Tests.IntegrationTests.TestClasses
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tests.IntegrationTests.Initialize;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The write to read service integration tests.
    /// </summary>
    [TestClass]
    public class WriteToReadServiceIntegrationTests : BaseTestDb
    {
        /// <summary>
        /// The test should add to generic registration read db.
        /// </summary>
        [TestMethod]
        public void TestShouldAddToGenericRegistrationReadDb()
        {
            // Arrange
            var repository = Factory.CreateWriteEventRepository();
            repository.Insert(SampleWriteEvents.CreateWriteEvents());

            var sut = Factory.CreateWriteToReadService();

            // Act
            var result = sut.AddToGenericRegistrationReadDb(new DateTime(2015, 7, 15, 17, 37, 17));

            // Assert
            Assert.AreEqual(2, result);

            //TODO: Get from db and assert that all is saved!
        }
    }
}