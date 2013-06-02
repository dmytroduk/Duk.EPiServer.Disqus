define([
// Dojo
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/dom-class",
   
// Dijit
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/form/Textarea",

// EPi
    "epi/shell/widget/_ModelBindingMixin",

// Disqus
    "./_OperationNotifier",

// Resources
    "dojo/text!./templates/AreaSettings.htm",
    "dojo/i18n!./nls/Settings"

], function (
// Dojo
    declare, lang, domClass,
    
// Dijit
    _WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, Textarea,
    
// EPi
    _ModelBindingMixin, 

// Disqus
    _OperationNotifier,

// Resources
    template, i18n) {
    
    return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, _ModelBindingMixin, _OperationNotifier], {
        // summary:
        //    Rendering areas configuration widget
        //
        // description:
        //    Widget to edit the list of rendering areas where Disqus comments should be rendered.
        //
        // tags:
        //    public

        // i18n: [public] Object
        //      Localized texts
        i18n: i18n,

        // templateString: [public] String
        //      Widget template
        templateString: template,
        
        modelBindingMap: {
            "renderingAreas": ["renderingAreas"]
        },

        _setRenderingAreasAttr: function (areas) {
            this._areaList.set("value", areas);
        }, 
       
        startup: function () {
            // summary:
            //      Loads settings and initializes edit controls
            // tags:
            //      public
            if (this._started) {
                return;
            }
            this.inherited(arguments);
           
            this.onOperationStarted();
            this.model.load().then(lang.hitch(this, function() {
                this.connect(this._areaList, "onChange",
                    function(value) { this._indicateUnsaved(value, this.model.renderingAreas); });
                this._areaList.intermediateChanges = true;
                this.onOperationCompleted();
            }), lang.hitch(this, function(errors) {
                this.onOperationFailed(errors);
            }));

        },
        
        _saveClick: function () {
            // summary:
            //      Prepares data, sets model values and saves settings
            // tags:
            //      private
            this.model.set("renderingAreas", this._areaList.value);
            this.onOperationStarted();
            this.model.save().then(lang.hitch(this, function() {
                this._indicateSaved();
                this.onOperationCompleted();
            }), lang.hitch(this, function(errors) {
                this.onOperationFailed(errors);
            }));
        },
        
        _indicateUnsaved: function (newValue, oldValue) {
            // summary:
            //      Indicates whether settings were updated and can be saved
            // tags:
            //      private
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
        }
    });
});
