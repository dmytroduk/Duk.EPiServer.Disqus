using System.Web;
using EPiServer.Framework.Web.Resources;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Registers Disqus client resources to be rendered in certain areas
    /// </summary>
    [ClientResourceRegister]
    public class ClientResourceRegister : IClientResourceRegister
    {
        private readonly IRenderingService _renderingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientResourceRegister" /> class.
        /// </summary>
        /// <param name="renderingService">The Disqus service.</param>
        public ClientResourceRegister(IRenderingService renderingService)
        {
            _renderingService = renderingService;
        }

        /// <summary>
        /// Registers the Disqus client resources to be rendered in defined areas.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="context">The context.</param>
        public void RegisterResources(IRequiredClientResourceList requiredResources, HttpContextBase context)
        {
            _renderingService.RenderInAreas(requiredResources);
        }
    }
}
