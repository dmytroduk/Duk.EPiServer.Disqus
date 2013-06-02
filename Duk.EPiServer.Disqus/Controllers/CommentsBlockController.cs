using System.Web.Mvc;
using Duk.EPiServer.Disqus.Models;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;

namespace Duk.EPiServer.Disqus.Controllers
{
    /// <summary>
    /// Renders Disqus comments block on MVC pages
    /// </summary>
    public class CommentsBlockController : BlockController<CommentsBlock>
    {
        private readonly IRequiredClientResourceList _requiredResources;
        private readonly IRenderingService _renderingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsBlockController"/> class.
        /// </summary>
        public CommentsBlockController()
        {
            _requiredResources = ServiceLocator.Current.GetInstance<IRequiredClientResourceList>();
            _renderingService = ServiceLocator.Current.GetInstance<IRenderingService>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsBlockController" /> class.
        /// </summary>
        /// <param name="requiredResources">The required client resources list.</param>
        /// <param name="renderingService">The Disqus rendering service.</param>
        public CommentsBlockController(IRequiredClientResourceList requiredResources, IRenderingService renderingService)
        {
            _requiredResources = requiredResources;
            _renderingService = renderingService;
        }

        /// <summary>
        /// Injects loader script and outputs Disqus comments code.
        /// </summary>
        /// <param name="currentBlock">The Disqus therad block.</param>
        /// <returns></returns>
        public override ActionResult Index(CommentsBlock currentBlock)
        {
            _renderingService.RegisterClientResources(_requiredResources);
            return new ContentResult { Content = _renderingService.Render() };
        }
    }
}
