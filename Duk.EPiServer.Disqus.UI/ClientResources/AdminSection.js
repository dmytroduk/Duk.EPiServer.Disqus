define([
// Dojo:
"dojo/_base/declare",
"dojo/string",

// Disqus
"./SettingsSection",
"./Admin",
"./GeneralSettingsViewModel",

"dojo/i18n!./nls/Settings"

], function (declare, string, 
    SettingsSection, Admin, GeneralSettingsViewModel, i18n) {

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
                this.settingsWidget = new Admin({ model: this.model }, this._settingsContainer);
            }

            if (!this.get("title")) {
                this.set("title", i18n.adminTitle);
            }

            if (!this.get("description")) {
                this.set("description", "<span>" + i18n.adminDescription + "</span>");
            }
        }
    });
});