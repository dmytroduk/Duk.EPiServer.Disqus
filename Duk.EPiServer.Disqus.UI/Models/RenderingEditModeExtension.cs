using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Duk.EPiServer.Disqus.Models;
using EPiServer.Framework.Localization;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;

namespace Duk.EPiServer.Disqus.UI.Models
{
    /// <summary>
    /// Extension to render additional stuff when Disqus comments are rendered on page in Edit mode.
    /// </summary>
    public class RenderingEditModeExtension : IRenderingEditModeExtension
    {
        private readonly string _configurationUrl;
        private readonly LocalizationService _localizationService;
        private readonly HttpContextBase _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingEditModeExtension" /> class.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="moduleTable">The module table.</param>
        /// <param name="localizationService">The localization service.</param>
        public RenderingEditModeExtension(RequestContext requestContext, ModuleTable moduleTable, LocalizationService localizationService)
        {
            _configurationUrl = GetConfigurationUrl(requestContext, moduleTable);
            _localizationService = localizationService;
            _context = requestContext.HttpContext;
        }

        /// <summary>
        /// Registers client resources that should be injected when page is opened in Edit UI.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        public void RegisterClientResources(IRequiredClientResourceList requiredResources, RenderingModel renderingModel)
        {
            // Require scripts here. 
            // Styles should be require in client resource register to make sure that they are added in Header area before that area is rendered.

            // HACK: Ugly fallback to register client resources (styles) if it was not registered using IClientResourceRegistrator.
            // It may happen when templates don't meet the standard requirements for EPiServer CMS page templates 
            // and don't require/render client resources. For example: block preview template in Alloy.
            // Open question: Should we solve it here?

            // Check if resources were registered (normal flow).
            if (EditModeClientResourceRegister.AreRegistered(_context))
            {
                return;
            }
            // Fallback: register now
            var register = ServiceLocator.Current.GetAllInstances<IClientResourceRegistrator>()
                .FirstOrDefault(r => typeof (EditModeClientResourceRegister) == r.GetType());

            register?.RegisterResources(requiredResources);
        }

        /// <summary>
        /// Returns string that should be appended to Disqus comments thread code when page is opened in Edit UI.
        /// Also registers client resources that should be injected in Edit mode.
        /// </summary>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        /// <param name="basicRendering">The basic comments rendering.</param>
        public void Render(RenderingModel renderingModel, ref StringBuilder basicRendering)
        {
            if (renderingModel == null)
            {
                return;
            }
            if (basicRendering == null)
            {
                basicRendering = new StringBuilder();
            }
            // wrap comments in div
            basicRendering.Insert(0, "<div class='disqus-threadContainer'>");
            if (renderingModel.IsEnabled)
            {
                // Add overlay div to indicate preview discussion
                basicRendering.Append("<div class='disqus-previewDiscussionOverlay'><span class='disqus-previewDiscussionText'>");
                basicRendering.Append(_localizationService.GetString("/disqus/ui/rendering/previewmessage"));
                basicRendering.Append("</span></div>");

                // Hack: to make page reload and display Disqus comments thread in on-page editing mode.
                basicRendering.Append("<script></script>");
            }
            else
            {
                basicRendering.AppendFormat(CultureInfo.InvariantCulture,
                                     "<div class='disqus-disabledMessage'>{0} <a href='{2}' target='_top'>{1}</a></div>",
                                     _localizationService.GetString("/disqus/ui/rendering/notconfiguredmessage"),
                                     _localizationService.GetString("/disqus/ui/rendering/configurationlinkmessage"),
                                     _configurationUrl);
            }
            // closing wrapping div
            basicRendering.Append("</div>");
        }

        /// <summary>
        /// Gets the Disqus configuration URL.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="moduleTable">The module table.</param>
        /// <returns></returns>
        private static string GetConfigurationUrl(RequestContext requestContext, ModuleTable moduleTable)
        {
            ShellModule currentModule;
            var moduleArea = moduleTable.TryGetModule(typeof(RenderingEditModeExtension).Assembly, out currentModule)
                                 ? currentModule.Name
                                 : "EPiServer.Disqus.UI";

            var routeValues = new RouteValueDictionary { { "moduleArea", moduleArea } };
            return new UrlHelper(requestContext).Action("Index", "Settings", routeValues);
        }

    }
}
