
namespace Duk.EPiServer.Disqus.Models.Context
{
    /// <summary>
    /// Context DTO
    /// </summary>
    public class ContentContext : IContext
    {
        /// <summary>
        /// Gets the value that should be used as Disqus identifier.
        /// Tells the Disqus service how to identify the current page.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <remarks>
        /// When the Disqus embed is loaded, the identifier is used to look up the correct thread.
        /// If disqus_identifier is undefined, the page's URL will be used.
        /// </remarks>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets the value that should be used as Disqus URL parameter.
        /// Tells the Disqus service the URL of the current page.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        /// <remarks>
        /// This URL is used to look up or create a thread if disqus_identifier is undefined.
        /// In addition, this URL is always saved when a thread is being created so that Disqus knows what page a thread belongs to.
        /// </remarks>
        public string Url { get; set; }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets the value that should be used as Disqus title parameter.
        /// This is used when creating the thread on Disqus for the first time.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        /// <remarks>
        /// If undefined, Disqus will use the title attribute of the page.
        /// If that attribute could not be used, Disqus will use the URL of the page.
        /// </remarks>
        public string Title { get; set; }

        /// <summary>
        /// Gets the value that should be used as Disqus category ID parameter.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        /// <remarks>
        /// Tells the Disqus service the category to be used for the current page.
        /// This is used when creating the thread on Disqus for the first time.
        /// </remarks>
        public string CategoryId { get; set; }


        /// <summary>
        /// Gets a value indicating whether a page is opened in Edit mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if page is opened in Edit mode; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsInEditMode { get; set; }

        /// <summary>
        /// Gets a value indicating whether a page is opened in Preview mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if page is opened in Preview mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsInPreviewMode { get; set; }

        /// <summary>
        /// Gets a value indicating whether this page is available for site visitors.
        /// </summary>
        /// <value>
        /// <c>true</c> if this page is available on site; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Usually the  page is considered as available when it is published and is visible on site.
        /// </remarks>
        public bool IsAvailableOnSite { get; set; }
    }
}
