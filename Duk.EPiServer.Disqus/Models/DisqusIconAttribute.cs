using System;
using System.Globalization;
using System.IO;
using EPiServer.DataAnnotations;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Custom attribute to resolve path to the icon image in module or add-on folder
    /// </summary>
    public class DisqusIconAttribute : ImageUrlAttribute
    {
        private const string IconResourceName = "Duk.EPiServer.Disqus.ClientResources.DisqusBlockIcon.png";
        private const string IconType = "image/png";

        private static readonly Lazy<string> IconDataUrl = new Lazy<string>(CreateDataUrl);

        /// <summary>
        /// Initializes a new instance of the <see cref="DisqusIconAttribute"/> class.
        /// </summary>
        public DisqusIconAttribute()
            : base(IconDataUrl.Value)
        {

        }

        /// <summary>
        /// Creates the data URL using embedded image.
        /// </summary>
        /// <returns></returns>
        private static string CreateDataUrl()
        {
            byte[] iconData;
            using (var iconStream = typeof(DisqusIconAttribute).Assembly.GetManifestResourceStream(IconResourceName))
            {
                if (iconStream == null)
                {
                    return string.Empty;
                }
                using (var memoryStream = new MemoryStream())
                {
                    iconStream.CopyTo(memoryStream);
                    iconData = memoryStream.ToArray();
                }
            }
            if (iconData.Length == 0)
            {
                return string.Empty;
            }
            return string.Format(CultureInfo.InvariantCulture, 
                "data:{0};base64,{1}", IconType, Convert.ToBase64String(iconData));
        }
    }
}
