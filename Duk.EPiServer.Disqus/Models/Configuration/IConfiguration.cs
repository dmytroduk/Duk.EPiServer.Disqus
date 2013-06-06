using System.Collections.Generic;

namespace Duk.EPiServer.Disqus.Models.Configuration
{
    /// <summary>
    /// Disqus configuration
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets the Disqus short name, which is the unique identifier for a website as registered on Disqus.
        /// </summary>
        /// <value>
        /// The Disqus short name.
        /// </value>
        string ShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Disqus should work in developer mode, 
        /// for example when testing a site and application URL is not equal to website URL defined on Disqus.
        /// </summary>
        /// <value>
        ///   <c>true</c> if developer mode; otherwise, <c>false</c>.
        /// </value>
        bool DeveloperMode { get; set; }

        /// <summary>
        /// Gets or sets the rendering areas where Disqus comments thread should be rendered on pages.
        /// </summary>
        /// <value>
        /// The rendering areas.
        /// </value>
        IList<string> RenderingAreas { get; set; }
    }
}
