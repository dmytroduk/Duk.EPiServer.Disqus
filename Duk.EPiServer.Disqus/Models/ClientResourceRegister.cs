using System.Web;
using EPiServer.Framework.Web.Resources;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Registers Disqus client resources to be rendered in certain areas
    /// </summary>
    [ClientResourceRegistrator]
    public class ClientResourceRegister : IClientResourceRegistrator
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
        public void RegisterResources(IRequiredClientResourceList requiredResources)
        {
            _renderingService.RenderInAreas(requiredResources);
        }
    }
}
