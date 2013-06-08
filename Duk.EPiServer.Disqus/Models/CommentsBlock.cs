using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Disqus comments block
    /// </summary>
    [ContentType(GUID = "1B9D9094-F895-41A6-B986-E5E40FC6DEE7", 
        DisplayName = "Disqus comments",
        Description = "Allows to add Disqus comments on a page.")]
    
    // Hack: unable to use partial path to the block icon;
    // have to implement custom attribute to resolve the path in add-on folder.
    [ModuleImageUrl("Duk.EPiServer.Disqus", "ClientResources/DisqusBlockIcon.png", false)]
    
    public class CommentsBlock : BlockData
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled. 
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>This is dummy property that cannot be editable in Edit UI. 
        /// We have to kepp it in order to make Disqus block "not null" when it is used as content property. 
        /// Otherwise block is considered as "null" and it is not rendered in view mode.</remarks>
        [Editable(false)]
        [Display(Name = "Enable Disqus comments",
            Description = "Disqus comments are always enabled by default."),]
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Sets the default values for new Disqus comments block.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            IsEnabled = true;
        }
    }
}
