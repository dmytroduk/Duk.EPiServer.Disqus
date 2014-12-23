using System;
using EPiServer;
using EPiServer.Configuration;
using EPiServer.Core;
using EPiServer.Editor;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;

namespace Duk.EPiServer.Disqus.Models.Context
{
    /// <summary>
    /// Default context provider, returns context data based on a current CMS page
    /// </summary>
    public class ContentContextProvider : IContextProvider
    {
        private readonly PageRouteHelper _pageRouteHelper;
        private readonly IEnterpriseSettings _enterpriseSettings;
        private readonly UrlResolver _urlResolver;
        private readonly Lazy<IContext> _currentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentContextProvider" /> class.
        /// </summary>
        /// <param name="pageRouteHelper">The page route helper.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="enterpriseSettings">The enterprise settings.</param>
        public ContentContextProvider(PageRouteHelper pageRouteHelper, UrlResolver urlResolver, IEnterpriseSettings enterpriseSettings)
        {
            _pageRouteHelper = pageRouteHelper;
            _urlResolver = urlResolver;
            _enterpriseSettings = enterpriseSettings;
            _currentContext = new Lazy<IContext>(GetContextInternal);
        }

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <returns></returns>
        public IContext GetContext()
        {
            return _currentContext.Value;
        }

        /// <summary>
        /// Gets the context information specific for the current CMS page.
        /// </summary>
        /// <returns></returns>
        private ContentContext GetContextInternal()
        {
            var context = new ContentContext
            {
                IsInEditMode = PageEditing.PageIsInEditMode,
                IsInPreviewMode = RequestSegmentContext.CurrentContextMode == ContextMode.Preview
            };
            
            Settings siteSettings;

            if (_pageRouteHelper.Page != null)
            {
                context.Identifier = _pageRouteHelper.Page.ContentGuid.ToString();

                context.IsAvailableOnSite = IsAvailableOnSite(_pageRouteHelper.Page);

                siteSettings = _enterpriseSettings.GetSettingsFromContent(_pageRouteHelper.Page.PageLink, true);

                if (context.IsAvailableOnSite && !string.IsNullOrWhiteSpace(context.SiteUrl))
                {
                    var externalUrl = CreateExternalUrl(_pageRouteHelper.Page, context.SiteUrl);
                    context.Url = !string.IsNullOrWhiteSpace(externalUrl) ? externalUrl : null;
                }
            }
            else
            {
                context.IsAvailableOnSite = false;
                siteSettings = Settings.Instance;
            }

            context.SiteUrl = siteSettings != null ? siteSettings.SiteUrl.ToString() : null;

            return context;
        }

        /// <summary>
        /// Determines whether the page is available on site.
        /// </summary>
        /// <param name="pageData">The page data.</param>
        /// <returns>
        ///   <c>true</c> if the page is available on site; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsAvailableOnSite(PageData pageData)
        {
            return pageData.Status == VersionStatus.Published &&
                   !pageData.IsPendingPublish &&
                   !pageData.IsDeleted &&
                   pageData.IsVisibleOnSite();
        }

        /// <summary>
        /// Creates the external page URL.
        /// </summary>
        /// <param name="pageData">The page data.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <returns></returns>
        private string CreateExternalUrl(PageData pageData, string siteUrl)
        {
            // TODO: always resolve path in view mode
            var pagePath = _urlResolver.GetVirtualPath(pageData.PageLink, pageData.LanguageID);
            var pagePathUrlBuilder = new UrlBuilder(pagePath);

            var uriBuilder = new UrlBuilder(siteUrl)
            {
                Path = pagePathUrlBuilder.Path,
                QueryCollection = pagePathUrlBuilder.QueryCollection
            };

            return uriBuilder.ToString();
        }
    }
}
