define([
// Dojo
    "dojo/_base/declare",
    "dojo/_base/lang",

// Dijit
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

// EPi
    "epi/shell/widget/_ModelBindingMixin",

// Disqus
    "./_OperationNotifier",

// Resources
    "dojo/text!./templates/AdminLinks.htm",
    "dojo/i18n!./nls/Settings"

], function (
// Dojo
    declare, lang,
    
// Dijit
    _WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin,
    
// EPi
    _ModelBindingMixin,

// Disqus
    _OperationNotifier,

// Resources
    template, i18n) {
    return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, _ModelBindingMixin, _OperationNotifier], {
        // summary:
        //    Disqus admin links widget
        //
        // description:
        //    Provides quick links to manage comments using Disqus Admin UI.
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
        //      Map to bind widget and model link attributes
        modelBindingMap: {
            "moderateAdminUrl": ["moderateAdminUrl"],
            "settingsAdminUrl": ["settingsAdminUrl"],
            "analyticsAdminUrl": ["analyticsAdminUrl"],
            "discussionsAdminUrl": ["discussionsAdminUrl"]
        },
        
        _setModerateAdminUrlAttr: { node: "_moderateLink", attribute: "href" },
        _setSettingsAdminUrlAttr: { node: "_settingsLink", attribute: "href" },
        _setAnalyticsAdminUrlAttr: { node: "_analyticsLink", attribute: "href" },
        _setDiscussionsAdminUrlAttr: { node: "_discussionsLink", attribute: "href" },
        
        startup: function () {
            // summary:
            //      Loads settings and initializes quick links
            // tags:
            //      public
            if (this._started) {
                return;
            }
            this.inherited(arguments);

            this.onOperationStarted();
            this.model.load().then(lang.hitch(this, function () {
                    this.onOperationCompleted();
                }),
                lang.hitch(this, function (errors) {
                    this.onOperationFailed(errors);
                }));
        }
        
    });
});
