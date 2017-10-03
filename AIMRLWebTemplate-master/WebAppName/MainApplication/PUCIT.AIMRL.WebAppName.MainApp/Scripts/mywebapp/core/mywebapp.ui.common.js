
MyWebApp.namespace("UI.Common");

MyWebApp.UI.Common = (function () {
    "use strict";
    //Private static Members

    var callbacks = {

    };

    //Public static Members
    return {

        initialisePage: function () {

        } // initialisePage
        , GetCurrentDateInSelectedTimeZone: function () {
            if (MyWebApp.UI.Common.CurrentCampSettings)
                var todaysdate = Date.parse(MyWebApp.UI.Common.CurrentCampSettings.CurrentTime).clone(); //new Date();            
            else
                var todaysdate = new Date();
            return todaysdate;
        },
        UserDetail: {},
        SetToolTip: function (block) {
            block.tooltip({
                track: true,
                delay: 0,
                showURL: false,
                showBody: " - "
                //fade: 250
            });
        },
        
        
        DateTimeDifferenceFloor: function (enddate, startdate, tovalue) {
            return DateTimeDifferenceInPST(startdate, enddate, tovalue, false);
        },

        DateTimeDifferenceCeil: function (enddate, startdate, tovalue) {
            return DateTimeDifferenceInPST(startdate, enddate, tovalue, true);
        },
        DateTimeDifferenceInPST: function (startdate, enddate, tovalue, IsCiel) {

            var d1 = Date.UTC(startdate.getFullYear(), startdate.getMonth(), startdate.getDate(), startdate.getHours(), startdate.getMinutes(), startdate.getSeconds());
            var d2 = Date.UTC(enddate.getFullYear(), enddate.getMonth(), enddate.getDate(), enddate.getHours(), enddate.getMinutes(), enddate.getSeconds());

            if (IsCiel) {
                return Math.ceil((d2 - d1) / tovalue);
            }
            else {
                return Math.floor((d2 - d1) / tovalue);
            }
        },
        
        ValidateEmailAddress: function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
            return pattern.test(emailAddress);
        },

        TrimContents: function (selector, length) {
            $(selector).each(function () {
                var name = $(this).html();
                if (name.length > length)
                    $(this).html(name.substring(0, length) + "..");
            });
        },
        setHandlebarTemplate: function (templateSelector, selectorToApply, data) {
            $(selectorToApply).html('');
            var source = $(templateSelector).html();
            var template = Handlebars.compile(source);

            var html = template(data);
            $(selectorToApply).html(html);
        }
        , registerCallbacks: function (cbacks) {
            callbacks = $.extend(callbacks, cbacks);
            return this;
        } //registerCallbacks      
    };
}());

