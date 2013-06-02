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

        // renderingAreas: [public] Array
        //		The list of rendering areas where Disqus comments should be added on page
        renderingAreas: null,

        // dataStore: [public] Object
        //		Configuration data store
        dataStore: null,
        
        _currentLoadDeferred: null,

        _currentSaveDeferred: null,

        constructor: function (params) {
            lang.mixin(this, params);
        },

        postscript: function () {
            // summary:
            //      Setup settings store.
            // tags:
            //      public
            this.inherited(arguments);

            if (!this.dataStore) {
                var storeRegistry = dependency.resolve("epi.storeregistry");
                this.dataStore = storeRegistry.get("duk-disqus.areaconfiguration");
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
            this.dataStore.get().then(lang.hitch(this, function (areaList) {
                this.set("renderingAreas", this._toString(areaList));
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
            // summary:
            //      Converts string to array using new line as separator.
            //
            //  areas:
            //      String that should be converted to the array.
            //
            //  returns:
            //      Array
            //
            // tags:
            //      private
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
        
        _toString: function (areaList) {
            // summary:
            //      Converts array to string using new line as separator.
            //
            //  areaList:
            //      Attay that should be converted to string.
            //
            //  returns:
            //      String
            //
            // tags:
            //      private
            return (areaList && lang.isArray(areaList)) ? areaList.join("\n") : "";
        }
    });
});