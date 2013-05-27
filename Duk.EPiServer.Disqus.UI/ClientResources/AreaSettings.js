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
    dojo, declare, lang, Deferred, domClass,
    
// Dijit
    _TemplatedMixin, _WidgetBase, _WidgetsInTemplateMixin, Textarea,
    
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

        templateString: template,
        i18n: i18n,
        
        modelBindingMap: {
            "renderingAreas": ["renderingAreas"]
        },

        _setRenderingAreasAttr: function (areas) {
            this._areaList.set("value", areas);
        }, 
       
        startup: function () {
            this.inherited(arguments);
            
            this.onOperationStarted();
            Deferred.when(this.model.load(), dojo.hitch(this, function () {
                this.connect(this._areaList, "onChange",
                    function (value) { this._indicateUnsaved(value, this.model.renderingAreas); });
                this._areaList.intermediateChanges = true;
                this.onOperationCompleted();
                }),
            dojo.hitch(this, function (errors) {
                this.onOperationFailed(errors);
            }));

        },
        
        _saveClick: function () {
            this.model.set("renderingAreas", this._areaList.value);
            this.onOperationStarted();
            Deferred.when(this.model.save(),
                dojo.hitch(this, function () {
                    this._indicateSaved();
                    this.onOperationCompleted();
                }),
                dojo.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));

        },
        
        _indicateUnsaved: function (newValue, oldValue) {
            if (newValue !== oldValue) {
                domClass.add(this._saveButton.domNode, "Sweet");
            }
        },

        _indicateSaved: function () {
            domClass.remove(this._saveButton.domNode, "Sweet");
        }
    });
});
