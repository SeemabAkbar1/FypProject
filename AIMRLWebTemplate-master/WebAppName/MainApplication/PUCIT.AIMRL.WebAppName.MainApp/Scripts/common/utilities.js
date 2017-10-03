Utilities = (function () {
    "use strict";
    return {

        LoadDropDown: function (dropdown, dataToBind, valueField, textField, defaultOptText, dataAttrField1, dataAttrField2) {
            if (defaultOptText)
            { }
            else
                defaultOptText = "--Select--";

            var $cmb = dropdown.html('');
            $("<option value='0'>" + defaultOptText + "</option>").appendTo($cmb);

            if (dataToBind == null)
                return false;

            for (var t = 0; t < dataToBind.length; t++) {
                if (dataToBind[t]) {
                    var val = dataToBind[t][valueField];
                    var txt = dataToBind[t][textField];
                    if (dataAttrField1) {
                        var extra = dataToBind[t][dataAttrField1];
                        var field2 = null;

                        if (dataAttrField2) {
                            field2 = dataToBind[t][dataAttrField2];
                        }
                        if (field2) {
                            $("<option value='" + val + "'>" + txt + "</option>").data(dataAttrField1, extra).data(dataAttrField2, field2).appendTo($cmb);
                        }
                        else {
                            $("<option value='" + val + "' dataAttrField1='" + extra + "'>" + txt + "</option>").data(dataAttrField1, extra).appendTo($cmb);
                        }
                    }
                    else {
                        $("<option value='" + val + "'>" + txt + "</option>").appendTo($cmb);
                    }

                }

            }
        }, //End of LoadDropDown

        IsValidDate: function (dateToValidate) {
            var result = Date.parse(dateToValidate);
            if (result) {
                return Date.parse(new Date(result).toString("M/d/yyyy"));
            }
            else
                return null;
        },
        DiffofDatesInMin: function (date1, date2) {
            var diffMs = (date1 - date2); // milliseconds
            //var diffDays = Math.round(diffMs / 86400000); // days
            //var diffHrs = Math.round((diffMs % 86400000) / 3600000); // hours
            //var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000); // minutes
            var diff = Math.round(diffMs / 60000); //Get diff in Minutes
            return diff;
        },

        IsValidTime: function (time) {
            var IsMatch = time.match(/^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$/);
            if (IsMatch == null)
                return false;
            else
                return true;

        }
    };
}());