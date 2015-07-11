namespace Api.WebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The ModelDocumentationProvider interface.
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// The get documentation.
        /// </summary>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// The get documentation.
        /// </summary>
        string GetDocumentation(Type type);
    }
}