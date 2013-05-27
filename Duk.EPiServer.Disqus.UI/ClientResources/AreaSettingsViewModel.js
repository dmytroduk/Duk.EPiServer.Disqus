define([
	"dojo/_base/declare",
	"dojo/_base/lang",
    "dojo/_base/array",
    "dojo/_base/Deferred",
	"dojo/Stateful",

	"epi/shell/_StatefulGetterSetterMixin",
    "epi/dependency"
],
function (
	declare, lang, array, Deferred, Stateful,
	_StatefulGetterSetterMixin, dependency
) {

    return declare([Stateful, _StatefulGetterSetterMixin], {
        // summary:
        // 		The view model for Disqus rendering area settings widget

        renderingAreas: null,

        dataStore: null,
        
        _currentLoadDeferred: null,

        _currentSaveDeferred: null,

        constructor: function (params) {
            // Add params
            lang.mixin(this, params);
        },

        postscript: function () {
            this.inherited(arguments);

            var storeRegistry = dependency.resolve("epi.storeregistry");
            this.dataStore = this.dataStore || storeRegistry.get("duk-disqus.areaconfiguration");
        },

        load: function () {
            if (this._currentLoadDeferred) {
                return this._currentLoadDeferred.promise;
            }
            this._currentLoadDeferred = new Deferred();
            this.dataStore.get().then(lang.hitch(this, function (areaList) {
                if (areaList) {
                    this.set("renderingAreas", this._toString(areaList));
                }
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
            var areaList = this._toArray(this.renderingAreas);
            this.set("renderingAreas", this._toString(areaList));
            this.dataStore.put(areaList).then(lang.hitch(this, function () {
                this._currentSaveDeferred.resolve();
                this._currentSaveDeferred = null;
            }), lang.hitch(this, function (error) {
                this._currentSaveDeferred.reject(error);
                this._currentSaveDeferred = null;
            }));
            return this._currentSaveDeferred.promise;
        },

        _toArray: function (areas) {
            if (lang.isArray(areas)) {
                return areas;
            } else {
                if (areas) {
                    var areasList = areas.split(/\n/g);
                    areasList = array.map(areasList, lang.trim);
                    areasList = array.filter(areasList, function (area) {
                        return area && area.length > 0;
                    });
                    return areasList;
                } else {
                    return [];
                }
            }
        },
        
        _toString: function(areaList) {
            return lang.isArray(areaList) ? areaList.join("\n") : null;
        }
    });
});