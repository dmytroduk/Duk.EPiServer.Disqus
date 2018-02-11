using Duk.EPiServer.Disqus.Models;
using Duk.EPiServer.Disqus.Models.Configuration;
using Duk.EPiServer.Disqus.Models.Context;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Duk.EPiServer.Disqus
{
    /// <summary>
    /// Configures service locator with default implementations
    /// </summary>
    [InitializableModule]
    public class InitializationModule : IConfigurableModule
    {
        /// <summary>
        /// Configures the service container.
        /// </summary>
        /// <param name="context">The context.</param>
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IConfigurationProvider, DatabaseConfigurationProvider>();
            context.Services.AddTransient<IContextProvider, ContentContextProvider>();
            context.Services.AddTransient<IRenderingService, RenderingService>();
            context.Services.AddTransient<ICodeBuilder, CodeBuilder>();
        }

        /// <summary>
        /// Called when initializing.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        /// Called when uninitializing.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Uninitialize(InitializationEngine context)
        {
        }

        /// <summary>
        /// Preloads the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void Preload(string[] parameters)
        {
        }
    }
}
