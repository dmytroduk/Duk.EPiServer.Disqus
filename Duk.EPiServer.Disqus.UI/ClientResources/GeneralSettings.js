define([
// Dojo
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/dom-class",


// Dijit
    "dijit/_TemplatedMixin",
    "dijit/_WidgetBase",
    "dijit/_WidgetsInTemplateMixin",

// Dojox
    "dojox/validate/web",

// EPi
    "epi/shell/widget/_ModelBindingMixin",

// Disqus
    "./_OperationNotifier",

// Resources
    "dojo/text!./templates/GeneralSettings.htm",
    "dojo/i18n!./nls/Settings"

], function (
// Dojo
    declare, lang, domClass, 

// Dijit
    _TemplatedMixin, _WidgetBase, _WidgetsInTemplateMixin,

// Dojox
    validate,

// EPi
    _ModelBindingMixin,

// Disqus
    _OperationNotifier,

// Resources
    template, i18n) {
    return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, _ModelBindingMixin, _OperationNotifier], {
        // summary:
        //    General settings widget
        //
        // description:
        //    Enables editing of general settings required for Disqus integration, like short name and so on.
        //
        // tags:
        //    public
        
        // i18n: [public] Object
        //      Localized texts
        i18n: i18n,
        
        // templateString: [public] String
        //      Widget template
        templateString: template,

        // modelBindingMap: [public] Object
        //      Map to bind widget and model attributes
        modelBindingMap: {
            "shortName": ["shortName"],
            "enabled": ["enabled"]
        },

        _setShortNameAttr: function (shortName) {
            this._shortNameControl.set("value", shortName, false);
        },

        _setEnabledAttr: function (enabled) {
            this._enabledControl.set("checked", enabled, false);
        },

        startup: function () {
            // summary:
            //      Loads settings and initializes controls
            // tags:
            //      public
            if (this._started) {
                return;
            }
            this.inherited(arguments);

            this.onOperationStarted();
            this.model.load().then(lang.hitch(this, function () {
                    this.connect(this._shortNameControl, "onChange",
                        function (value) { this._indicateUnsaved(value, this.model.shortName); });
                    this.connect(this._enabledControl, "onChange",
                        function (value) { this._indicateUnsaved(value, this.model.enabled); });
                    this._shortNameControl.intermediateChanges = true;
                    this.onOperationCompleted();
                }), lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        },

        _saveClick: function () {
            // summary:
            //      Validates settings, sets model values and saves data
            // tags:
            //      private
            if (!this._validateSettings()) {
                return;
            }
            this.model.set("shortName", this._shortNameControl.value);
            this.model.set("enabled", this._enabledControl.checked);
            this.onOperationStarted();
            this.model.save().then(lang.hitch(this, function () {
                    this._indicateSaved();
                    this.onOperationCompleted();
                }), lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        },

        _indicateUnsaved: function (newValue, oldValue) {
            // summary:
            //      Indicates whether settings were updated and can be saved
            // tags:
            //      private            
            if (!this._validateSettings()) {
                return;
            }
            if (newValue !== oldValue) {
                domClass.add(this._saveButton.domNode, "Sweet");
            }
        },

        _indicateSaved: function () {
            // summary:
            //      Removes indication that settings were updated
            // tags:
            //      private                  
            domClass.remove(this._saveButton.domNode, "Sweet");
        },
        
        _validateSettings: function () {
            // summary:
            //      Checks whether settings are valid
            // tags:
            //      private                  
            return this._shortNameControl.isValid();
        }
    });
});
