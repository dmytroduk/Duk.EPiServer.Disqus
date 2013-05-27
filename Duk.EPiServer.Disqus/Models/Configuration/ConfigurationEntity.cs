using System;
using System.Collections.Generic;
using EPiServer.Data.Dynamic;

namespace Duk.EPiServer.Disqus.Models.Configuration
{
    /// <summary>
    /// Data entity to store Disqus settings in DDS
    /// </summary>
    [EPiServerDataStore(AutomaticallyRemapStore = true)]
    public class ConfigurationEntity
    {
        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether developer mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if developer mode is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool DeveloperMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should show powered by notice.
        /// </summary>
        /// <value>
        /// <c>true</c> if should show powered by notice; otherwise, <c>false</c>.
        /// </value>
        public bool ShowPoweredByNotice { get; set; }

        /// <summary>
        /// Gets or sets the rendering areas where Disqus comments thread should be rendered.
        /// </summary>
        /// <value>
        /// The rendering areas.
        /// </value>
        public List<string> RenderingAreas { get; set; }

        /// <summary>
        /// Gets or sets the data entity ID.
        /// </summary>
        /// <value>
        /// The entity ID.
        /// </value>
        public Guid Id { get; set; }
    }
}
