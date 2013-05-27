define([
// Dojo:
"dojo/_base/declare",
"dojo/string",

// Disqus
"./SettingsSection",
"./GeneralSettings",
"./GeneralSettingsViewModel",

"dojo/i18n!./nls/Settings"

], function (declare, string, 
    SettingsSection, GeneralSettings, GeneralSettingsViewModel, i18n) {

    return declare([SettingsSection], {
        // summary:
        //    General settings section widget
        //
        // tags:
        //    public

        postCreate: function () {
            this.inherited(arguments);

            this.model = this.model || new GeneralSettingsViewModel();

            if (!this.settingsWidget) {
                this.settingsWidget = new GeneralSettings({ model: this.model }, this._settingsContainer);
            }

            if (!this.get("title")) {
                this.set("title", i18n.generalSettingsTitle);
            }

            if (!this.get("description")) {
                this.set("description", "<span>" + 
                    string.substitute(i18n.generalSettingsDescription, {
                        disqusLink: "<a class='epi-visibleLink' href='http://disqus.com/profile/signup/' target='_blank'>disqus.com</a>"
                    }) + 
                    "</span>");
            }
        }
    });
});