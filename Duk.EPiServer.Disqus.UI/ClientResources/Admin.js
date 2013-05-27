define([
// Dojo
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/_base/Deferred",

// Dijit
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

// EPi
    "epi/shell/widget/_ModelBindingMixin",

// Disqus
    "./_OperationNotifier",

// Resources
    "dojo/text!./templates/Admin.htm",
    "dojo/i18n!./nls/Settings"

], function (
// Dojo
    declare, lang, Deferred,
    
// Dijit
    _WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin,
    
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
            "adminLinks": ["adminLinks"]
        },

        _setAdminLinksAttr: function (adminLinks) {
            if (adminLinks) {
                this._moderateLink.href = adminLinks.moderate;
                this._settingsLink.href = adminLinks.settings;
                this._analyticsLink.href = adminLinks.analytics;
                this._discussionsLink.href = adminLinks.discussions;

                // TODO: remove debug output
                console.debug(adminLinks);
            }
        },

        startup: function () {
            this.inherited(arguments);

            this.onOperationStarted();
            Deferred.when(this.model.load(), lang.hitch(this, function () {
                    this.onOperationCompleted();
                }),
                lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        }
        
    });
});
