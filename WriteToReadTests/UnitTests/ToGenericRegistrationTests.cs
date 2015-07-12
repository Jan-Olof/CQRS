namespace Tests.WriteToReadTests.UnitTests
{
    using System;

    using Common.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;

    using Tests.TestCommon.SampleObjects;

    using WriteToRead;

    /// <summary>
    /// The to generic registration tests.
    /// </summary>
    [TestClass]
    public class ToGenericRegistrationTests
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
        /// The test should get all registrations.
        /// </summary>
        //[TestMethod]
        public void TestShouldGetAllRegistrations()
        {
            // Arrange
            var sut = this.CreateToGenericRegistration();

            //this.repository.GetAll()
            //    .Returns(SampleRegistrations.CreateRegistrations().AsQueryable());

            // Act
            //var result = sut.AddToGenericRegistrationDatabase(SampleWriteEvents.CreateWriteEvents());

            // Assert
           //Assert.IsTrue(result);
        }

        /// <summary>
        /// The create to generic registration.
        /// </summary>
        private ToGenericRegistration CreateToGenericRegistration()
        {
            return new ToGenericRegistration();
        }
    }
}