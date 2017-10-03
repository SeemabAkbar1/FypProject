/*** 
* Used for defining the MyWebApp UI SignUps
* @module SignUps
* @namespace MyWebApp.UI
*/


MyWebApp.namespace("UI.SignUps");

MyWebApp.UI.SignUps = (function () {
    "use strict";

    var systemUser = null;

    function initialiseControls() {

        $('#UsernameTextBox').blur(function () {
            verifyUserName();
        });
        $('#emailAddressTextBox').blur(function () {
            verifyEmail();
        });

        //debugger;        
        $("#logonCommandButton").click(function (e) {

            //debugger;
            $("#signupform").validate();

//            if ($("#signupform").valid() == false)
//                return false;

            if (InsertUserDetails() == false)
                return false;

            if (systemUser == null)
                return false;

            //debugger;

            var jsondatatosave = JSON.stringify(systemUser);

            var url = 'UserInfoData';

            MyWebApp.Globals.MakeAjaxCall("POST", url, jsondatatosave, function (data) {
                //debugger;
                
                if (data.AccountCreated == false) {

                    if (data.UserNameExists == true) {

                        MyWebApp.UI.showMessage("#spusernamestatus", "User with User Name '" + systemUser.Username + "' already exists!", Enums.MessageType.Error);
                    }
                    if (data.EmailExists == true) {
                        MyWebApp.UI.showMessage("#spemailstatus", "User with Email '" + systemUser.EmailAddress + "' already exists!", Enums.MessageType.Error);
                    }
                    displayErrorMessage('There are issues with the data you entered. Please correct and try again.');
                    return false;
                }
                else {
                    fnClearAllFileds();
                    displaySuccessMessage('Account has been created!');
                }

            }, function (xhr, ajaxOptions, thrownError) {
                //$('#divProcessingMessage').hide();
                displayErrorMessage('There was a problem saving the user details: "' + thrownError + '". Please try again.');
            }); //End of MakeAjaxCall to Save Data

            return false;
        });


    } //End of IntiializeControls

    function InsertUserDetails() {

        var firstname = $.trim($('#firstnameTextBox').val()),
            emailAddress = $.trim($('#emailAddressTextBox').val()),
            passwordTextBox = $.trim($('#PasswordTextBox').val()),
            confirmPassword = $.trim($('#ConfirmPasswordTextBox').val()),
            username = $.trim($('#UsernameTextBox').val());

        if (username === '') {
            displayErrorMessage('You must enter a username.');
            $('#UsernameTextBox').focus();
            return false;
        }
        if (firstname === '') {
            displayErrorMessage('You must enter a Name.');
            $('#firstnameTextBox').focus();
            return false;
        }

        else if (emailAddress === '') {
            displayErrorMessage('You must enter a EmailId.');
            $('#emailAddressTextBox').focus();
            return false;
        }
        else if (isValidEmailAddress(emailAddress) == false) {
            displayErrorMessage('This email address is not valid.');
            $('#emailAddressTextBox').focus();
            return false;
        }
        if (passwordTextBox === '') {
            displayErrorMessage('You must enter a password.');
            $('#PasswordTextBox').focus();
            return false;
        }
        if (confirmPassword === '') {
            displayErrorMessage('You must enter a confirm password.');
            $('#ConfirmPasswordTextBox').focus();
            return false;
        }
        if (passwordTextBox != confirmPassword) {
            displayErrorMessage('Your confirm password must match with password.');
            $('#ConfirmPasswordTextBox').focus();
            return false;
        }

        systemUser = new fnsystemUser();
        systemUser.Name = firstname;
        systemUser.Login = username;
        systemUser.EmailAddress = emailAddress;
        systemUser.Password = passwordTextBox;


    } // End of InsertUser Details

    function verifyUserName() {

        if ($.trim($('#UsernameTextBox').val()) == "") {
            $("#spusernamestatus").hide();
            return false;
        }
        //debugger;
        var url = "UserInfoData/VerifyUserName/?pDataToVerify=" + $.trim($('#UsernameTextBox').val());

        MyWebApp.UI.showMessage("#spusernamestatus", "Processing...", Enums.MessageType.Loading);

        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (data) {
            //debugger;
            if (data == "Error")
            { return false; }



            if (data.Result == true) {
                MyWebApp.UI.showMessage("#spusernamestatus", "User with User Name '" + data.UserName + "' already exists!", Enums.MessageType.Error);
            }
            else {
                MyWebApp.UI.showMessage("#spusernamestatus", "Available", Enums.MessageType.Success);
            }
        });
    } //Verify User Name

    function verifyEmail() {

        if ($.trim($('#emailAddressTextBox').val()) == "") {
            $("#spemailstatus").hide();
            return false;
        }

        var url = "UserInfoData/VerifyEmailAddress/?pDataToVerify=" + $.trim($('#emailAddressTextBox').val());

        MyWebApp.UI.showMessage("#spemailstatus", "Processing...", Enums.MessageType.Loading);

        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (data) {

            if (data == "Error")
            { return false; }

            if (data.Result == true) {
                MyWebApp.UI.showMessage("#spemailstatus", "User with Email '" + data.Email + "' already exists!", Enums.MessageType.Error);
            }
            else {
                MyWebApp.UI.showMessage("#spemailstatus", "Available", Enums.MessageType.Success);
            }
        });
    }


    function fnsystemUser() {
        this.Name = null;
        this.EmailAddress = null;
        this.Password = null;
        this.Login = null;
        this.UserID = 0;
    };

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };

    function displayErrorMessage(message) {
        if (message == "") {
            $("#spresultstatus").hide();
        }
        //$('#ErrorMessage').hide();
        else {
            MyWebApp.UI.showMessage("#spresultstatus", message, Enums.MessageType.Error);

        }
    }
    function displaySuccessMessage(message) {

        MyWebApp.UI.showMessage("#spresultstatus", message, Enums.MessageType.Success);
    }

    function fnClearAllFileds() {
        $('#firstnameTextBox').val("");
        $('#lastnameTextBox').val("");
        $('#emailAddressTextBox').val("");
        $('#PasswordTextBox').val("");
        $('#ConfirmPasswordTextBox').val("");
        $('#UsernameTextBox').val("");

        $("#spusernamestatus").hide();
        $("#spemailstatus").hide();
        displayErrorMessage("");
    }

    return {

        initialisePage: function () {
            MyWebApp.Debugger.log('SignUps:initialisePage');
            initialiseControls();

        }

    };



} ());

