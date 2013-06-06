using System.ComponentModel.DataAnnotations;

namespace Duk.EPiServer.Disqus.UI.Models
{
    /// <summary>
    /// Disqus configuration DTO
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Gets or sets the Disqus short name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        [Required]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether developer mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if developer mode is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool DeveloperMode { get; set; }
    }
}
