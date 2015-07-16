namespace Tests.WriteToReadTests.UnitTests
{
    using System;

    using Common.Utilities;

    using DataAccess.Read.Dal.CodeFirst.DbContext;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// The read context.
        /// </summary>
        private IReadContext readContext;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.readContext = NSubstitute.Substitute.For<IReadContext>();

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

            //Add fake!

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

            // Act
            var result = sut.AddRegistrationTypeToDbSet("Comic");

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

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddRegistrationTypeToDbSet("Comic"));
        }

        /// <summary>
        /// The test should insert registration.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeMovie(), TimeStamp, "2001: A Space Odyssey");

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

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeMovie(), TimeStamp, "2001: A Space Odyssey"));
        }

        /// <summary>
        /// The test should check property type.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

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

            // Act
            var result = sut.CheckProperty(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick");

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

            // Act
            var result = sut.CheckProperty(SamplePropertyTypes.CreatePropertyTypeAuthor(), "David Lynch");

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

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.CheckProperty(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick"));
        }

        /// <summary>
        /// The test should insert property.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.InsertProperty(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick", SampleRegistrations.CreateRegistration2001());

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

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.InsertProperty(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick", SampleRegistrations.CreateRegistration2001()));
        }

        /// <summary>
        /// The test should add registration to property.
        /// </summary>
        [TestMethod]
        public void TestShouldAddRegistrationToProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.AddRegistrationToProperty(SampleProperties.CreateProperty1968(), SampleRegistrations.CreateRegistration2001());

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// The test should add registration to property and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldAddRegistrationToPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddRegistrationToProperty(SampleProperties.CreateProperty1968(), SampleRegistrations.CreateRegistration2001()));
        }

        /// <summary>
        /// The create generic registration service.
        /// </summary>
        private GenericRegistrationRepository CreateGenericRegistrationService()
        {
            return new GenericRegistrationRepository(this.readContext);
        }
    }
}