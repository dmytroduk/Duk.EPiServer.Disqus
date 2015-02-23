using System.Linq;
using Duk.EPiServer.Disqus.Models;
using Duk.EPiServer.Disqus.Models.Configuration;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework;
using EPiServer.Globalization;
using EPiServer.Packaging;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace Duk.EPiServer.Disqus
{
    /// <summary>
    /// Add-on initializer, performs required actions after add-on installation and before add-on uninstallation.
    /// </summary>
    [ModuleDependency(typeof(PackagingInitialization))]
    [ModuleDependency(typeof(global::EPiServer.Web.InitializationModule))]
    public class AddonInitializer : PackageInitializer
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IContentModelUsage _contentModelUsage;
        private readonly LanguageSelectorFactory _languageSelectorFactory;
        private readonly IConfigurationProvider _configurationProvider;

        private IContentRepository ContentRepository
        {
            get { return _contentRepository ?? ServiceLocator.Current.GetInstance<IContentRepository>(); }
        }

        private IContentTypeRepository ContentTypeRepository
        {
            get { return _contentTypeRepository ?? ServiceLocator.Current.GetInstance<IContentTypeRepository>(); }
        }

        private IContentModelUsage ContentModelUsage
        {
            get { return _contentModelUsage ?? ServiceLocator.Current.GetInstance<IContentModelUsage>(); }
        }

        private LanguageSelectorFactory LanguageSelectorFactory
        {
            get { return _languageSelectorFactory ?? ServiceLocator.Current.GetInstance<LanguageSelectorFactory>(); }
        }

        private IConfigurationProvider ConfigurationProvider
        {
            get { return _configurationProvider ?? ServiceLocator.Current.GetInstance<IConfigurationProvider>(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonInitializer"/> class.
        /// </summary>
        public AddonInitializer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonInitializer"/> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="contentTypeRepository">The content type repository.</param>
        /// <param name="contentModelUsage">The content model usage.</param>
        /// <param name="languageSelectorFactory">The language selector factory.</param>
        /// <param name="configurationProvider">The configuration provider.</param>
        public AddonInitializer(IContentRepository contentRepository, IContentTypeRepository contentTypeRepository,
            IContentModelUsage contentModelUsage, LanguageSelectorFactory languageSelectorFactory,
            IConfigurationProvider configurationProvider)
        {
            _contentRepository = contentRepository;
            _contentTypeRepository = contentTypeRepository;
            _contentModelUsage = contentModelUsage;
            _languageSelectorFactory = languageSelectorFactory;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Executes after Disqus comments add-on is installed to create default instance of Disqus comments block.
        /// </summary>
        public override void AfterInstall()
        {
            var blockType = ContentTypeRepository.Load<CommentsBlock>();
            var defaultDisqusBlock = ContentRepository.GetDefault<IContent>(ContentReference.GlobalBlockFolder, blockType.ID, ContentLanguage.PreferredCulture);
            defaultDisqusBlock.Name = "Disqus comments";
            ContentRepository.Save(defaultDisqusBlock, SaveAction.Publish, AccessLevel.NoAccess);
        }

        /// <summary>
        /// Executes before Disqus comments add-on is uninstalled to remove all related data.
        /// </summary>
        public override void BeforeUninstall()
        {
            var blockType = ContentTypeRepository.Load<CommentsBlock>();

            ContentModelUsage.ListContentOfContentType(blockType)
                .Select(usage => usage.ContentLink).Distinct().ToList()
                .ForEach(contentLink => ContentRepository.Delete(contentLink, true, AccessLevel.NoAccess));

            ContentTypeRepository.Delete(blockType);

            ConfigurationProvider.Clear();
        }

        /// <summary>
        /// Executes after add-on upgrade. Does nothing for first version.
        /// </summary>
        public override void AfterUpdate()
        {
        }
    }
}
