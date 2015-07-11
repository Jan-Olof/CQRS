namespace Api.WebApi.Areas.HelpPage.ModelDescriptions
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// The complex type model description.
    /// </summary>
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexTypeModelDescription"/> class.
        /// </summary>
        public ComplexTypeModelDescription()
        {
            this.Properties = new Collection<ParameterDescription>();
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}