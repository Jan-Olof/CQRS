namespace Api.WebApi.Areas.HelpPage.ModelDescriptions
{
    using System;

    /// <summary>
    /// The parameter annotation.
    /// </summary>
    public class ParameterAnnotation
    {
        /// <summary>
        /// Gets or sets the annotation attribute.
        /// </summary>
        public Attribute AnnotationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public string Documentation { get; set; }
    }
}