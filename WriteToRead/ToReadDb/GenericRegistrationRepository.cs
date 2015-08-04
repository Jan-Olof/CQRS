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
    public class GenericRegistrationRepository : IGenericRegistrationRepository
    {
        private const string Name = "Name";

        private const string OriginalWriteEventId = "OriginalWriteEventId";

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The read context.
        /// </summary>
        private readonly IReadContext readContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRegistrationRepository"/> class.
        /// </summary>
        public GenericRegistrationRepository(IReadContext readContext)
        {
            if (readContext == null)
            {
                throw new ArgumentNullException(nameof(readContext));
            }

            this.readContext = readContext;
        }

        /// <summary>
        /// Add properties to a registration.
        /// </summary>
        public void AddProperties(Gdto gdto, Registration registration)
        {
            foreach (var property in gdto.Properties)
            {
                this.AddPropertyTypeAndProperty(registration, property);
            }
        }

        /// <summary>
        /// Add a property to a registration.
        /// </summary>
        public void AddProperty(PropertyType propertyType, KeyValuePair<string, string> property, Registration registration)
        {
            var registrationProperty = this.CheckProperty(propertyType, property.Value);

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
        /// Add propert type to db set.
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
        public Property CheckProperty(PropertyType type, string value)
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
        /// Check which property type a property is and return the type.
        /// </summary>
        public PropertyType CheckPropertyType(string type)
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
        /// Check which registration type a registration is and return the type.
        /// </summary>
        public RegistrationType CheckRegistrationType(string entityType)
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
        /// Delete a registration.
        /// </summary>
        public bool DeleteRegistration(string namePropertyValue)
        {
            throw new NotImplementedException();
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
        /// Get RegistrationType from a Gdto.
        /// </summary>
        public RegistrationType GetRegistrationType(Gdto gdto)
        {
            var registrationType = this.CheckRegistrationType(gdto.EntityType);

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
        /// Check if a property exists in a registration.
        /// </summary>
        private static bool DoesPropertyExistInRegistration(Registration registration, KeyValuePair<string, string> keyValuePair)
        {
            bool exists = false;
            foreach (var property in registration.Properties)
            {
                if (string.Equals(property.PropertyType.Name, keyValuePair.Key, StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(Name, keyValuePair.Key, StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(OriginalWriteEventId, keyValuePair.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    exists = true;
                }
            }

            return exists;
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
                foreach (var keyValuePair in gdto.Properties)
                {
                    if (string.Equals(property.PropertyType.Name, keyValuePair.Key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        property.Value = keyValuePair.Value;
                    }
                }
            }

            return registration;
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
                bool exists = DoesPropertyExistInRegistration(registration, keyValuePair);

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
        private void AddPropertyTypeAndProperty(Registration registration, KeyValuePair<string, string> property)
        {
            var propertyType = this.CheckPropertyType(property.Key);

            if (propertyType.Id == 0)
            {
                propertyType = this.AddPropertyTypeToDbSet(property.Key);
            }

            if (propertyType.Id > -1)
            {
                this.AddProperty(propertyType, property, registration);
            }
        }
    }
}