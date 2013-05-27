
namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Disqus comments thread model that contains loader script and Disqus thread code that should be rendered on a page.
    /// </summary>
    public class RenderingModel
    {
        /// <summary>
        /// Gets or sets the Disqus comments thread code.
        /// </summary>
        /// <value>
        /// The Disqus thread code.
        /// </value>
        public string ThreadCode { get; set; }

        /// <summary>
        /// Gets or sets the Disqus loader script.
        /// </summary>
        /// <value>
        /// The loader script.
        /// </value>
        public string LoaderScript { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Disqus comments are configured and enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if Disqus comments are configured and enabled.; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }
    }
}
