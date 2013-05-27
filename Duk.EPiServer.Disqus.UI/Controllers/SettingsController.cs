using System.Web.Mvc;
using EPiServer.Shell.Navigation;
using EPiServer.Shell.Web.Mvc;

namespace Duk.EPiServer.Disqus.UI.Controllers
{
    /// <summary>
    /// Disqus settings controller
    /// </summary>
    public class SettingsController : Controller
    {
        private readonly IBootstrapper _bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsController"/> class.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public SettingsController(IBootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        /// <summary>
        /// Starts Disqus settings UI.
        /// </summary>
        /// <returns></returns>
        [MenuItem(MenuPaths.Global + "/disqus", 
            SortIndex = SortIndex.Last + 20, 
            TextResourceKey = "/disqus/ui/navigationtitle")]
        public ActionResult Index()
        {
            return View(_bootstrapper.BootstrapperViewName, 
                _bootstrapper.CreateViewModel("Disqus", ControllerContext, "Duk.EPiServer.Disqus.UI"));
        }
    }
}
