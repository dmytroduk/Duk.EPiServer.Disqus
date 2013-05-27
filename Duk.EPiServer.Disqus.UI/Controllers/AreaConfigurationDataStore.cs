using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Duk.EPiServer.Disqus.Models.Configuration;
using EPiServer.Framework.Serialization;
using EPiServer.Shell.Services.Rest;

namespace Duk.EPiServer.Disqus.UI.Controllers
{
    /// <summary>
    /// Disqus rendenring areas data store
    /// </summary>
    [RestStore("disqusareaconfiguration")]
    public class AreaConfigurationDataStore : RestControllerBase
    {
        private readonly IConfigurationProvider _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaConfigurationDataStore"/> class.
        /// </summary>
        /// <param name="configurationProvider">The configuration provider.</param>
        /// <param name="valueProviders">The value providers.</param>
        /// <param name="serializerFactory">The serializer factory.</param>
        public AreaConfigurationDataStore(IConfigurationProvider configurationProvider,
            IEnumerable<IRestControllerValueProvider> valueProviders, IObjectSerializerFactory serializerFactory)
            : base(valueProviders, serializerFactory)
        {
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Gets the configured Disqus rendering areas.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public RestResult Get(string id)
        {
            var configuration = _configurationProvider.Load();
            return Rest(configuration.RenderingAreas);
        }

        /// <summary>
        /// Saves defined areas where Disqus comments should be rendered.
        /// </summary>
        /// <param name="areas">The areas.</param>
        /// <returns></returns>
        [HttpPost]
        public RestResult Post(IEnumerable<string> areas)
        {
            var configuration = _configurationProvider.Load();
            configuration.RenderingAreas.Clear();

            var areaList = areas != null ? areas.ToList() : new List<string>();
            areaList.ForEach(area =>
                                 {
                                     if (!string.IsNullOrWhiteSpace(area))
                                     {
                                         configuration.RenderingAreas.Add(area);
                                     }
                                 });
            _configurationProvider.Save(configuration);
            return Rest(areaList);
        }
    }
}