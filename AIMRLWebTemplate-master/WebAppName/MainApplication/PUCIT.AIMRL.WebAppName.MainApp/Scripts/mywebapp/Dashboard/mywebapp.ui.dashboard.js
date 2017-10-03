MyWebApp.namespace("UI.Dashboard");

MyWebApp.UI.Dashboard = (function () {
    "use strict";
    var _isInitialized = false;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;

            var txt = $("#spMessage").text();
            if (txt.length > 0)
                MyWebApp.UI.showRoasterMessage(txt, Enums.MessageType.Error);
        }
    }
   
    
    return {

        readyMain: function () {
            initialisePage();
        }
    };
}
    ());