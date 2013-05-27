define([
	"dojo/_base/declare",
	"dojo/_base/lang",
    "dojo/_base/Deferred",
	"dojo/Stateful",

	"epi/shell/_StatefulGetterSetterMixin",
    "epi/dependency"
],
function (
	declare, lang, Deferred, Stateful,
	_StatefulGetterSetterMixin, dependency ) {

    return declare([Stateful, _StatefulGetterSetterMixin], {
        // summary:
        // 		The view model for Disqus general settings widget

        // shortName: String
        //		The short name (Disqus ID)
        shortName: null,

        // developerMode: Boolean
        //		True when developer mode is on
        developerMode: false,

        // showPoweredByNotice: Boolean
        //		True when 'Powered by Disqus' notice should be displayed
        showPoweredByNotice: false,
        
        // adminLinks: Object
        //      Links to Disqus Admin UI
        adminLinks: null,

        configurationStore: null,
        
        _currentLoadDeferred: null,
        
        _currentSaveDeferred: null,

        constructor: function (params) {
            // Add params
            lang.mixin(this, params);
        },

        postscript: function () {
            this.inherited(arguments);

            var storeRegistry = dependency.resolve("epi.storeregistry");
            this.configurationStore = this.configurationStore || storeRegistry.get("duk-disqus.configuration");
        },

        load: function () {
            if (this._currentLoadDeferred) {
                return this._currentLoadDeferred.promise;
            }
            this._currentLoadDeferred = new Deferred();
            this.configurationStore.get().then(lang.hitch(this, function (configuration) {
                if (configuration) {
                    this.set("shortName", configuration.shortName);
                    this.set("developerMode", configuration.developerMode);
                    this.set("showPoweredByNotice", configuration.showPoweredByNotice);
                }
                this.set("adminLinks", this._createAdminLinks());
                this._currentLoadDeferred.resolve();
                this._currentLoadDeferred = null;
            }), lang.hitch(this, function (error) {
                this._currentLoadDeferred.reject(error);
                this._currentLoadDeferred = null;
            }));
            return this._currentLoadDeferred.promise;
        },

        save: function () {
            if (this._currentSaveDeferred) {
                return _currentSaveDeferred.promise;
            }
            this._currentSaveDeferred = new Deferred();
            this.configurationStore.put({
                shortName: this.shortName,
                developerMode: this.developerMode,
                showPoweredByNotice: this.showPoweredByNotice
            }).then(lang.hitch(this, function() {
                this.set("adminLinks", this._createAdminLinks());
                this._currentSaveDeferred.resolve();
                this._currentSaveDeferred = null;
            }), lang.hitch(this, function (error) {
                this._currentSaveDeferred.reject(error);
                this._currentSaveDeferred = null;
            }));
            return this._currentSaveDeferred.promise;
        },
      
        _createAdminLinks: function () {
            var links = {
                moderate: "#",
                settings: "#",
                analytics: "#",
                discussions: "#"
            };
            if (this.shortName) {
                links.moderate = "http://" + this.shortName + ".disqus.com/admin/moderate/";
                links.settings = "http://" + this.shortName + ".disqus.com/admin/settings/";
                links.analytics = "http://" + this.shortName + ".disqus.com/admin/analytics/";
                links.discussions = "http://" + this.shortName + ".disqus.com/admin/discussions/";
            }
            return links;
        }
    });
});