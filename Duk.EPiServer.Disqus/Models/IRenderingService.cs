
using EPiServer.Framework.Web.Resources;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Generates model object that is used to inject Disqus resources and render Disqus code on a page
    /// </summary>
    public interface IRenderingService
    {
        /// <summary>
        /// Registers the client resources that should be injected on page to enable Disqus comments.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        void RegisterClientResources(IRequiredClientResourceList requiredResources);

        /// <summary>
        /// Returns the Disqus comments thread code that should be rendered on a page.
        /// </summary>
        /// <returns></returns>
        string Render();

        /// <summary>
        /// Renders the Disqus comments thread code in specified area.
        /// Also registers Disqus loader script to be injected on a page.
        /// </summary>
        /// <param name="requiredResources">The required resources list.</param>
        void RenderInAreas(IRequiredClientResourceList requiredResources);

    }
}