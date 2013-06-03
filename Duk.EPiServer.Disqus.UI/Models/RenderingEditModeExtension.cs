using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Duk.EPiServer.Disqus.Models;
using EPiServer.Framework.Localization;
using EPiServer.Framework.Web.Resources;
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
        }

        /// <summary>
        /// Registers client resources that should be injected when page is opened in Edit UI.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        public void RegisterClientResources(IRequiredClientResourceList requiredResources, RenderingModel renderingModel)
        {
            if (renderingModel == null || requiredResources == null)
            {
                return;
            }

            requiredResources.Require("duk-disqus.EditMode");

            if (!renderingModel.IsEnabled)
            {
                return;
            }

            // Hack: output text in Edit mode to indicate thread placeholders that were not used by Disqus.
            // For example, it can be a case when there are several placeholders on a page.
            // We have to inject inline CSS here to be able to provide localized message text.
            var inlineStyle = String.Format(CultureInfo.InvariantCulture, "div#disqus_thread:empty:before {{content: '{0}';}}",
                                            _localizationService.GetString("/disqus/ui/rendering/severalthreadsonpage"));

            requiredResources.RequireStyleInline(inlineStyle, "duk-disqus.EditMode.severalThreadsIndicator", null);
        }

        /// <summary>
        /// Returns string that should be appended to Disqus comments thread code when page is opened in Edit UI.
        /// Also registers client resources that should be injected in Edit mode.
        /// </summary>
        /// <param name="renderingModel">The Disqus comments rendering model.</param>
        /// <returns></returns>
        public string Render(RenderingModel renderingModel)
        {
            if (renderingModel == null)
            {
                return String.Empty;
            }
            var strings = new StringBuilder();
            if (renderingModel.IsEnabled)
            {
                // Hack: to make page reload and display Disqus comments thread in on-page editing mode.
                strings.Append("<script></script>");
            }
            else
            {
                strings.AppendFormat(CultureInfo.InvariantCulture,
                                     "<div class='disqus-disabledMessage'>{0} <a href='{2}' target='_top'>{1}</a></div>",
                                     _localizationService.GetString("/disqus/ui/rendering/notconfiguredmessage"),
                                     _localizationService.GetString("/disqus/ui/rendering/configurationlinkmessage"),
                                     _configurationUrl);
            }
            return strings.ToString();
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
