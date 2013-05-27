namespace Duk.EPiServer.Disqus.Models.Configuration
{
    /// <summary>
    /// Provides Disqus configuration
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Loads the Disqus configuration.
        /// </summary>
        /// <returns>Disqus configuration</returns>
        IConfiguration Load();

        /// <summary>
        /// Saves the Disqus configuration.
        /// </summary>
        /// <param name="configuration">The Disqus configuration</param>
        void Save(IConfiguration configuration);

        /// <summary>
        /// Clears Disqus configuration and removes any existing data.
        /// </summary>
        void Clear();
    }
}
