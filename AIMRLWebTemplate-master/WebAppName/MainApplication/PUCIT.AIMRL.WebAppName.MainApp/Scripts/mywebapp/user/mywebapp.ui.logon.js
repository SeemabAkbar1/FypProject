/// <reference path="../../_references.js" />

/*** 
* Exposes UI functions for the logon page
* @module Logon
* @namespace MyWebApp.UI
*/

MyWebApp.namespace("UI.Logon");

MyWebApp.UI.Logon = (function () {
    "use strict";
    var _isInitialized = false;

    function initialisePage() {

        if (_isInitialized == false) {
            _isInitialized = true;
            ClearFields();
            BindEvents();
            LoadExistingInfo();
        }
    }//End of initialisePage

    function BindEvents() {

        $("#txtUserName").watermark('User Name');
        $("#txtPassword").watermark('Password');

        $('#lnkLogin').click(function () {
            Logon();
            return false;
        });

        $('#txtUserName,#txtPassword').bind('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                Logon();
                e.preventDefault();
                return false;
            }
        });

        $("#resend_password_link").bind("click", function () {
            window.location = $(this).attr("href");
        });

        $("#lnkHelp").bind("click", function () {
            window.location = $(this).attr("href");
        });

        $('#lnkLogin, ul#icons li').hover(
            function () { $(this).addClass('ui-state-hover'); },
            function () { $(this).removeClass('ui-state-hover'); }
        );
        $(document).ready(function () {
            $('#SendButton').click(function (e) {
                //  e.preventDefault();
                sendEmail();
            });//end of click
        });//end of ready




    }//End of BindEvents

    function ClearFields() {

    }//End of ClearFields

    function sendEmail() {
        var Email = $('#emailID').val();
        alert(Email);
       
        debugger;
        var url = "UserInfoData/sendEmail/?emailAddress=" + Email;

        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            debugger;
            if (result.success === true) {
                console.log(result);   
                    alert("Email sent to your account");
            
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting students: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        });
    }

    function LoadExistingInfo() {

        var uName = $.cookie("UserName");
        var pasd = $.cookie("Password");

        if (uName != null && uName != "") {

            $('#txtUserName').val(uName);
            $('#txtPassword').val(pasd);
            $("#chkKeepMeLoggedIn").attr("checked", true);
        }
    }

    function Logon() {

        var userName = $('#txtUserName').val();
        var password = $('#txtPassword').val();

        if ($.trim(userName) === '') {
            MyWebApp.UI.showRoasterMessage("You must enter a user name.", Enums.MessageType.Error);
            $('#txtUserName').focus();
            return;
        }
        if ($.trim(password) === '') {
            MyWebApp.UI.showRoasterMessage("You must enter a password.", Enums.MessageType.Error);
            $('#txtPassword').focus();
            return;
        }

        if ($("#chkKeepMeLoggedIn").is(":checked") == true) {
            $.cookie("UserName", userName, { expires: 5 });
            $.cookie("Password", password, { expires: 5 });
        }
        else {
            $.cookie("UserName", null);
            $.cookie("Password", null);
        }

        var login = {
            UserName: userName,
            Password: password
        }

        var data = JSON.stringify(login);

        var url = "UserInfoData/ValidateUser";

        MyWebApp.Globals.MakeAjaxCall("POST", url, data, function (result) {
            console.log(result);
            if (result.success === true) {

                MyWebApp.UI.showRoasterMessage("Login is successful, entering into the application...", Enums.MessageType.Success);
                debugger
                var returnUrl = MyWebApp.UI.getURLParameterByName("ReturnURL");

                if (returnUrl != "")
                    window.location.href = returnUrl;
                else
                    window.location.href = MyWebApp.Globals.baseURL + result.redirect;

            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error, 5000);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            MyWebApp.UI.showRoasterMessage('There was a problem authenticating your credentials: "' + xhr.responseText + '". Please try again.', Enums.MessageType.Error);
        });
    }//End of Logon function

    return {

        readyMain: function () {
            initialisePage();
        }
    };
}());