/*** 
* Used for defining the MyWebApp UI
* @module UI
* @namespace MyWebApp
*/


MyWebApp.namespace("UI");

MyWebApp.UI = (function () {
    "use strict";
    var msgsContainer = new Object()
    var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };

    return {
        ShowLastMsgAndRefresh: function (msg) {
                MyWebApp.UI.showRoasterMessage(msg, Enums.MessageType.Success);
            setTimeout(function () {
                location.reload();
            }, 1000);
        },

        showRoasterMessage: function (message, type, _delay) {
            if (!_delay)
                _delay = 3000;

            var opts = {
                title: "Over Here",
                text: "",
                addclass: "stack-bottomright",
                stack: stack_bottomright,
                history:false,
                shadow: false,
                delay: _delay
            };
              
            switch (type) {
                case Enums.MessageType.Error:

                    opts.title = "Error";
                    opts.text = message;
                    opts.type = "error";

                    break;
                case Enums.MessageType.Info:
                    opts.title = "Information";
                    opts.text = message;
                    opts.type = "info";
                    break;
                case Enums.MessageType.Success:
                    opts.title = "Success";
                    opts.text = message;
                    opts.type = "success";
                    break;
                case Enums.MessageType.Warning:
                    opts.title = "Warning";
                    opts.text = message;
                    opts.type = "notice";
                    break;
            }
            $.pnotify(opts);

        },
        showMessage: function (containerselector, message, type, _delay) {

            if (type == Enums.MessageType.Loading || message == "")
                return;

            this.showRoasterMessage(message, type, _delay);
            
            return;

            

        }, //End of ShwoMessage Function
        getURLParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

    };
} ());

