namespace Tests.WebApiTests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Api.WebApi.Controllers;

    using Common.DataTransferObjects;
    using Common.Utilities;

    using Domain.Read.Interfaces;
    using Domain.Write.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Tests.TestCommon.SampleObjects;

    /// <summary>
    /// The registration controller tests.
    /// </summary>
    [TestClass]
    public class RegistrationControllerTests
    {
        /// <summary>
        /// The time stamp.
        /// </summary>
        private static readonly DateTime TimeStamp = new DateTime(2015, 6, 26, 17, 37, 15);

        /// <summary>
        /// The command handler.
        /// </summary>
        private ICommandService commandService;

        /// <summary>
        /// The registration service.
        /// </summary>
        private IRegistrationService registrationService;

        /// <summary>
        /// The set up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.commandService = Substitute.For<ICommandService>();
            this.registrationService = Substitute.For<IRegistrationService>();

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
            var sut = this.CreateRegistrationController();

            this.registrationService.GetAllRegistrations().Returns(SampleRegistrations.CreateRegistrations());

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
        /// The test should get all registrations and return no posts.
        /// </summary>
        [TestMethod]
        public void TestShouldGetAllRegistrationsAndReturnNoPosts()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            // Act
            var result = sut.GetAllRegistrations();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<List<RegistrationDto>>(jsonString);

            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// The test should get all registrations and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetAllRegistrationsAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.registrationService.GetAllRegistrations().Throws<Exception>();

            // Act
            var result = sut.GetAllRegistrations();

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The test should get registration.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistration()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.registrationService.GetRegistration(1).Returns(SampleRegistrations.CreateRegistration2001());

            // Act
            var result = sut.GetRegistration(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<RegistrationDto>(jsonString);

            Assert.AreEqual("2001: A Space Odyssey", content.Name);
            Assert.AreEqual("Stanley Kubrick", content.RegistrationProperties.Single(p => p.PropertyTypeName == "Author").Value);
        }

        /// <summary>
        /// The test should get registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.registrationService.GetRegistration(1).Throws<Exception>();

            // Act
            var result = sut.GetRegistration(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The test should get registrations.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrations()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.registrationService.GetRegistrations(1).Returns(SampleRegistrations.CreateRegistrations());

            // Act
            var result = sut.GetRegistrationsForOneType(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<List<RegistrationDto>>(jsonString);

            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("2001: A Space Odyssey", content.Single(r => r.Id == 2).Name);
            Assert.AreEqual("Stanley Kubrick", content.Single(r => r.Id == 2).RegistrationProperties.Single(p => p.PropertyTypeName == "Author").Value);
        }

        /// <summary>
        /// The test should get registrations for one type and return no posts.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationsForOneTypeAndReturnNoPosts()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            // Act
            var result = sut.GetRegistrationsForOneType(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<List<RegistrationDto>>(jsonString);

            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// The test should get registrations for one type and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldGetRegistrationsForOneTypeAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.registrationService.GetRegistrations(1).Throws<Exception>();

            // Act
            var result = sut.GetRegistrationsForOneType(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The test should post registration.
        /// </summary>
        [TestMethod]
        public void TestShouldPostRegistration()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Insert(SampleGdto.CreateGdto()).ReturnsForAnyArgs(11);

            // Act
            var result = sut.PostRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(11, content);
        }

        /// <summary>
        /// The test should post registration and return bad request.
        /// </summary>
        [TestMethod]
        public void TestShouldPostRegistrationAndReturnBadRequest()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Insert(SampleGdto.CreateGdto()).ReturnsForAnyArgs(0);

            // Act
            var result = sut.PostRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// The test should post registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldPostRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Insert(SampleGdto.CreateGdto()).ThrowsForAnyArgs<Exception>();

            // Act
            var result = sut.PostRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The test should put registration.
        /// </summary>
        [TestMethod]
        public void TestShouldPutRegistration()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Update(SampleGdto.CreateGdto()).ReturnsForAnyArgs(11);

            // Act
            var result = sut.PutRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(11, content);
        }

        /// <summary>
        /// The test should put registration and return bad request.
        /// </summary>
        [TestMethod]
        public void TestShouldPutRegistrationAndReturnBadRequest()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Update(SampleGdto.CreateGdto()).ReturnsForAnyArgs(0);

            // Act
            var result = sut.PutRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// The test should put registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldPutRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Update(SampleGdto.CreateGdto()).ThrowsForAnyArgs<Exception>();

            // Act
            var result = sut.PutRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The test should delete registration.
        /// </summary>
        [TestMethod]
        public void TestShouldDeleteRegistration()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Delete(SampleGdto.CreateGdto()).ReturnsForAnyArgs(11);

            // Act
            var result = sut.DeleteRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var jsonString = result.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<int>(jsonString);

            Assert.AreEqual(11, content);
        }

        /// <summary>
        /// The test should delete registration and return bad request.
        /// </summary>
        [TestMethod]
        public void TestShouldDeleteRegistrationAndReturnBadRequest()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Delete(SampleGdto.CreateGdto()).ReturnsForAnyArgs(0);

            // Act
            var result = sut.DeleteRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// The test should delete registration and throw exception.
        /// </summary>
        [TestMethod]
        public void TestShouldDeleteRegistrationAndThrowException()
        {
            // Arrange
            var sut = this.CreateRegistrationController();

            this.commandService.Delete(SampleGdto.CreateGdto()).ThrowsForAnyArgs<Exception>();

            // Act
            var result = sut.DeleteRegistration(SampleGdto.CreateGdto());

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// The create registration controller.
        /// </summary>
        private RegistrationController CreateRegistrationController()
        {
            var controller = new RegistrationController(this.commandService, this.registrationService);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();
            controller.Request.RequestUri = new Uri(string.Concat(ConfigurationManager.AppSettings.Get("WebApiAddress"), "/api/"));

            return controller;
        }
    }
}