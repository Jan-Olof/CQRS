namespace Tests.ReadTests.UnitTests
{
    using System;
    using System.Linq;

    using Common.DataAccess;
    using Common.Utilities;

    using Domain.Read.Entities;
    using Domain.Read.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Tests.TestCommon;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The registration service tests.
    /// </summary>
    [TestClass]
    public class RegistrationServiceTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The repository.
        /// </summary>
        private IRepository<Registration> repository;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.repository = Substitute.For<IRepository<Registration>>();

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
        [TestMethod]
        public void TestShouldGetAllRegistrations()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetAll()
                .Returns(SampleRegistrations.CreateRegistrations().AsQueryable());

            // Act
            var result = sut.GetAllRegistrations();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("2001: A Space Odyssey", result.Single(r => r.Id == 2).Name);
            Assert.AreEqual("Stanley Kubrick", result.Single(r => r.Id == 2).Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        /// <summary>
        /// The test should get all registrations and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetAllRegistrationsAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetAllRegistrations());
        }

        /// <summary>
        /// The test should get registrations of one type.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationsOfOneType()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetAll()
                .Returns(SampleRegistrations.CreateRegistrations().AsQueryable());

            // Act
            var result = sut.GetRegistrations(1);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sapiens", result.Single().Name);
            Assert.AreEqual("Yuval Noah Harari", result.Single().Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        /// <summary>
        /// The test should get registrations of one type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationsOfOneTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetRegistrations(1));
        }

        /// <summary>
        /// The test should get one registration.
        /// </summary>
        [TestMethod]
        public void TestShouldGetOneRegistration()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetOne(1)
                .Returns(SampleRegistrations.CreateRegistrationSapiens());

            // Act
            var result = sut.GetRegistration(1);

            // Assert
            Assert.AreEqual("Sapiens", result.Name);
            Assert.AreEqual("Yuval Noah Harari", result.Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        /// <summary>
        /// The test should get one registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetOneRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationService();

            this.repository.GetOne(1).Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetRegistration(1));
        }

        /// <summary>
        /// The create registration service.
        /// </summary>
        private RegistrationService CreateRegistrationService()
        {
            return new RegistrationService(this.repository);
        }
    }
}