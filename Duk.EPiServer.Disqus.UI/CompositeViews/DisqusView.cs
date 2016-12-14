using EPiServer.Shell.ViewComposition;
using EPiServer.Shell.ViewComposition.Containers;

namespace Duk.EPiServer.Disqus.UI.CompositeViews
{
    /// <summary>
    /// Disqus bootstrapper view
    /// </summary>
    [CompositeView]
    public class DisqusView : ICompositeView
    {
        private const string ViewName = "Disqus";
        private IContainer _rootContainer;

        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return ViewName; }
        }

        /// <summary>
        /// Gets the view title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get { return ViewName; }
        }

        /// <summary>
        /// Defines a default context for the view, for instance the start page for the CMS home view.
        /// </summary>
        /// <remarks>
        /// Set to null if the view should not have a default context.
        /// </remarks>
        public string DefaultContext
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the Disqus UI root container.
        /// </summary>
        /// <value>
        /// The root container.
        /// </value>
        public IContainer RootContainer
        {
            get
            {
                if (_rootContainer == null)
                {
                    var customContainer = new CustomContainer("duk-disqus/RootContainer");
                    customContainer.Settings.Add("id", Name + "_rootContainer");
                    customContainer.Settings.Add("persist", "true"); //Persist window size on client
                    _rootContainer = customContainer;
                }
                return _rootContainer;
            }
        }

        /// <summary>
        /// Creates the Disqus view.
        /// </summary>
        /// <returns></returns>
        public ICompositeView CreateView()
        {
            return new DisqusView();
        }
    }
}
