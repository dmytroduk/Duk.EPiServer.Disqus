using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Disqus comments block
    /// </summary>
    [ContentType(GUID = "1B9D9094-F895-41A6-B986-E5E40FC6DEE2")]
    // Hack: unable to use partial path to the block icon;
    // have to implement custom attribute to resolve the path in add-on folder.
    [ModuleImageUrl("Duk.EPiServer.Disqus", "ClientResources/DisqusBlockIcon.png", false)]
    public class CommentsBlock : BlockData
    {
    }
}
