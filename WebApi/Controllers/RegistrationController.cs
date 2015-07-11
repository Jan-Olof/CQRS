namespace Api.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Api.WebApi.Mapping;

    using Common.DataTransferObjects;

    using Domain.Read.Entities;
    using Domain.Read.Interfaces;
    using Domain.Write.Interfaces;

    using NLog;

    /// <summary>
    /// The registration controller handles all registrations made in the system.
    /// </summary>
    public class RegistrationController : ApiController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The command handler.
        /// </summary>
        private readonly ICommandService commandService;

        /// <summary>
        /// The registration service.
        /// </summary>
        private readonly IRegistrationService registrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController(ICommandService commandService, IRegistrationService registrationService)
        {
            if (commandService == null)
            {
                throw new ArgumentNullException("commandService");
            }

            if (registrationService == null)
            {
                throw new ArgumentNullException("registrationService");
            }

            this.commandService = commandService;
            this.registrationService = registrationService;
        }

        /// <summary>
        /// Get all registrations in the data store.
        /// </summary>
        /// <returns>
        /// Returns a HttpResponseMessage with a list of all registrations.
        /// </returns>
        [HttpGet]
        [Route("api/registration")]
        public HttpResponseMessage GetAllRegistrations()
        {
            try
            {
                var registrations = this.registrationService.GetAllRegistrations() ?? new List<Registration>();

                var registrationDtos = RegistrationMapping.CreateRegistrationDtos(registrations);

                return this.Request.CreateResponse(HttpStatusCode.OK, registrationDtos);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get one registration with a certain id in the data store.
        /// </summary>
        /// <param name="id">
        /// The id used to get the registration.
        /// </param>
        /// <returns>
        /// Returns a HttpResponseMessage with a registration.
        /// </returns>
        [HttpGet]
        [Route("api/registration/{id}")]
        public HttpResponseMessage GetRegistration(int id)
        {
            try
            {
                var registration = this.registrationService.GetRegistration(id);

                var registrationDto = RegistrationMapping.CreateRegistrationDto(registration);

                return this.Request.CreateResponse(HttpStatusCode.OK, registrationDto);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all registrations with a certain type in the data store.
        /// </summary>
        /// <param name="type">
        /// The type used to get the registrations.
        /// </param>
        /// <returns>
        /// Returns a HttpResponseMessage with a list of registrations.
        /// </returns>
        [HttpGet]
        [Route("api/registration/type/{type}")]
        public HttpResponseMessage GetRegistrationsForOneType(int type)
        {
            try
            {
                var registrations = this.registrationService.GetRegistrations(type) ?? new List<Registration>();

                var registrationDtos = RegistrationMapping.CreateRegistrationDtos(registrations);

                return this.Request.CreateResponse(HttpStatusCode.OK, registrationDtos);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Post a new registration. This inserts a new registration in the data store.
        /// </summary>
        /// <param name="content">
        /// The content is a Gdto that contains all data to register.
        /// </param>
        /// <returns>
        /// Returns a HttpResponseMessage with the id of the event as content.
        /// </returns>
        [HttpPost]
        [Route("api/registration")]
        public HttpResponseMessage PostRegistration([FromBody]Gdto content)
        {
            try
            {
                int returnedId = this.commandService.Insert(content);

                return returnedId > 0
                    ? this.Request.CreateResponse(HttpStatusCode.OK, returnedId)
                    : this.Request.CreateResponse(HttpStatusCode.BadRequest, "PostRegistration failed to insert event.");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Put an existing registration. This updates an existing registration in the data store.
        /// </summary>
        /// <param name="content">
        /// The content is a Gdto that contains all data to register.
        /// </param>
        /// <returns>
        /// Returns a HttpResponseMessage with the id of the event as content.
        /// </returns>
        [HttpPut]
        [Route("api/registration")]
        public HttpResponseMessage PutRegistration([FromBody]Gdto content)
        {
            try
            {
                int returnedId = this.commandService.Update(content);

                return returnedId > 0
                    ? this.Request.CreateResponse(HttpStatusCode.OK, returnedId)
                    : this.Request.CreateResponse(HttpStatusCode.BadRequest, "PutRegistration failed to insert event.");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing registration. This deletes an existing registration in the data store.
        /// </summary>
        /// <param name="content">
        /// The content is a Gdto that contains all data to register.
        /// </param>
        /// <returns>
        /// Returns a HttpResponseMessage with the id of the event as content.
        /// </returns>
        [HttpDelete]
        [Route("api/registration")]
        public HttpResponseMessage DeleteRegistration([FromBody]Gdto content)
        {
            try
            {
                int returnedId = this.commandService.Delete(content);

                return returnedId > 0
                    ? this.Request.CreateResponse(HttpStatusCode.OK, returnedId)
                    : this.Request.CreateResponse(HttpStatusCode.BadRequest, "DeleteRegistration failed to insert event.");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
