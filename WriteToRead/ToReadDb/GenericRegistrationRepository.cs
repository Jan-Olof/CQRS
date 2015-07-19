namespace WriteToRead.ToReadDb
{
    using System;
    using System.Collections.ObjectModel;

    using DataAccess.Read.Dal.CodeFirst.DbContext;

    using Domain.Read.Entities;

    using NLog;

    using WriteToRead.Interfaces;

    /// <summary>
    /// Class that is used to transfer data from the Write database to the Read database.
    /// </summary>
    public class GenericRegistrationRepository : IGenericRegistrationRepository
    {
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
                throw new ArgumentNullException("readContext");
            }

            this.readContext = readContext;
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
        /// Add registration to db set.
        /// </summary>
        public Registration AddRegistrationToDbSet(RegistrationType type, DateTime timestamp, string name)
        {
            try
            {
                var registration = Registration.CreateRegistration(type, timestamp, name);

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
        /// Check which property type a property is and return the type.
        /// </summary>
        public PropertyType CheckPropertyType(string type)
        {
            try
            {
                if (string.Equals(type, "Name", StringComparison.CurrentCultureIgnoreCase))
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
        /// Add property to db set.
        /// </summary>
        public Property AddPropertyToDbSet(PropertyType type, string value, Registration registration)
        {
            try
            {
                var property = Property.CreateProperty(type, value, registration);

                this.readContext.Properties.Add(property);

                return property;
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
    }
}