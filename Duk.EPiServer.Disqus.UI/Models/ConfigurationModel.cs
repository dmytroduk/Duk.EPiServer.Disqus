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
        /// Gets or sets a value indicating whether Disqus comments are enabled on website.
        /// </summary>
        /// <value>
        ///   <c>true</c> if Disqus comments are enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }
    }
}
