define([
"dojo/_base/declare",

"dijit/_WidgetBase",
"dijit/_Container",

"./GeneralSettingsSection",
"./AdminSection",
"./AreaSettingsSection",
"./GeneralSettingsViewModel"
],

function (declare, _WidgetBase, _Container,
    GeneralSettingsSection, AdminSection, AreaSettingsSection, GeneralSettingsViewModel ) {

    return declare([_WidgetBase, _Container], {
        //  summary:
        //      Root container for Disqus UI component.
        //  description:
        //      Creates and initializes navigation main widgets.

        startup: function () {
            // tags: Creates settings widgets
            //      public

            if (this._started) {
                return;
            }
            
            this.inherited(arguments);
            
            var generalSettingsModel = new GeneralSettingsViewModel();

            var generalSection = new GeneralSettingsSection({ model: generalSettingsModel });
            this.addChild(generalSection);
            
            var adminSection = new AdminSection({ model: generalSettingsModel });
            this.addChild(adminSection);
          
            var areaSettingsSection = new AreaSettingsSection();
            this.addChild(areaSettingsSection);
        }
    });

});