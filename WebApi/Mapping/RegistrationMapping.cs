namespace Api.WebApi.Mapping
{
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataTransferObjects;

    using Domain.Read.Entities;

    /// <summary>
    /// Mapping between RegistrationModel and RegistrationDto.
    /// </summary>
    public static class RegistrationMapping
    {
        /// <summary>
        /// Create registration dtos from registrations.
        /// </summary>
        public static IList<RegistrationDto> CreateRegistrationDtos(IEnumerable<Registration> registrations)
        {
            return registrations
                .Select(CreateRegistrationDto)
                .ToList();
        }

        /// <summary>
        /// Create registration dto from registration.
        /// </summary>
        public static RegistrationDto CreateRegistrationDto(Registration registration)
        {
            return new RegistrationDto
                       {
                           Id = registration.Id,
                           Name = registration.Name,
                           Created = registration.Created,
                           Updated = registration.Updated,
                           RegistrationTypeId = registration.RegistrationType.Id,
                           RegistrationTypeName = registration.RegistrationType.Name,
                           RegistrationProperties = CreateRegistrationPropertiesDtos(registration.RegistrationProperties)
                       };
        }

        /// <summary>
        /// Create registration properties dtos from registration properties.
        /// </summary>
        private static IList<RegistrationPropertyDto> CreateRegistrationPropertiesDtos(IEnumerable<RegistrationProperty> registrationProperties)
        {
            return registrationProperties
                .Select(CreateRegistrationPropertyDto)
                .ToList();
        }

        /// <summary>
        /// Create registration property dto from registration property.
        /// </summary>
        private static RegistrationPropertyDto CreateRegistrationPropertyDto(RegistrationProperty registrationProperty)
        {
            return new RegistrationPropertyDto
                       {
                           Id = registrationProperty.Id,
                           PropertyTypeId = registrationProperty.PropertyType.Id,
                           PropertyTypeName = registrationProperty.PropertyType.Name,
                           Value = registrationProperty.Value
                       };
        }
    }
}