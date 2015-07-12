namespace Common.DataTransferObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// The gdto is a generic registration dto used in commands.
    /// </summary>
    public class Gdto
    {
        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public IList<KeyValuePair<string, string>> Properties { get; set; }
    }
}