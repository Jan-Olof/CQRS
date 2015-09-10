namespace WriteToRead.ToReadDb
{
    using Common.DataTransferObjects;
    using DataAccess.Read.Dal.CodeFirst.DbContext;
    using Domain.Read.Entities;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using WriteToRead.Interfaces;

    /// <summary>
    /// Class that is used to transfer data from the Write database to the Read database.
    /// </summary>
    public class WriteToReadRepository : IWriteToReadRepository
    {
        private const string Name = "Name";
        private const string OriginalWriteEventId = "OriginalWriteEventId";

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IReadContext readContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteToReadRepository"/> class.
        /// </summary>
        public WriteToReadRepository(IReadContext readContext)
        {
            if (readContext == null)
            {
                throw new ArgumentNullException(nameof(readContext));
            }

            this.readContext = readContext;
        }

        /// <summary>
        /// Add a property to a registration.
        /// </summary>
        public void AddProperty(PropertyType propertyType, KeyValuePair<string, string> property, Registration registration)
        {
            var registrationProperty = this.CheckIfPropertyIsRegistered(propertyType, property.Value);

            if (registrationProperty.Id == 0)
            {
                this.AddPropertyToDbSet(propertyType, property.Value, registration);
            }
            else
            {
                this.AddRegistrationToProperty(registrationProperty, registration);
            }
        }

        /// <summary>
        /// Add property to db set.
        /// </summary>
        public Property AddPropertyToDbSet(PropertyType type, string value, Registration registration)
        {
            try
            {
                var property = Property.CreateProperty(type, value, registration);

                this.readContext.Properties.Add(property);
                registration.Properties.Add(property);

                return property;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Add property type to db set.
        /// </summary>
        public PropertyType AddPropertyTypeToDbSet(string type)
        {
            try
            {
                var propertyType = PropertyType.CreatePropertyType(0, type);

                this.readContext.PropertyTypes.Add(propertyType);

                return propertyType;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Add properties to a registration.
        /// </summary>
        public void AddRegistrationProperties(Gdto gdto, Registration registration)
        {
            foreach (var property in gdto.Properties)
            {
                this.AddPropertyTypeAndProperty(registration, property);
            }
        }

        /// <summary>
        /// Add registration to db set.
        /// </summary>
        public Registration AddRegistrationToDbSet(RegistrationType type, DateTime timestamp, string name, int originalWriteEventId)
        {
            try
            {
                var registration = Registration.CreateRegistration(type, timestamp, name, originalWriteEventId);

                this.readContext.Registrations.Add(registration);

                return registration;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Add a registration to a property.
        /// </summary>
        public bool AddRegistrationToProperty(Property property, Registration registration)
        {
            try
            {
                if (property.Registrations == null)
                {
                    property.Registrations = new Collection<Registration>();
                }

                property.Registrations.Add(registration);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Add registration type to db set.
        /// </summary>
        public RegistrationType AddRegistrationTypeToDbSet(string entityType)
        {
            try
            {
                var registrationType = RegistrationType.CreateRegistrationType(entityType);

                this.readContext.RegistrationTypes.Add(registrationType);

                return registrationType;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Check if a property is already registered and return the id.
        /// </summary>
        public Property CheckIfPropertyIsRegistered(PropertyType type, string value)
        {
            try
            {
                if (this.readContext.Properties == null)
                {
                    return new Property();
                }

                foreach (var property in this.readContext.Properties)
                {
                    if (property.PropertyTypeId != type.Id)
                    {
                        continue;
                    }

                    if (string.Equals(property.Value, value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return property;
                    }
                }

                return new Property();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Delete a registration.
        /// </summary>
        public bool DeleteRegistration(Registration registration)
        {
            try
            {
                var properties = new List<Property>();
                properties.AddRange(registration.Properties);

                foreach (var property in properties)
                {
                    this.readContext.Properties.Remove(property);
                }

                this.readContext.Registrations.Remove(registration);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Check which property type a property is and return the type.
        /// </summary>
        public PropertyType GetPropertyType(string type)
        {
            try
            {
                if (string.Equals(type, Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return PropertyType.CreatePropertyType(-1, string.Empty);
                }

                if (this.readContext.PropertyTypes == null)
                {
                    return PropertyType.CreatePropertyType(0, string.Empty);
                }

                foreach (var propertyType in this.readContext.PropertyTypes)
                {
                    if (string.Equals(type, propertyType.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return propertyType;
                    }
                }

                return PropertyType.CreatePropertyType(0, string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get registration from OriginalWriteEventId.
        /// </summary>
        public Registration GetRegistration(int originalWriteEventId)
        {
            try
            {
                return Registration.GetRegistration(this.readContext.Registrations, originalWriteEventId);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Check which registration type a registration is and return the type.
        /// </summary>
        public RegistrationType GetRegistrationType(string entityType)
        {
            try
            {
                if (this.readContext.RegistrationTypes == null)
                {
                    return new RegistrationType();
                }

                foreach (var registrationType in this.readContext.RegistrationTypes)
                {
                    if (string.Equals(entityType, registrationType.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return registrationType;
                    }
                }

                return new RegistrationType();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get RegistrationType from a Gdto.
        /// </summary>
        public RegistrationType GetRegistrationType(Gdto gdto)
        {
            var registrationType = this.GetRegistrationType(gdto.EntityType);

            if (registrationType.Id == 0)
            {
                registrationType = this.AddRegistrationTypeToDbSet(gdto.EntityType);
            }

            return registrationType;
        }

        /// <summary>
        /// The save all changes to the database.
        /// </summary>
        public bool SaveAllChanges()
        {
            using (var dbContextTransaction = this.readContext.Database.BeginTransaction())
            {
                try
                {
                    int changes = this.readContext.SaveChanges();

                    dbContextTransaction.Commit();

                    return changes > 0;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    this.logger.Error(ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Update all properties for a registration. Also inserting new and removing old properties.
        /// </summary>
        public Registration UpdateProperties(Registration registration, Gdto gdto)
        {
            try
            {
                if (registration == null || gdto == null)
                {
                    return registration;
                }

                registration = UpdateExistingProperties(registration, gdto);

                registration = this.AddNewProperties(registration, gdto);

                registration = DeleteNonExistingProperties(registration, gdto);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }

            return registration;
        }

        /// <summary>
        /// Update a registration.
        /// </summary>
        public Registration UpdateRegistration(Registration registration, RegistrationType type, DateTime timestamp, string name)
        {
            try
            {
                return Registration.UpdateRegistration(registration, type, timestamp, name);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Check if a property exists in a GDTO.
        /// </summary>
        private static bool CheckIfPropertyExistInGdto(IList<KeyValuePair<string, string>> gdtoProperties, Property registrationProperty)
        {
            bool exists = false;
            foreach (var gdtoProperty in gdtoProperties)
            {
                if (string.Equals(gdtoProperty.Key, registrationProperty.PropertyType.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Check if a property exists in a registration.
        /// </summary>
        private static bool CheckIfPropertyExistInRegistration(Registration registration, KeyValuePair<string, string> gdtoProperty)
        {
            bool exists = false;
            foreach (var property in registration.Properties)
            {
                if (string.Equals(property.PropertyType.Name, gdtoProperty.Key, StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(Name, gdtoProperty.Key, StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(OriginalWriteEventId, gdtoProperty.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Delete properties that does not exist in the GDTO.
        /// </summary>
        private static Registration DeleteNonExistingProperties(Registration registration, Gdto gdto)
        {
            if (registration.Properties == null || gdto.Properties == null)
            {
                return registration;
            }

            var toDelete = GetPropertiesToDelete(registration, gdto);

            foreach (var property in toDelete)
            {
                registration.Properties.Remove(property);
            }

            return registration;
        }

        /// <summary>
        /// Get the properties that should be deleted.
        /// </summary>
        private static IList<Property> GetPropertiesToDelete(Registration registration, Gdto gdto)
        {
            var toDelete = new List<Property>();

            foreach (var property in registration.Properties)
            {
                bool exists = CheckIfPropertyExistInGdto(gdto.Properties, property);

                if (!exists)
                {
                    toDelete.Add(property);
                }
            }

            return toDelete;
        }

        /// <summary>
        /// Update the values of existing properties with new values from the Gdto.
        /// </summary>
        private static Registration UpdateExistingProperties(Registration registration, Gdto gdto)
        {
            if (registration.Properties == null || gdto.Properties == null)
            {
                return registration;
            }

            foreach (var property in registration.Properties)
            {
                UpdateProperty(gdto, property);
            }

            return registration;
        }

        /// <summary>
        /// Update the value of one existing property with new value from the Gdto.
        /// </summary>
        private static void UpdateProperty(Gdto gdto, Property registrationProperty)
        {
            foreach (var gdtoProperty in gdto.Properties)
            {
                if (string.Equals(registrationProperty.PropertyType.Name, gdtoProperty.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    registrationProperty.Value = gdtoProperty.Value;
                }
            }
        }

        /// <summary>
        /// Add new properties to a registration.
        /// </summary>
        private Registration AddNewProperties(Registration registration, Gdto gdto)
        {
            if (registration.Properties == null || gdto.Properties == null)
            {
                return registration;
            }

            foreach (var keyValuePair in gdto.Properties)
            {
                bool exists = CheckIfPropertyExistInRegistration(registration, keyValuePair);

                if (!exists)
                {
                    this.AddPropertyTypeAndProperty(registration, keyValuePair);
                }
            }

            return registration;
        }

        /// <summary>
        /// Add both property type and property to a registration.
        /// </summary>
        private void AddPropertyTypeAndProperty(Registration registration, KeyValuePair<string, string> gdtoProperty)
        {
            var propertyType = this.GetPropertyType(gdtoProperty.Key);

            if (propertyType.Id == 0)
            {
                propertyType = this.AddPropertyTypeToDbSet(gdtoProperty.Key);
            }

            if (propertyType.Id > -1)
            {
                this.AddProperty(propertyType, gdtoProperty, registration);
            }
        }
    }
}