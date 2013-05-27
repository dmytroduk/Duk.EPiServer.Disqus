using Duk.EPiServer.Disqus.Models.Configuration;
using Duk.EPiServer.Disqus.Models.Context;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Generates Disqus loader script and comments thread code based on the configuration and the context
    /// </summary>
    public interface ICodeBuilder
    {
        /// <summary>
        /// Creates the Disqus loader script.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The current context.</param>
        /// <returns></returns>
        string CreateLoaderScript(IConfiguration configuration, IContext context);

        /// <summary>
        /// Creates the Disqus comments thread code.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The current context.</param>
        /// <returns></returns>
        string CreateThreadCode(IConfiguration configuration, IContext context);
    }
}