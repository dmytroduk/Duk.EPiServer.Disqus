using EPiServer.Editor;
using EPiServer.Web.Routing;

namespace Duk.EPiServer.Disqus.Models.Context
{
    /// <summary>
    /// Default context provider, returns context data based on a current CMS page
    /// </summary>
    public class ContentContextProvider : IContextProvider
    {
        private readonly PageRouteHelper _pageRouteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentContextProvider"/> class.
        /// </summary>
        /// <param name="pageRouteHelper">The page route helper.</param>
        public ContentContextProvider(PageRouteHelper pageRouteHelper)
        {
            _pageRouteHelper = pageRouteHelper;
        }

        /// <summary>
        /// Gets the context information specific for CMS page.
        /// </summary>
        /// <returns></returns>
        public IContext GetContext()
        {
            var context = new ContentContext();
            
            // TODO: check why page can be null
            if (_pageRouteHelper.Page != null)
            {
                context.Identifier = _pageRouteHelper.Page.ContentGuid.ToString();
                
                // TODO: resolve external URL for each page?
                // context.Url = ...
            }

            context.IsInEditMode = PageEditing.PageIsInEditMode;

            return context;
        }
    }
}
