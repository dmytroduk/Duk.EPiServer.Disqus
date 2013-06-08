using System.Collections.Generic;
using System.Linq;

namespace Duk.EPiServer.Disqus.Models.Configuration
{
    /// <summary>
    /// Disqus configuration data
    /// </summary>
    public class DisqusConfiguration: IConfiguration
    {
        private List<string> _renderingAreas = new List<string>();

        /// <summary>
        /// Gets the Disqus short name, which is the unique identifier for a website as registered on Disqus.
        /// </summary>
        /// <value>
        /// The Disqus short name.
        /// </value>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Disqus comments are enabled on website.
        /// </summary>
        /// <value>
        ///   <c>true</c> if Disqus comments are enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets the rendering areas where Disqus comments thread should be rendered on pages.
        /// </summary>
        /// <value>
        /// The rendering areas.
        /// </value>
        public IList<string> RenderingAreas
        {
            get { return _renderingAreas; }
            set { _renderingAreas = value != null ? value.ToList() : new List<string>(); }
        }
    }
}