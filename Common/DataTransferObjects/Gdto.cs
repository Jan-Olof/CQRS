namespace Common.DataTransferObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// The gdto.
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
        public ICollection<KeyValuePair<string, string>> Properties { get; set; }
    }
}