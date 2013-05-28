define([
"dojo/_base/declare",
"dojo/_base/array",
"dojo/dom-construct",
"dojo/dom-style",

"dijit/_WidgetBase"],

function (declare, array, domConstruct, domStyle,
    _WidgetBase) {

    return declare("duk-disqus.NotificationList", [_WidgetBase], {
        // summary:
        //    Notification list
        //
        // description:
        //    Displays the list of notification messages.
        //
        // tags:
        //    public
       
        // messageList: Array
        //      The list of message to display
        messageList: null,
        
        _messageListNode: null,
        
        buildRendering: function () {
            // summary:
            //    Creates root DOM nodes for rendering.
            //
            // tags:
            //    public    
            this.domNode = domConstruct.create("div");
            this._messageListNode = domConstruct.create("ul");
            domConstruct.place(this._messageListNode, this.domNode);
        },
        
        startup: function () {
            // summary:
            //    Starts the widget.
            //
            // tags:
            //    public            
            this.inherited(arguments);
            if (!this.messageList) {
                this.messageList = [];
            }
            this.render();
        },

        render: function () {
            // summary:
            //    Renders notification messages.
            //
            // tags:
            //    public
            domConstruct.empty(this._messageListNode);

            if (this.messageList.length > 0) {
                array.forEach(this.messageList, function (message) {
                    var li = domConstruct.create('li', {
                        innerHTML: message
                    });
                    domConstruct.place(li, this._messageListNode);
                }, this);
                domStyle.set(this.domNode, "display", "block");
            }
            else {
                domStyle.set(this.domNode, "display", "none");
            }
        },

        add: function (messages) {
            // summary:
            //    Adds notification messages.
            //
            // tags:
            //    public
            if (messages) {
                if (!(messages instanceof Array)) {
                    messages = [messages];
                }
                array.forEach(messages, function (message) {
                    this.messageList.push(message);
                }, this);
                this.render();                
            }
        },

        clear: function () {
            // summary:
            //    Clears all notification messages.
            //
            // tags:
            //    public
            this.messageList = [];
            this.render();
        }
    });
});