define([
// Dojo
    "dojo",
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/_base/Deferred",
    "dojo/dom-class",


// Dijit
    "dijit/_TemplatedMixin",
    "dijit/_WidgetBase",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/form/ValidationTextBox",

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
    dojo, declare, lang, Deferred, domClass, 

// Dijit
    _TemplatedMixin, _WidgetBase, _WidgetsInTemplateMixin, ValidationTextBox,

// Dojox
    validate,

// EPi
    _ModelBindingMixin,

// Disqus
    _OperationNotifier,

// Resources
    template, i18n
) {

    // module:
    //		
    // summary:
    //		

    return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, _ModelBindingMixin, _OperationNotifier], {

        i18n: i18n,
        templateString: template,

        modelBindingMap: {
            "shortName": ["shortName"],
            "developerMode": ["developerMode"],
            "showPoweredByNotice": ["showPoweredByNotice"]
        },

        _setShortNameAttr: function (shortName) {
            this._shortNameControl.set("value", shortName, false);
        },

        _setDeveloperModeAttr: function (developerMode) {
            this._developerModeControl.set("checked", developerMode, false);
        },

        _setShowPoweredByNoticeAttr: function (showPoweredByNotice) {
            this._showPoweredByNoticeControl.set("checked", showPoweredByNotice, false);
        },

        startup: function () {
            this.inherited(arguments);

            this.onOperationStarted();
            this.model.load().then(lang.hitch(this, function () {
                    this.connect(this._shortNameControl, "onChange",
                        function (value) { this._indicateUnsaved(value, this.model.shortName); });
                    this.connect(this._developerModeControl, "onChange",
                        function (value) { this._indicateUnsaved(value, this.model.developerMode); });
                    this.connect(this._showPoweredByNoticeControl, "onChange",
                        function (value) { this._indicateUnsaved(value, this.model.showPoweredByNotice); });
                    this._shortNameControl.intermediateChanges = true;
                    this.onOperationCompleted();
                }), lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        },

        _saveClick: function () {
            if (!this._validateSettings()) {
                return;
            }
            this.model.set("shortName", this._shortNameControl.value);
            this.model.set("developerMode", this._developerModeControl.checked);
            this.model.set("showPoweredByNotice", this._showPoweredByNoticeControl.checked);
            this.onOperationStarted();
            this.model.save().then(lang.hitch(this, function () {
                    this._indicateSaved();
                    this.onOperationCompleted();
                }), lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        },

        _indicateUnsaved: function (newValue, oldValue) {
            if (!this._validateSettings()) {
                return;
            }
            if (newValue !== oldValue) {
                domClass.add(this._saveButton.domNode, "Sweet");
            }
        },

        _indicateSaved: function () {
            domClass.remove(this._saveButton.domNode, "Sweet");
        },
        
        _validateSettings: function () {
            return this._shortNameControl.isValid();
        }
    });
});
