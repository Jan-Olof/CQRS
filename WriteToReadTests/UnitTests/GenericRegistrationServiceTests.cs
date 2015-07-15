namespace Tests.WriteToReadTests.UnitTests
{
    using System;
    using System.Linq;

    using Common.DataAccess;
    using Common.Utilities;

    using Domain.Read.Entities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Tests.TestCommon;
    using Tests.TestCommon.SampleObjects;

    using WriteToRead.ToReadDb;

    /// <summary>
    /// The generic registration service tests.
    /// </summary>
    [TestClass]
    public class GenericRegistrationServiceTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The registration type repository.
        /// </summary>
        private IRepository<RegistrationType> registrationTypeRepository;

        /// <summary>
        /// The registration repository.
        /// </summary>
        private IRepository<Registration> registrationRepository;

        /// <summary>
        /// The property type repository.
        /// </summary>
        private IRepository<PropertyType> propertyTypeRepository;

        /// <summary>
        /// The property repository.
        /// </summary>
        private IRepository<Property> propertyRepository;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.registrationTypeRepository = Substitute.For<IRepository<RegistrationType>>();
            this.registrationRepository = Substitute.For<IRepository<Registration>>();
            this.propertyTypeRepository = Substitute.For<IRepository<PropertyType>>();
            this.propertyRepository = Substitute.For<IRepository<Property>>();

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
        /// The test should check registration type.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationTypeRepository.GetAll()
                .Returns(SampleRegistrationTypes.CreateRegistrationTypes().AsQueryable());

            // Act
            var result = sut.CheckRegistrationType("Book");

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// The test should check registration type and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationTypeAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationTypeRepository.GetAll()
                .Returns(SampleRegistrationTypes.CreateRegistrationTypes().AsQueryable());

            // Act
            var result = sut.CheckRegistrationType("Stamp");

            // Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The test should check registration type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationTypeRepository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.CheckRegistrationType("Stamp"));
        }

        /// <summary>
        /// The test should insert registration type.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationTypeRepository.Insert(SampleRegistrationTypes.CreateRegistrationTypeComic())
                .ReturnsForAnyArgs(SampleRegistrationTypes.CreateRegistrationTypeComic());

            // Act
            var result = sut.InsertRegistrationType("Comic");

            // Assert
            Assert.AreEqual(3, result);
        }

        /// <summary>
        /// The test should insert registration type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationTypeRepository.Insert(SampleRegistrationTypes.CreateRegistrationTypeComic()).Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.InsertRegistrationType("Comic"));
        }

        /// <summary>
        /// The test should insert registration.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationRepository.Insert(SampleRegistrations.CreateRegistration2001())
                .ReturnsForAnyArgs(SampleRegistrations.CreateRegistration2001());

            // Act
            var result = sut.InsertRegistration(2, TimeStamp, "2001: A Space Odyssey");

            // Assert
            Assert.AreEqual(2, result.Id);
        }

        /// <summary>
        /// The test should insert registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.registrationRepository.Insert(SampleRegistrations.CreateRegistration2001()).ThrowsForAnyArgs<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.InsertRegistration(2, TimeStamp, "2001: A Space Odyssey"));
        }

        /// <summary>
        /// The test should check property type.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyTypeRepository.GetAll()
                .Returns(SamplePropertyTypes.CreatePropertyTypes().AsQueryable());

            // Act
            var result = sut.CheckPropertyType("Author");

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// The test should check property type and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyTypeAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyTypeRepository.GetAll()
                .Returns(SamplePropertyTypes.CreatePropertyTypes().AsQueryable());

            // Act
            var result = sut.CheckPropertyType("Autor");

            // Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The test should check property type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyTypeRepository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.CheckPropertyType("Author"));
        }

        /// <summary>
        /// The test should insert property type.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyTypeRepository.Insert(SamplePropertyTypes.CreatePropertyTypeSize())
                .ReturnsForAnyArgs(SamplePropertyTypes.CreatePropertyTypeSize());

            // Act
            var result = sut.InsertPropertType("Size");

            // Assert
            Assert.AreEqual(3, result);
        }

        /// <summary>
        /// The test should insert property type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyTypeRepository.Insert(SamplePropertyTypes.CreatePropertyTypeSize()).Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.InsertPropertType("Size"));
        }

        /// <summary>
        /// The test should check property.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.GetAll()
                .Returns(SampleProperties.CreateProperties().AsQueryable());

            // Act
            var result = sut.CheckProperty(1, "Stanley Kubrick");

            // Assert
            Assert.AreEqual(3, result.Id);
        }

        /// <summary>
        /// The test should check property and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.GetAll()
                .Returns(SampleProperties.CreateProperties().AsQueryable());

            // Act
            var result = sut.CheckProperty(1, "David Lynch");

            // Assert
            Assert.AreEqual(0, result.Id);
        }

        /// <summary>
        /// The test should check property and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.GetAll().Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.CheckProperty(1, "Stanley Kubrick"));
        }

        /// <summary>
        /// The test should insert property.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.Insert(SampleProperties.CreatePropertyKubrickDb())
                .ReturnsForAnyArgs(SampleProperties.CreatePropertyKubrickDb());

            // Act
            var result = sut.InsertProperty(1, "Stanley Kubrick", SampleRegistrations.CreateRegistration2001());

            // Assert
            Assert.AreEqual(3, result.Id);
        }

        /// <summary>
        /// The test should insert property and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.Insert(SampleProperties.CreatePropertyKubrickDb()).ThrowsForAnyArgs<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.InsertProperty(1, "Stanley Kubrick", SampleRegistrations.CreateRegistration2001()));
        }

        /// <summary>
        /// The test should add registration to property.
        /// </summary>
        [TestMethod]
        public void TestShouldAddRegistrationToProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.SaveAllChanges().ReturnsForAnyArgs(true);

            // Act
            var result = sut.AddRegistrationToProperty(SampleProperties.CreateProperty1968(), SampleRegistrations.CreateRegistration2001());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestShouldAddRegistrationToPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.SaveAllChanges().ThrowsForAnyArgs<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddRegistrationToProperty(SampleProperties.CreateProperty1968(), SampleRegistrations.CreateRegistration2001()));
        }

        /// <summary>
        /// The create generic registration service.
        /// </summary>
        private GenericRegistrationService CreateGenericRegistrationService()
        {
            return new GenericRegistrationService(
                this.registrationTypeRepository, this.registrationRepository, this.propertyTypeRepository, this.propertyRepository);
        }
    }
}