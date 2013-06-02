define([
"dojo/_base/declare",
"dojo/string",

"dijit/_WidgetBase",
"dijit/_Container",

"./SettingsSection",
"./GeneralSettings",
"./AdminLinks",
"./GeneralSettingsViewModel",
"./AreaSettings",
"./AreaSettingsViewModel",
       
"dojo/i18n!./nls/Settings"
],

function (declare, string, _WidgetBase, _Container,
    SettingsSection, GeneralSettings, AdminLinks, GeneralSettingsViewModel, AreaSettings, AreaSettingsViewModel, i18n) {

    return declare([_WidgetBase, _Container], {
        //  summary:
        //      Root container for Disqus UI component.
        //  description:
        //      Creates and initializes settings widgets.
       
        buildRendering: function () {
            // summary:
            //      Creates all settings widgets
            // tags:
            //      public
            this.inherited(arguments);
            
            // TODO: Own and destroy created widgets and models to avoid memory leaks. Use dijit/Destroyable starting from Dojo 1.8.
            
            var generalSettingsModel = new GeneralSettingsViewModel();

            // General settings widgets
            var generalSection = new SettingsSection({
                settingsWidget: new GeneralSettings({ model: generalSettingsModel }),
                title: i18n.generalSettingsTitle,
                description: "<span>" +
                    string.substitute(i18n.generalSettingsDescription, {
                        disqusLink: "<a class='epi-visibleLink' href='http://disqus.com/profile/signup/' target='DisqusAdmin'>disqus.com</a>"
                    }) +
                    "</span>"
            });
            this.addChild(generalSection);
            
            // Admin links widgets
            var adminSection = new SettingsSection({
                settingsWidget: new AdminLinks({ model: generalSettingsModel }),
                title: i18n.adminTitle,
                description: "<span>" + i18n.adminDescription + "</span>"
            });
            this.addChild(adminSection);
          
            // Rendering areas configuration widgets
            var areaSettingsSection = new SettingsSection({
                settingsWidget: new AreaSettings({ model: new AreaSettingsViewModel() }),
                title: i18n.areaSettingsTitle,
                description: "<span>" + 
                    string.substitute(i18n.areaSettingsDescription, {
                        sdkLink: "<a class='epi-visibleLink' href='http://world.episerver.com/Documentation/Items/Developers-Guide/EPiServer-CMS/7/Client-Resources/Client-Resource-Management/' target='_blank'>EPiServer CMS SDK</a>"
                    }) + 
                    "</span>"
            });
            this.addChild(areaSettingsSection);
        }
    });

});