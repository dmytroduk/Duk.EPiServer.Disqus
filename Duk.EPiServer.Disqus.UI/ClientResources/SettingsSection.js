define([
"dojo/_base/declare",

"dijit/_WidgetBase",
"dijit/_WidgetsInTemplateMixin",
"dijit/_TemplatedMixin",
"dijit/_Container",

"dojox/widget/Standby",

"./_OperationNotifier",
"./NotificationList",

"dojo/text!./templates/SettingsSection.htm"

], function (declare, _WidgetBase, _WidgetsInTemplateMixin, _TemplatedMixin, _Container, Standby,
    _OperationNotifier, NotificationList,
    template) {

    return declare([_WidgetBase, _Container, _TemplatedMixin, _WidgetsInTemplateMixin], {
        // summary:
        //    Settings section widget
        //
        // description:
        //    Displays the configuration section and corresponding settings widget.
        //
        // tags:
        //    public

        // templateString: [public] String
        //      Widget template
        templateString: template,
        
        // settingsWidget: [public] Object
        //      Settings widget that should be displayed in this section
        settingsWidget: null,

        _setTitleAttr: { node: "_titleNode", type: "innerHTML" },

        _setDescriptionAttr: { node: "_descriptionNode", type: "innerHTML" },

        startup: function () {
            // summary:
            //    Places settings widget and subscribes to operation notifications.
            //
            // tags:
            //    public
            if (this._started) {
                return;
            }
            this.inherited(arguments);
            
            if (this.settingsWidget) {
                this.settingsWidget.placeAt(this._settingsContainer);
                if (this.settingsWidget.isInstanceOf(_OperationNotifier)) {
                    this._standbyIndicator.target = this.settingsWidget.domNode;
                    this._subscribeToNotifications();
                }
                this.settingsWidget.startup();
            }
        },
     
        _subscribeToNotifications: function () {
            // summary:
            //    Subscribes to operation notifications of settings widget.
            //
            // tags:
            //    private
            
            this.connect(this.settingsWidget, "onOperationStarted", function () {
                this._standbyIndicator.show();
            });
            this.connect(this.settingsWidget, "onOperationCompleted", function (messages) {
                this._errorList.clear();
                this._messageList.clear();
                this._messageList.add(messages);
                this._standbyIndicator.hide();
            });
            this.connect(this.settingsWidget, "onOperationFailed", function (messages) {
                this._messageList.clear();
                this._errorList.clear();
                this._errorList.add(messages);
                this._standbyIndicator.hide();
            });
            this.connect(this.settingsWidget, "onOperationError", function (message) {
                this._errorList.add(message);
            });
        }
    });
});