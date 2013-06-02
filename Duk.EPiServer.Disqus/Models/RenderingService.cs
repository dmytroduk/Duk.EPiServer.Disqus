using System;
using System.Linq;
using System.Text;
using Duk.EPiServer.Disqus.Models.Configuration;
using Duk.EPiServer.Disqus.Models.Context;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Generates Disqus comments thread and injects client resources that should be rendered.
    /// </summary>
    public class RenderingService : IRenderingService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly Lazy<IRenderingEditModeExtension> _editModeRendering;
        private readonly ICodeBuilder _codeBuilder;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IContextProvider _contextProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingService" /> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <param name="codeBuilder">The Disqus code builder.</param>
        /// <param name="configurationProvider">The Disqus configuration provider.</param>
        /// <param name="contextProvider">The context provider.</param>
        public RenderingService(IServiceLocator serviceLocator, ICodeBuilder codeBuilder, IConfigurationProvider configurationProvider, IContextProvider contextProvider)
        {
            _serviceLocator = serviceLocator;
            _editModeRendering = new Lazy<IRenderingEditModeExtension>(GetEditModeRenderingExtension);
            _codeBuilder = codeBuilder;
            _configurationProvider = configurationProvider;
            _contextProvider = contextProvider;
        }

        /// <summary>
        /// Registers the client resources that should be injected on page to enable Disqus comments.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        public void RegisterClientResources(IRequiredClientResourceList requiredResources)
        {
            var configuration = _configurationProvider.Load();
            var context = _contextProvider.GetContext();

            var renderingModel = CreateRenderingModel(configuration, context);

            RegisterClientResources(requiredResources, context, renderingModel);
        }

        /// <summary>
        /// Returns the Disqus comments thread code that should be rendered on a page.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            var configuration = _configurationProvider.Load();
            var context = _contextProvider.GetContext();

            var renderingModel = CreateRenderingModel(configuration, context);

            return Render(context, renderingModel);
        }


        /// <summary>
        /// Renders the Disqus comments thread code in specified area.
        /// Also registers Disqus loader script to be injected on a page.
        /// </summary>
        /// <param name="requiredResources">The required resources list.</param>
        public void RenderInAreas(IRequiredClientResourceList requiredResources)
        {
            var configuration = _configurationProvider.Load();
            if (!configuration.RenderingAreas.Any())
            {
                return;
            }

            var context = _contextProvider.GetContext();
            var renderingModel = CreateRenderingModel(configuration, context);

            RegisterClientResources(requiredResources, context, renderingModel);

            var threadCode = Render(context, renderingModel);

            if (!String.IsNullOrWhiteSpace(threadCode))
            {
                configuration.RenderingAreas.ToList().ForEach(renderingArea =>
                    requiredResources.RequireHtmlInline(threadCode).AtArea(renderingArea));
            }
        }

        /// <summary>
        /// Registers the client resources that should be injected on page to enable Disqus comments.
        /// </summary>
        /// <param name="requiredResources">The required resources.</param>
        /// <param name="context">The context.</param>
        /// <param name="renderingModel">The rendering model.</param>
        private void RegisterClientResources(IRequiredClientResourceList requiredResources, IContext context, RenderingModel renderingModel)
        {
            if (!renderingModel.IsEnabled && !context.IsInEditMode)
            {
                return;
            }

            requiredResources.RequireScriptInline(renderingModel.LoaderScript, "epi-disqus.Loader", null).AtFooter();

            if (context.IsInEditMode && _editModeRendering.Value != null)
            {
                // Do specific injections for Edit UI
                _editModeRendering.Value.RegisterClientResources(requiredResources, renderingModel);
            }
        }

        /// <summary>
        /// Returns the Disqus comments thread code and additional rendering for Edit UI.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="renderingModel">The rendering model.</param>
        /// <returns></returns>
        private string Render(IContext context, RenderingModel renderingModel)
        {
            if (!renderingModel.IsEnabled && !context.IsInEditMode)
            {
                return String.Empty;
            }

            var strings = new StringBuilder(renderingModel.ThreadCode);
            if (context.IsInEditMode && _editModeRendering.Value != null)
            {
                // Do specific rendering for Edit UI
                strings.Append(_editModeRendering.Value.Render(renderingModel));
            }

            return strings.ToString();
        }

        /// <summary>
        /// Creates the Disqus comments rendering model based on context and configuration.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private RenderingModel CreateRenderingModel(IConfiguration configuration, IContext context)
        {
            var renderingModel = new RenderingModel { IsEnabled = configuration.IsEnabled };
            if (configuration.IsEnabled)
            {
                renderingModel.LoaderScript = _codeBuilder.CreateLoaderScript(configuration, context);
                renderingModel.ThreadCode = _codeBuilder.CreateThreadCode(configuration, context);
            }
            return renderingModel;
        }

        /// <summary>
        /// Gets the rendering extension for Edit mode.
        /// </summary>
        /// <returns></returns>
        private IRenderingEditModeExtension GetEditModeRenderingExtension()
        {
            try
            {
                return _serviceLocator.GetInstance<IRenderingEditModeExtension>();
            }
            catch (ActivationException)
            {
                return null;
            }
        }
    }
}
