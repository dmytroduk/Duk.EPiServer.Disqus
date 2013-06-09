using System.Text;
using EPiServer.Framework.Web.Resources;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Rendering extension to output something specific when viewing a page in Edit UI.
    /// Should be implemented in Disqus UI part and be available on website where CMS Edit UI is installed.
    /// </summary>
    public interface IRenderingEditModeExtension
    {
        /// <summary>
        /// Registers client resources that should be injected when page is opened in Edit UI.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        void RegisterClientResources(IRequiredClientResourceList requiredResources, RenderingModel renderingModel);

        /// <summary>
        /// Returns string that should be appended to Disqus comments thread code when page is opened in Edit UI.
        /// </summary>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        /// <param name="basicRendering">The basic comments rendering.</param>
        void Render(RenderingModel renderingModel, ref StringBuilder basicRendering);
    }
}
