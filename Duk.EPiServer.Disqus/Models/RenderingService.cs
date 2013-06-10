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
        private const string PreviewIdentifier = "Preview_F6241C2F-EA1D-4182-9FD9-864D4C178047";
        private const string PreviewTitle = "Preview";

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
            var context = GetContext();

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
            var context = GetContext();

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

            var context = GetContext();
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
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        private IContext GetContext()
        {
            return CorrectContext(_contextProvider.GetContext());
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

            requiredResources.RequireScriptInline(renderingModel.LoaderScript, "duk-disqus.Loader", null).AtFooter();

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

            var renderingResult = new StringBuilder(renderingModel.ThreadCode);
            if (context.IsInEditMode && _editModeRendering.Value != null)
            {
                // Do specific rendering for Edit UI
                _editModeRendering.Value.Render(renderingModel, ref renderingResult);
            }

            return renderingResult.ToString();
        }

        /// <summary>
        /// Creates the Disqus comments rendering model based on context and configuration.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private RenderingModel CreateRenderingModel(IConfiguration configuration, IContext context)
        {
            var isDisqusEnabled = IsDisqusEnabled(configuration);
            var renderingModel = new RenderingModel { IsEnabled = isDisqusEnabled };
            if (isDisqusEnabled)
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
            object renderingEditModeExtension;
            return _serviceLocator.TryGetExistingInstance(typeof (IRenderingEditModeExtension), out renderingEditModeExtension)
                       ? renderingEditModeExtension as IRenderingEditModeExtension
                       : null;
        }

        /// <summary>
        /// Determines whether Disqus comments are enabled on site.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if Disqus is enabled; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsDisqusEnabled(IConfiguration configuration)
        {
            return configuration != null && configuration.Enabled && !String.IsNullOrWhiteSpace(configuration.ShortName); 
        }

        /// <summary>
        /// Determines whether actual comments should be displayed on a page.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if actual comments should be displayed on a page; otherwise, <c>false</c>.
        /// </returns>
        private static bool ShouldDisplayActualComments(IContext context)
        {
            return context != null && context.IsAvailableOnSite && !context.IsInEditMode;
        }

        /// <summary>
        /// Corrects the context. 
        /// </summary>
        /// <param name="currentContext">The current context.</param>
        /// <returns>Current context if actual comments should be displayed, 
        /// otherwise returns corrected context in order to render dummy preview discussion.</returns>
        private static IContext CorrectContext(IContext currentContext)
        {
            return ShouldDisplayActualComments(currentContext) ? currentContext : CreatePreviewContext(currentContext);
        }

        /// <summary>
        /// Creates the context that should be used to render dummy preview discussion.
        /// </summary>
        /// <param name="originalContext">The original context.</param>
        /// <returns></returns>
        private static IContext CreatePreviewContext(IContext originalContext)
        {
            return new ContentContext
            {
                Identifier = PreviewIdentifier,
                SiteUrl = originalContext.SiteUrl,
                Url = originalContext.SiteUrl,
                Title = PreviewTitle,
                IsAvailableOnSite = originalContext.IsAvailableOnSite,
                IsInEditMode = originalContext.IsInEditMode
            };
        }
    }
}
