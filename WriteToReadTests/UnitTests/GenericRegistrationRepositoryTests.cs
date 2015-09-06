namespace Tests.WriteToReadTests.UnitTests
{
    using Common.Utilities;
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using Domain.Read.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Tests.TestCommon;
    using Tests.TestCommon.SampleObjects;
    using WriteToRead.ToReadDb;

    /// <summary>
    /// The generic registration repository tests.
    /// </summary>
    [TestClass]
    public class GenericRegistrationRepositoryTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The registration types.
        /// </summary>
        private IDbSet<Property> properties;

        /// <summary>
        /// The registration types.
        /// </summary>
        private IDbSet<PropertyType> propertyTypes;

        /// <summary>
        /// The read context.
        /// </summary>
        private IReadContext readContext;

        /// <summary>
        /// The registration types.
        /// </summary>
        private IDbSet<Registration> registrations;

        /// <summary>
        /// The registration types.
        /// </summary>
        private IDbSet<RegistrationType> registrationTypes;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.readContext = Substitute.For<IReadContext>();

            var regTypes = SampleRegistrationTypes.CreateRegistrationTypes().AsQueryable();
            this.registrationTypes = Substitute.For<IDbSet<RegistrationType>>().Initialize(regTypes);

            var regs = SampleRegistrations.CreateRegistrations().AsQueryable();
            this.registrations = Substitute.For<IDbSet<Registration>>().Initialize(regs);

            var propTypes = SamplePropertyTypes.CreatePropertyTypes().AsQueryable();
            this.propertyTypes = Substitute.For<IDbSet<PropertyType>>().Initialize(propTypes);

            var props = SampleProperties.CreateProperties().AsQueryable();
            this.properties = Substitute.For<IDbSet<Property>>().Initialize(props);

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
        /// The test should add registration to property.
        /// </summary>
        [TestMethod]
        public void TestShouldAddRegistrationToProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.AddRegistrationToProperty(
                SampleProperties.CreateProperty1968(), SampleRegistrations.CreateRegistration2001());

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
            MyAssert.Throws<Exception>(() => sut.AddRegistrationToProperty(null, null));
        }

        /// <summary>
        /// The test should check property.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.CheckIfPropertyIsRegistered(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick");

            // Assert
            Assert.AreEqual("Stanley Kubrick", result.Value);
        }

        /// <summary>
        /// The test should check property and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.CheckIfPropertyIsRegistered(SamplePropertyTypes.CreatePropertyTypeAuthor(), "David Lynch");

            // Assert
            Assert.AreEqual(string.Empty, result.Value);
        }

        /// <summary>
        /// The test should check property and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.CheckIfPropertyIsRegistered(SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick"));
        }

        /// <summary>
        /// The test should check property type.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Returns(this.propertyTypes);

            // Act
            var result = sut.GetPropertyType("Author");

            // Assert
            Assert.AreEqual("Author", result.Name);
        }

        /// <summary>
        /// The test should check property type and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyTypeAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Returns(this.propertyTypes);

            // Act
            var result = sut.GetPropertyType("Autor");

            // Assert
            Assert.AreEqual(string.Empty, result.Name);
        }

        /// <summary>
        /// The test should check property type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckPropertyTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetPropertyType("Author"));
        }

        /// <summary>
        /// The test should check registration type.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Returns(this.registrationTypes);

            // Act
            var result = sut.GetRegistrationType("Book");

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        /// <summary>
        /// The test should check registration type and not find any.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationTypeAndNotFindAny()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Returns(this.registrationTypes);

            // Act
            var result = sut.GetRegistrationType("Stamp");

            // Assert
            Assert.AreEqual(0, result.Id);
        }

        /// <summary>
        /// The test should check registration type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldCheckRegistrationTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.GetRegistrationType("Stamp"));
        }

        [TestMethod]
        public void TestShouldDeleteRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.DeleteRegistration(SampleRegistrations.CreateRegistration2001());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestShouldGetRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Registrations.Returns(this.registrations);

            // Act
            var result = sut.GetRegistration(1);

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void TestShouldGetRegistrationAndReturnsNull()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Registrations.Returns(this.registrations);

            // Act
            var result = sut.GetRegistration(66);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// The test should get registration type.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Returns(this.registrationTypes);

            // Act
            var result = sut.GetRegistrationType(SampleGdto.CreateGdtoWithWriteEventId());

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        /// <summary>
        /// The test should insert property.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.AddPropertyToDbSet(
                SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick", SampleRegistrations.CreateRegistration2001());

            // Assert
            Assert.AreEqual("Stanley Kubrick", result.Value);
        }

        /// <summary>
        /// The test should insert property and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Properties.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(
                () => sut.AddPropertyToDbSet(
                    SamplePropertyTypes.CreatePropertyTypeAuthor(), "Stanley Kubrick", SampleRegistrations.CreateRegistration2001()));
        }

        /// <summary>
        /// The test should insert property type.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Returns(this.propertyTypes);

            // Act
            var result = sut.AddPropertyTypeToDbSet("Size");

            // Assert
            Assert.AreEqual("Size", result.Name);
        }

        /// <summary>
        /// The test should insert property type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertPropertyTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddPropertyTypeToDbSet("Size"));
        }

        /// <summary>
        /// The test should insert registration.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Registrations.Returns(this.registrations);

            // Act
            var result = sut.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeMovie(), TimeStamp, "2001: A Space Odyssey", 2);

            // Assert
            Assert.AreEqual("2001: A Space Odyssey", result.Name);
        }

        /// <summary>
        /// The test should insert registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.Registrations.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(
                () => sut.AddRegistrationToDbSet(SampleRegistrationTypes.CreateRegistrationTypeMovie(), TimeStamp, "2001: A Space Odyssey", 2));
        }

        /// <summary>
        /// The test should insert registration type.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationType()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Returns(this.registrationTypes);

            // Act
            var result = sut.AddRegistrationTypeToDbSet("Comic");

            // Assert
            Assert.AreEqual("Comic", result.Name);
        }

        /// <summary>
        /// The test should insert registration type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldInsertRegistrationTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Throws<Exception>();

            // Act & Assert
            MyAssert.Throws<Exception>(() => sut.AddRegistrationTypeToDbSet("Comic"));
        }

        [TestMethod]
        public void TestShouldUpdateProperties()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.UpdateProperties(SampleRegistrations.CreateRegistration2001(), SampleGdto.CreateGdtoWithWriteEventIdMovie());

            // Assert
            Assert.AreEqual("Stanley Kubrikk", result.Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        [TestMethod]
        public void TestShouldUpdatePropertiesAddingOneProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.RegistrationTypes.Returns(this.registrationTypes);
            this.readContext.Registrations.Returns(this.registrations);
            this.readContext.PropertyTypes.Returns(this.propertyTypes);
            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.UpdateProperties(this.readContext.Registrations.Single(r => r.Id == 2), SampleGdto.CreateGdtoWithWriteEventIdMovieNewProp());

            // Assert
            Assert.AreEqual("Keir Dullea", result.Properties.Single(p => p.PropertyType.Name == "Actor").Value);
        }

        [TestMethod]
        public void TestShouldUpdatePropertiesAndReturnNull()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.UpdateProperties(null, SampleGdto.CreateGdtoWithWriteEventIdMovie());

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestShouldUpdatePropertiesRemovingOneProperty()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            this.readContext.PropertyTypes.Returns(this.propertyTypes);
            this.readContext.Properties.Returns(this.properties);

            // Act
            var result = sut.UpdateProperties(SampleRegistrations.CreateRegistration2001(), SampleGdto.CreateGdtoWithWriteEventIdMovieRemoveProp());

            // Assert
            Assert.IsNull(result.Properties.SingleOrDefault(p => p.PropertyType.Name == "Published"));
            Assert.AreEqual(1, result.Properties.Count);
        }

        [TestMethod]
        public void TestShouldUpdatePropertiesWhenGdtoIsNull()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.UpdateProperties(SampleRegistrations.CreateRegistration2001(), null);

            // Assert
            Assert.AreEqual("Stanley Kubrick", result.Properties.Single(p => p.PropertyType.Name == "Author").Value);
        }

        [TestMethod]
        public void TestShouldUpdateRegistration()
        {
            // Arrange
            var sut = this.CreateGenericRegistrationService();

            // Act
            var result = sut.UpdateRegistration(
                SampleRegistrations.CreateRegistration2001(), SampleRegistrationTypes.CreateRegistrationTypeMovie(), TimeStamp, "20001: A Space Odyssey");

            // Assert
            Assert.AreEqual("20001: A Space Odyssey", result.Name);
        }

        /// <summary>
        /// The create generic registration service.
        /// </summary>
        private WriteToReadRepository CreateGenericRegistrationService()
        {
            return new WriteToReadRepository(this.readContext);
        }
    }
}