using System.Globalization;
using System.Text;
using Duk.EPiServer.Disqus.Models.Configuration;
using Duk.EPiServer.Disqus.Models.Context;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Generates the Disqus comments thread code and loader script that should be rendered on a page.
    /// </summary>
    public class CodeBuilder : ICodeBuilder
    {
        private const string ShortnameParameterTemplate = "var disqus_shortname = '{0}';";
        private const string DeveloperModeParameterTemplate = "var disqus_developer = {0};";
        private const string IdentifierParameterTemplate = "var disqus_identifier = '{0}';";
        private const string UrlParameterTemplate = "var disqus_url = '{0}';";
        private const string TitleParameterTemplate = "var disqus_title = '{0}';";
        private const string CategoryIdParameterTemplate = "var disqus_category_id = '{0}';";

        const string LoaderScriptTemplate = @"${Parameters}
            (function() {
                var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
                (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
            })();";

        private const string ThreadTemplate = "<div id=\"disqus_thread\"></div>";

        private const string NoScriptTemplate = "<noscript>Please enable JavaScript to view the <a href=\"http://disqus.com/?ref_noscript\">comments powered by Disqus.</a></noscript>";

        /// <summary>
        /// Creates the Disqus loader script.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The current context.</param>
        /// <returns></returns>
        public string CreateLoaderScript(IConfiguration configuration, IContext context)
        {
            var parameters = new StringBuilder();
            AddParameter(configuration.ShortName, ShortnameParameterTemplate, ref parameters);
            AddParameter(configuration.DeveloperMode ? "1" : "0", DeveloperModeParameterTemplate, ref parameters);
            AddParameter(context.Identifier, IdentifierParameterTemplate, ref parameters);
            AddParameter(context.Url, UrlParameterTemplate, ref parameters);
            AddParameter(context.Title, TitleParameterTemplate, ref parameters);
            AddParameter(context.CategoryId, CategoryIdParameterTemplate, ref parameters);
            return LoaderScriptTemplate.Replace("${Parameters}", parameters.ToString());            
        }

        /// <summary>
        /// Creates the Disqus comments thread code.
        /// </summary>
        /// <param name="configuration">The Disqus configuration.</param>
        /// <param name="context">The current context.</param>
        /// <returns></returns>
        public string CreateThreadCode(IConfiguration configuration, IContext context)
        {
            var code = new StringBuilder(ThreadTemplate);
            code.Append(NoScriptTemplate);
            return code.ToString();
        }

        private static void AddParameter(object parameter, string parameterTemplate, ref StringBuilder builder)
        {
            if (parameter != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, parameterTemplate, parameter);
            }
        }
    }
}
