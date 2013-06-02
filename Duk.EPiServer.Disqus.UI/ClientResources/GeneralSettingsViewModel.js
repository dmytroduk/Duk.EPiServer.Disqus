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
        // 		The view model for General settings and Disqus Admin widgets

        // shortName: [public] String
        //		The short name (Disqus ID)
        shortName: null,

        // developerMode: [public] Boolean
        //		True when developer mode is on
        developerMode: false,

        // showPoweredByNotice: [public] Boolean
        //		True when 'Powered by Disqus' notice should be displayed
        showPoweredByNotice: false,
      
        // moderateAdminUrl: [public] String
        //      The URL of the comments moderation section in the Disqus Admin UI
        moderateAdminUrl: null,

        // settingsAdminUrl: [public] String
        //      The URL of the settings section in the Disqus Admin UI
        settingsAdminUrl: null,

        // analyticsAdminUrl: [public] String
        //      The URL of the analytics section in the Disqus Admin UI
        analyticsAdminUrl: null,
        
        // discussionsAdminUrl: [public] String
        //      The URL of the migrate, import and export tools section in the Disqus Admin UI
        discussionsAdminUrl: null,

        // configurationStore: [public] Object
        //      Configuration data store
        configurationStore: null,
        
        _currentLoadDeferred: null,
        
        _currentSaveDeferred: null,

        constructor: function (params) {
            // Add params
            lang.mixin(this, params);
        },

        postscript: function () {
            // summary:
            //      Setup settings store.
            // tags:
            //      public
            this.inherited(arguments);

            if (!this.configurationStore) {
                var storeRegistry = dependency.resolve("epi.storeregistry");
                this.configurationStore = storeRegistry.get("duk-disqus.configuration");
            }
        },

        load: function () {
            // summary:
            //      Loads settings from the store and initializes model attributes.
            //
            //  returns:
            //      Promise indicating when load operation is completed
            //
            // tags:
            //      public
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
                this._createAdminLinks();
                this._currentLoadDeferred.resolve();
                this._currentLoadDeferred = null;
            }), lang.hitch(this, function (error) {
                this._currentLoadDeferred.reject(error);
                this._currentLoadDeferred = null;
            }));
            return this._currentLoadDeferred.promise;
        },

        save: function () {
            // summary:
            //      Saves current settings data to the store.
            //
            //  returns:
            //      Promise indicating when save operation is completed
            //
            // tags:
            //      public            
            if (this._currentSaveDeferred) {
                return _currentSaveDeferred.promise;
            }
            this._currentSaveDeferred = new Deferred();
            this.configurationStore.put({
                shortName: this.shortName,
                developerMode: this.developerMode,
                showPoweredByNotice: this.showPoweredByNotice
            }).then(lang.hitch(this, function() {
                this._createAdminLinks();
                this._currentSaveDeferred.resolve();
                this._currentSaveDeferred = null;
            }), lang.hitch(this, function (error) {
                this._currentSaveDeferred.reject(error);
                this._currentSaveDeferred = null;
            }));
            return this._currentSaveDeferred.promise;
        },
      
        _createAdminLinks: function () {
            // summary:
            //      Constructs admin links using current settings.
            // tags:
            //      public            
            if (this.shortName) {
                this.set("moderateAdminUrl", "http://" + this.shortName + ".disqus.com/admin/moderate/");
                this.set("settingsAdminUrl", "http://" + this.shortName + ".disqus.com/admin/settings/");
                this.set("analyticsAdminUrl", "http://" + this.shortName + ".disqus.com/admin/analytics/");
                this.set("discussionsAdminUrl", "http://" + this.shortName + ".disqus.com/admin/discussions/");
            } else {
                this.set("moderateAdminUrl", "#");
                this.set("settingsAdminUrl", "#");
                this.set("analyticsAdminUrl", "#");
                this.set("discussionsAdminUrl", "#");
            }
        }
    });
});