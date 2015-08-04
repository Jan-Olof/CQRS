namespace Tests.IntegrationTests.TestClasses
{
    using Common.DataTransferObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Tests.IntegrationTests.Initialize;
    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The registration controller integration tests.
    /// </summary>
    [TestClass]
    public class RegistrationControllerIntegrationTests : BaseTestDb
    {
        /// <summary>
        /// The test should delete registration.
        /// </summary>
        [TestMethod]
        public void TestShouldDeleteRegistration()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            // Act
            var result = sut.DeleteRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(1, content);
        }

        /// <summary>
        /// The test should get all registrations.
        /// </summary>
        [TestMethod]
        public void TestShouldGetAllRegistrations()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            SeedDatabaseObjects.CreateRegistrationsInDb();

            // Act
            var result = sut.GetAllRegistrations();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<List<RegistrationDto>>(jsonString);

            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("2001: A Space Odyssey", content.Single(r => r.Id == 2).Name);
            Assert.AreEqual("Stanley Kubrick", content.Single(r => r.Id == 2).RegistrationProperties.Single(p => p.PropertyTypeName == "Author").Value);
        }

        /// <summary>
        /// The test should get registration.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistration()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            SeedDatabaseObjects.CreateRegistrationsInDb();

            // Act
            var result = sut.GetRegistration(2);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<RegistrationDto>(jsonString);

            Assert.AreEqual("2001: A Space Odyssey", content.Name);
            Assert.AreEqual("Stanley Kubrick", content.RegistrationProperties.Single(p => p.PropertyTypeName == "Author").Value);
        }

        /// <summary>
        /// The test should get registrations for one type.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationsForOneType()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            SeedDatabaseObjects.CreateRegistrationsInDb();

            // Act
            var result = sut.GetRegistrationsForOneType(2);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<List<RegistrationDto>>(jsonString);

            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("2001: A Space Odyssey", content.Single(r => r.Id == 2).Name);
            Assert.AreEqual("Stanley Kubrick", content.Single(r => r.Id == 2).RegistrationProperties.Single(p => p.PropertyTypeName == "Author").Value);
        }

        /// <summary>
        /// The test should post registration.
        /// </summary>
        [TestMethod]
        public void TestShouldPostRegistration()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            // Act
            var result = sut.PostRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(1, content);
        }

        /// <summary>
        /// The test should put registration.
        /// </summary>
        [TestMethod]
        public void TestShouldPutRegistration()
        {
            // Arrange
            var sut = Factory.CreateRegistrationController();

            // Act
            var result = sut.PutRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(1, content);
        }
    }
}