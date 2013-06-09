
namespace Duk.EPiServer.Disqus.Models.Context
{
    /// <summary>
    /// Context data used to generate Disqus code with corresponding parameters
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Gets the value that should be used as Disqus identifier. 
        /// Tells the Disqus service how to identify the current page.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <remarks>When the Disqus embed is loaded, the identifier is used to look up the correct thread. 
        /// If disqus_identifier is undefined, the page's URL will be used.</remarks>
        string Identifier { get; }

        /// <summary>
        /// Gets the value that should be used as Disqus URL parameter.  
        /// Tells the Disqus service the URL of the current page. 
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        /// <remarks>This URL is used to look up or create a thread if disqus_identifier is undefined. 
        /// In addition, this URL is always saved when a thread is being created so that Disqus knows what page a thread belongs to.</remarks>
        string Url { get; }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        string SiteUrl { get; }

        /// <summary>
        /// Gets the value that should be used as Disqus title parameter. 
        /// This is used when creating the thread on Disqus for the first time.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        /// <remarks>If undefined, Disqus will use the title attribute of the page. 
        /// If that attribute could not be used, Disqus will use the URL of the page.</remarks>
        string Title { get; }

        /// <summary>
        /// Gets the value that should be used as Disqus category ID parameter.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        /// <remarks>Tells the Disqus service the category to be used for the current page. 
        /// This is used when creating the thread on Disqus for the first time.</remarks>
        string CategoryId { get; }

        /// <summary>
        /// Gets a value indicating whether a page is opened in Edit mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if page is opened in Edit mode; otherwise, <c>false</c>.
        /// </value>
        bool IsInEditMode { get; }

        /// <summary>
        /// Gets a value indicating whether this page is available for site visitors.
        /// </summary>
        /// <value>
        /// <c>true</c> if this page is available on site; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Usually the  page is considered as available when it is published and is visible on site.</remarks>
        bool IsAvailableOnSite { get; }
    }
}
