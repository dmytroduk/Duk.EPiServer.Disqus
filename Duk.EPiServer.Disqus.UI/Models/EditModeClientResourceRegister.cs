using System.Globalization;
using System.Web;
using Duk.EPiServer.Disqus.Models.Context;
using EPiServer.Framework.Localization;
using EPiServer.Framework.Web.Resources;

namespace Duk.EPiServer.Disqus.UI.Models
{
    /// <summary>
    /// Registers client resources that are required to indicate Disqus threads in Edit UI.
    /// </summary>
    [ClientResourceRegister]
    public class EditModeClientResourceRegister : IClientResourceRegister
    {
        private readonly LocalizationService _localizationService;
        private readonly IContextProvider _contextProvider;

        public const string ResourcesAreRegisteredKey = "Duk.EPiServer.Disqus.StylesAreRegistered";

        /// <summary>
        /// Initializes a new instance of the <see cref="EditModeClientResourceRegister" /> class.
        /// </summary>
        /// <param name="localizationService">The localization service.</param>
        /// <param name="contextProvider">The context provider.</param>
        public EditModeClientResourceRegister(LocalizationService localizationService, IContextProvider contextProvider)
        {
            _localizationService = localizationService;
            _contextProvider = contextProvider;
        }

        /// <summary>
        /// Registers the Disqus client resources to be rendered in defined areas.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="context">The context.</param>
        public void RegisterResources(IRequiredClientResourceList requiredResources, HttpContextBase context)
        {
            var renderingContext = _contextProvider.GetContext();

            if (!renderingContext.IsInPreviewMode && !renderingContext.IsInEditMode)
            {
                return;
            }

            if (renderingContext.IsInEditMode)
            {
                requiredResources.Require("duk-disqus.EditMode");

                // Hack: output text in Edit mode to indicate thread placeholders that were not used by Disqus.
                // For example, it can be a case when there are several placeholders on a page.
                // We have to inject inline CSS here to be able to provide localized message text.
                var inlineStyle = string.Format(CultureInfo.InvariantCulture, "div#disqus_thread:empty:before {{content: '{0}';}}",
                                                _localizationService.GetString("/disqus/ui/rendering/severalthreadsonpage"));

                requiredResources.RequireStyleInline(inlineStyle, "duk-disqus.EditMode.severalThreadsIndicator", null);
            }

            // Inject the following styles for Edit and Preview modes
            requiredResources.Require("duk-disqus.PreviewMode");

            context.Items[ResourcesAreRegisteredKey] = true;
        }

        /// <summary>
        /// Determines whether resources are registered.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static bool AreRegistered(HttpContextBase context)
        {
            var registered = context.Items[ResourcesAreRegisteredKey];
            return registered != null && (bool) registered;
        }
    }
}
