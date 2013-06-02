using System.Collections.Generic;
using System.Linq;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Composition;

namespace Duk.EPiServer.Disqus.Models.Configuration
{
    /// <summary>
    /// Provides Disqus configuration stored in DDS
    /// </summary>
    public class DatabaseConfigurationProvider : IConfigurationProvider
    {
        private readonly DynamicDataStoreFactory _storeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfigurationProvider"/> class.
        /// </summary>
        /// <param name="storeFactory">The store factory.</param>
        public DatabaseConfigurationProvider(DynamicDataStoreFactory storeFactory)
        {
            _storeFactory = storeFactory;
        }

        /// <summary>
        /// Loads the Disqus configuration from DDS.
        /// </summary>
        /// <returns></returns>
        public IConfiguration Load()
        {
            ConfigurationEntity settingsEntity;
            using (var store = _storeFactory.GetOrCreateStore(typeof(ConfigurationEntity)))
            {
                settingsEntity = store.Items<ConfigurationEntity>().FirstOrDefault();
            }
            if (settingsEntity == null)
            {
                return CreateDefaultConfiguration();
            }
            var settings = CreateConfiguration(settingsEntity);
            // TODO: cache settings?
            return settings;
        }

        /// <summary>
        /// Saves the Disqus configuration in DDS.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        public void Save(IConfiguration configuration)
        {
            using (var store = _storeFactory.GetOrCreateStore(typeof(ConfigurationEntity)))
            {
                var existingConfiguration = store.Items<ConfigurationEntity>().FirstOrDefault();
                if (existingConfiguration != null)
                {
                    UpdateEntity(existingConfiguration, configuration);
                }
                store.Save(existingConfiguration ?? CreateEntity(configuration));
            }            
        }

        /// <summary>
        /// Clears Disqus configuration and removes all existing data in DDS.
        /// </summary>
        public void Clear()
        {
            _storeFactory.DeleteStore(typeof(ConfigurationEntity), true);
        }

        private IConfiguration CreateDefaultConfiguration()
        {
            return new Configuration { ShortName = string.Empty, DeveloperMode = true, ShowPoweredByNotice = false };
        }

        private static IConfiguration CreateConfiguration(ConfigurationEntity entity)
        {
            return new Configuration
                       {
                           DeveloperMode = entity.DeveloperMode,
                           ShortName = entity.ShortName,
                           ShowPoweredByNotice = entity.ShowPoweredByNotice,
                           RenderingAreas = entity.RenderingAreas != null ? entity.RenderingAreas.ToList() : new List<string>()
                       };
        }

        private static ConfigurationEntity CreateEntity(IConfiguration configuration)
        {
            var entiry = new ConfigurationEntity();
            UpdateEntity(entiry, configuration);
            return entiry;
        }

        private static void UpdateEntity(ConfigurationEntity entity, IConfiguration configuration)
        {
            entity.DeveloperMode = configuration.DeveloperMode;
            entity.ShortName = configuration.ShortName;
            entity.ShowPoweredByNotice = configuration.ShowPoweredByNotice;
            entity.RenderingAreas = configuration.RenderingAreas.ToList();
        }

    }
}
