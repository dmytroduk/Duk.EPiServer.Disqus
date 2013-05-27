define([
"dojo/_base/declare",

"epi/_Module",
"epi/routes"
],

function (declare, _Module, routes) {

    return declare([_Module], {
        // summary:
        //		Disqus module initializer.
        //

        initialize: function () {
            // summary:
            //		Initialize module
            //
            // description:
            //      Creates and register Disqus settings data store.
            //
            this.inherited(arguments);
            
            var registry = this.resolveDependency("epi.storeregistry");

            var configurationStoreUrl = routes.getRestPath({ moduleArea: "Duk.EPiServer.Disqus.UI", storeName: "disqusconfiguration" });
            registry.create("duk-disqus.configuration", configurationStoreUrl);

            var areaConfigurationStoreUrl = routes.getRestPath({ moduleArea: "Duk.EPiServer.Disqus.UI", storeName: "disqusareaconfiguration" });
            registry.create("duk-disqus.areaconfiguration", areaConfigurationStoreUrl);
        }
    });
});
