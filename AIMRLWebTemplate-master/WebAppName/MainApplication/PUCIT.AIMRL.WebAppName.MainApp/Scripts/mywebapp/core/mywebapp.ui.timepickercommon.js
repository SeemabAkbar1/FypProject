
MyWebApp.namespace("UI.TimePickerCommon");

MyWebApp.UI.TimePickerCommon = (function () {
    "use strict";
    //Private static Members

    var callbacks = {

    };

    //Public static Members
    return {
        initialisePage: function () {

        } // initialisePage
        , TimePickerLoadingOnClick: function (timepickerclass, starttimeclass, endtimeclass, Is24Hour) {
            //$(".timeAdd").live("click", function () {            
            $(timepickerclass).live("click", function () {
                //debugger;
                var startdatefield = $(this).closest("div.input-append").find(starttimeclass);//".startdate");
                var enddatefield = $(this).closest("div.input-append").find(endtimeclass);//".enddate");
                if (startdatefield.val() != "")
                    startdatefield.timepicker('setTime', startdatefield.val());
                else
                    Is24Hour == true? startdatefield.timepicker('setTime', "0:00"): startdatefield.timepicker('setTime', "12:00 AM");

                if (enddatefield.val() != "")
                    enddatefield.timepicker('setTime', enddatefield.val());
                else
                    Is24Hour == true ? enddatefield.timepicker('setTime', "0:00") : enddatefield.timepicker('setTime', "12:00 AM");
            });
        }
        
        ,EnableDisableCalendarIcon: function(lipanel, blenable) {
            if (blenable === undefined) {
                lipanel.each(function () {
                    var ischecked = $(this).find(".CheckBox").is(":checked");
                    if (ischecked) {
                        MyWebApp.UI.TimePickerCommon.CalendarIconEnabled($(this));
                    }
                    else {
                        MyWebApp.UI.TimePickerCommon.CalendarIconDisabled($(this));
                    }
                });
            }
            else if (blenable) {
                //check added on 2nd october, 2014 - introduce new timepicker
                lipanel.each(function () {
                    MyWebApp.UI.TimePickerCommon.CalendarIconEnabled($(this));
                });
            }
            else {
                //check added on 2nd october, 2014 - introduce new timepicker
                lipanel.each(function () {
                    MyWebApp.UI.TimePickerCommon.CalendarIconDisabled($(this));

                });
            }
    }
    ,CalendarIconEnabled: function(currli) {
        var calendaricon = currli.find("span");
        calendaricon.removeClass("disabled");
        calendaricon.addClass("add-on ");
        calendaricon.find("i").removeClass("ico-disabled");
        calendaricon.find("i").addClass("timeAdd");
    }

    ,CalendarIconDisabled: function(currli) {
        var calendaricon = currli.find("span");
        calendaricon.removeClass("add-on ");
        calendaricon.addClass("disabled");
        calendaricon.find("i").addClass("ico-disabled");
        calendaricon.find("i").removeClass("timeAdd");
    }
    };
}());

