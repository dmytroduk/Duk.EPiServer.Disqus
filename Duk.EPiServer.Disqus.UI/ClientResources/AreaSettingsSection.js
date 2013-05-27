define([
// Dojo:
"dojo/_base/declare",
"dojo/string",    

// Disqus
"./SettingsSection",
"./AreaSettings",
"./AreaSettingsViewModel",

"dojo/i18n!./nls/Settings"


], function (declare, string, 
    SettingsSection, AreaSettings, AreaSettingsViewModel, i18n) {

    return declare([SettingsSection], {
        // summary:
        //    Disqus rendering area settings section widget
        //
        // tags:
        //    public

        postCreate: function () {
            this.inherited(arguments);
            if (!this.settingsWidget) {
                this.settingsWidget = new AreaSettings({ model: new AreaSettingsViewModel() }, this._settingsContainer);
            }

            if (!this.get("title")) {
                this.set("title", i18n.areaSettingsTitle);
            }

            if (!this.get("description")) {
                this.set("description", "<span>" + 
                    string.substitute(i18n.areaSettingsDescription, {
                        sdkLink: "<a class='epi-visibleLink' href='http://world.episerver.com/Documentation/Items/Developers-Guide/EPiServer-CMS/7/Client-Resources/Client-Resource-Management/' target='_blank'>EPiServer CMS SDK</a>"
                    }) + 
                    "</span>");
                
            }

        }
    });
});