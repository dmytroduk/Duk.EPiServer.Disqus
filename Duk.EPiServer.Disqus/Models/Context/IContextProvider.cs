
namespace Duk.EPiServer.Disqus.Models.Context
{
    /// <summary>
    /// Provides context information that is used to generate Disqus code with corresponding parameters
    /// </summary>
    public interface IContextProvider
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        IContext GetContext();
    }
}
