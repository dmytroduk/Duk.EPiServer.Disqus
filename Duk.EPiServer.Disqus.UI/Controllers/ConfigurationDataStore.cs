using System.Collections.Generic;
using System.Web.Mvc;
using Duk.EPiServer.Disqus.Models.Configuration;
using Duk.EPiServer.Disqus.UI.Models;
using EPiServer.Framework;
using EPiServer.Framework.Serialization;
using EPiServer.Shell.Services.Rest;

namespace Duk.EPiServer.Disqus.UI.Controllers
{
    /// <summary>
    /// General configuration data store
    /// </summary>
    [RestStore("disqusconfiguration")]
    public class ConfigurationDataStore : RestControllerBase
    {
        private readonly IConfigurationProvider _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDataStore"/> class.
        /// </summary>
        /// <param name="configurationProvider">The configuration provider.</param>
        public ConfigurationDataStore(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public RestResult Get(string id)
        {
            var configuration = _configurationProvider.Load();
            return Rest(CreateModel(configuration));
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="configurationModel">The configuration model.</param>
        /// <returns></returns>
        [HttpPost]
        public RestResult Post(ConfigurationModel configurationModel)
        {
            Validator.ThrowIfNullOrEmpty("shortName", configurationModel.ShortName);
            
            var configuration = _configurationProvider.Load();
            
            configuration.ShortName = configurationModel.ShortName;
            configuration.Enabled = configurationModel.Enabled;
            
            _configurationProvider.Save(configuration);
           
            return Rest(configurationModel);
        }

        private static ConfigurationModel CreateModel(IConfiguration configuration)
        {
            return new ConfigurationModel
                       {
                           ShortName = configuration.ShortName,
                           Enabled = configuration.Enabled
                       };
        }
    }
}