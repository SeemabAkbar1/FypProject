MyWebApp.namespace("UI.ChangePassoword");

MyWebApp.UI.ChangePassword = (function () {
    "use strict";
    var _isInitialized = false;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
        }
    }

    function BindEvents() {

           $(document).ready(function () {
            $('#SaveButton').click(function (e) {
                e.preventDefault();
                changePassword();
            });//end of click
        });//end of ready

    }//End of Bind Events


    function changePassword() {

        //alert(NewPassword);
        if ($('#form-field-pass2').val() != $('#form-field-pass3').val()) {
            alert("The two password fields doesn't match ");
        }
        else {
            var password = {
                CurrentPassword: $('#form-field-pass1').val(),
                NewPassword: $('#form-field-pass2').val()
            }
            var dataToSend = JSON.stringify(password);
            var url = "Dashboard/changePassword";
            //  jQuery.support.cors = true;
            debugger;
            MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

                if (result.success === true) {
                    MyWebApp.UI.showRoasterMessage(result.success, Enums.MessageType.Error);

                } else {
                   // alert("Password not Changed Successfully");
                    MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);

                }
            }, function (xhr, ajaxoptions, thrownerror) {
                alert("Error Has occured");
            });
        }
    }
    return {

        readyMain: function () {
            initialisePage();
        }
    };
}
    ());