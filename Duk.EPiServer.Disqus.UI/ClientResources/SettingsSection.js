define([
"dojo",
"dojo/_base/declare",
"dojo/_base/array",
"dojo/dom-construct",
"dojo/dom-style",

"dijit",
"dijit/layout/_LayoutWidget",
"dijit/_WidgetBase",
"dijit/_WidgetsInTemplateMixin",
"dijit/_TemplatedMixin",
"dijit/_Container",
"dijit/form/Button",

"dojox/widget/Standby",

"./_OperationNotifier",
"./NotificationList",

"dojo/text!./templates/SettingsSection.htm"

], function (dojo, declare, array, domConstruct, domStyle, dijit,
    _LayoutWidget, _WidgetBase, _WidgetsInTemplateMixin, _TemplatedMixin, _Container, Button,
    Standby,
    _OperationNotifier, NotificationList,
    template) {

    return declare([_WidgetBase, _Container, _TemplatedMixin, _WidgetsInTemplateMixin], {
        // summary:
        //    Settings section widget
        //
        // description:
        //    Displays the Disqus configuration section.
        //
        // tags:
        //    public


        templateString: template,
        
        model: null,

        settingsWidget: null,

        _setTitleAttr: { node: "_titleNode", type: "innerHTML" },

        _setDescriptionAttr: { node: "_descriptionNode", type: "innerHTML" },

        startup: function () {
            this.inherited(arguments);
            if (this.settingsWidget) {
                if (this.settingsWidget.isInstanceOf(_OperationNotifier)) {
                    this._standbyIndicator.target = this.settingsWidget.domNode;
                    this._subscribeToNotifications();
                }
                this.settingsWidget.startup();
            }
        },

        _subscribeToNotifications: function () {
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