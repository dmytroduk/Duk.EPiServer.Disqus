using System.Globalization;
using System.Web;
using EPiServer.Editor;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="EditModeClientResourceRegister"/> class.
        /// </summary>
        /// <param name="localizationService">The localization service.</param>
        public EditModeClientResourceRegister(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        /// <summary>
        /// Registers the Disqus client resources to be rendered in defined areas.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="context">The context.</param>
        public void RegisterResources(IRequiredClientResourceList requiredResources, HttpContextBase context)
        {
            if (!PageEditing.PageIsInEditMode)
            {
                return;
            }

            requiredResources.Require("duk-disqus.EditMode");

            // Hack: output text in Edit mode to indicate thread placeholders that were not used by Disqus.
            // For example, it can be a case when there are several placeholders on a page.
            // We have to inject inline CSS here to be able to provide localized message text.
            var inlineStyle = string.Format(CultureInfo.InvariantCulture, "div#disqus_thread:empty:before {{content: '{0}';}}",
                                            _localizationService.GetString("/disqus/ui/rendering/severalthreadsonpage"));

            requiredResources.RequireStyleInline(inlineStyle, "duk-disqus.EditMode.severalThreadsIndicator", null);
        }
    }
}
