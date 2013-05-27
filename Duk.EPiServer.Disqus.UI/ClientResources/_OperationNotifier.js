define([
"dojo/_base/declare"
],

function (declare) {

    return declare(null, {
        // summary:
        //    Mixin for widget that can notify about started or completed operation and results
        //
        // tags:
        //    public

        onOperationStarted: function () {
            // summary:
            //    Triggered when some operation is started.
            //
            // tags:
            //    public callback
        },

        onOperationCompleted: function (messages) {
            // summary:
            //    Triggered when operation is successfully completed.
            //
            // tags:
            //    public callback
        },

        onOperationFailed: function (messages) {
            // summary:
            //    Triggered when operation is failed.
            //
            // tags:
            //    public callback
        },

        onOperationError: function (message) {
            // summary:
            //    Triggered when error is occured during the operation.
            //
            // tags:
            //    public callback
        },
    });
});