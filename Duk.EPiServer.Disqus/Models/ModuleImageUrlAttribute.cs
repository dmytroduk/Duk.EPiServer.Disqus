using System.Web;
using EPiServer.DataAnnotations;
using EPiServer.Packaging.Configuration;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Duk.EPiServer.Disqus.Models
{
    /// <summary>
    /// Custom attribute to resolve path to the icon image in module or add-on folder
    /// </summary>
    public class ModuleImageUrlAttribute : ImageUrlAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleImageUrlAttribute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module or add-on.</param>
        /// <param name="pathInModuleForlder">The partial path in module forlder.</param>
        /// <param name="isProtectedModule">if set to <c>true</c> the protected modules virtual path is going to be used as the base path.</param>
        public ModuleImageUrlAttribute(string moduleName, string pathInModuleForlder, bool isProtectedModule)
            : base(ResolvePath(moduleName, pathInModuleForlder, isProtectedModule))
        {
        }

        /// <summary>
        /// Resolves the absolute virtual path inside module folder.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="pathInModuleFolder">The path in module folder.</param>
        /// <param name="isProtectedModule">if set to <c>true</c> [is protected module].</param>
        /// <returns></returns>
        private static string ResolvePath(string moduleName, string pathInModuleFolder, bool isProtectedModule)
        {
            try
            {
                // Get virtual path to public or protected add-ons from packaging configuration
                var packagingConfiguration = ServiceLocator.Current.GetInstance<IPackagingConfiguration>();
                var modulesPath = isProtectedModule
                                      ? packagingConfiguration.ProtectedVirtualPath
                                      : packagingConfiguration.PublicVirtualPath;
                return
                    VirtualPathUtilityEx.Combine(
                        VirtualPathUtility.AppendTrailingSlash(
                            VirtualPathUtilityEx.Combine(VirtualPathUtilityEx.ToAbsolute(modulesPath), moduleName)),
                        pathInModuleFolder);
            }
            catch (ActivationException)
            {
                return "";
            }
        }
    }
}
