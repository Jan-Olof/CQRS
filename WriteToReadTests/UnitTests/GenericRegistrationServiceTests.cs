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
        /// The property type repository.
        /// </summary>
        private IRepository<PropertyType> propertyTypeRepository;

        /// <summary>
        /// The property repository.
        /// </summary>
        private IRepository<RegistrationProperty> propertyRepository;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.registrationTypeRepository = Substitute.For<IRepository<RegistrationType>>();
            this.propertyTypeRepository = Substitute.For<IRepository<PropertyType>>();
            this.propertyRepository = Substitute.For<IRepository<RegistrationProperty>>();
            
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

        [TestMethod]
        public void TestShouldCheckProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.propertyRepository.GetAll()
                .Returns(SampleRegistrationProperties.CreateRegistrationProperties().AsQueryable());

            // Act
            var result = sut.CheckProperty("Stanley Kubrick");

            // Assert
            Assert.AreEqual(1, result);
        }

        ///// <summary>
        ///// The test should check property type and not find any.
        ///// </summary>
        //[TestMethod]
        //public void TestShouldCheckPropertyTypeAndNotFindAny()
        //{
        //    // Arrange
        //    var sut = this.CreateGenericRegistrationService();

        //    this.propertyTypeRepository.GetAll()
        //        .Returns(SamplePropertyTypes.CreatePropertyTypes().AsQueryable());

        //    // Act
        //    var result = sut.CheckPropertyType("Autor");

        //    // Assert
        //    Assert.AreEqual(0, result);
        //}

        ///// <summary>
        ///// The test should check property type and throw exception.
        ///// </summary>
        //[TestMethod]
        //public void TestShouldCheckPropertyTypeAndThrowException()
        //{
        //    // Arrange
        //    var sut = this.CreateGenericRegistrationService();

        //    this.propertyTypeRepository.GetAll().Throws<Exception>();

        //    // Act & Assert
        //    MyAssert.Throws<Exception>(() => sut.CheckPropertyType("Author"));
        //}


        /// <summary>
        /// The create generic registration service.
        /// </summary>
        private GenericRegistrationService CreateGenericRegistrationService()
        {
            return new GenericRegistrationService(this.registrationTypeRepository, this.propertyTypeRepository, this.propertyRepository);
        }
    }
}