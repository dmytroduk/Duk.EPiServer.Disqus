using System;
using System.Web.UI;
using Duk.EPiServer.Disqus.Models;
using EPiServer.Core;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Duk.EPiServer.Disqus
{
    /// <summary>
    /// Control to render Disqus comments block on Web Forms page
    /// </summary>
    public class CommentsControl : Control, IContentDataControl<CommentsBlock>
    {
        private readonly Lazy<IRenderingService> _renderingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsControl"/> class.
        /// </summary>
        public CommentsControl()
        {
            _renderingService = new Lazy<IRenderingService>(() => ServiceLocator.Current.GetInstance<IRenderingService>());
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            var requiredResourcesList = ServiceLocator.Current.GetInstance<IRequiredClientResourceList>();
            _renderingService.Value.RegisterClientResources(requiredResourcesList);
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(_renderingService.Value.Render());   
        }

        /// <summary>
        /// Gets or sets the current data.
        /// </summary>
        /// <value>
        /// The current data.
        /// </value>
        IContentData IContentDataControl.CurrentData
        {
            get { return CurrentData; }
            set { CurrentData = (CommentsBlock)value; }
        }

        /// <summary>
        /// Gets or sets the current Disqus comments block data.
        /// </summary>
        /// <value>
        /// The current Disqus comments block data.
        /// </value>
        public CommentsBlock CurrentData { get; set; }
    }
}
