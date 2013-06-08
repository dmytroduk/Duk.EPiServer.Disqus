using System;
using System.IO;
using Duk.EPiServer.Disqus.Models;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;

namespace Duk.EPiServer.Disqus
{
    /// <summary>
    /// Disqus comments dynamic content
    /// </summary>
    [DynamicContentPlugIn(LanguagePath = "/disqus/dynamiccontent",
        DisplayName = "Disqus comments",
        Description = "Allows to add Disqus comments on a page.")]
    public class CommentsDynamicContent : IDynamicContentView, IDynamicContentDisplayType
    {
        /// <summary>
        /// Gets the dynamic content properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public PropertyDataCollection Properties
        {
            get { return new PropertyDataCollection(); }
        }

        /// <summary>
        /// Gets or sets the dynamic content state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State
        {
            get
            {
                return String.Empty;
            }
            set {}
        }

        /// <summary>
        /// Gets a value indicating whether content should be rendered as as block element.
        /// </summary>
        /// <value>
        /// <c>true</c> if render as block element; otherwise, <c>false</c>.
        /// </value>
        public bool RenderAsBlockElement
        {
            get { return true; }
        }

        /// <summary>
        /// Renders the Disqus comments thread as dynemic content.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Render(TextWriter writer)
        {
            var renderingService = ServiceLocator.Current.GetInstance<IRenderingService>();
            var requiredResourcesList = ServiceLocator.Current.GetInstance<IRequiredClientResourceList>();

            renderingService.RegisterClientResources(requiredResourcesList);
            writer.Write(renderingService.Render());
        }

    }
}
