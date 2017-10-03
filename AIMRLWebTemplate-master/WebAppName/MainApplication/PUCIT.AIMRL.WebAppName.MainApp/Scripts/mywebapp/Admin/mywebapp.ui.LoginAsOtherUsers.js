/// <reference path="../../_references.js" />

/*** 
* Exposes UI functions for the LoginAsOtherUsers page
* @module LoginAsOtherUsers
* @namespace MyWebApp.UI
*/

MyWebApp.namespace("UI.LoginAsOtherUsers");

MyWebApp.UI.LoginAsOtherUsers = (function () {
    "use strict";
    var _isInitialized = false;

    var mainAutocompleteObj1;
    var contributorsList = [];
    var isLogLoaded = false;
    var areContributorsLoaded = false;
    function initialisePage() {

        if (_isInitialized == false) {
            _isInitialized = true;
            ClearFields();
            BindEvents();
            LoadExistingInfo();
            mainAutocompleteObj1 = new JQUIAutoCompleteWrapper({
                inputSelector: "#txtUserName",
                dataSource: "Admin/SearchUser",
                queryString: "",
                listItemClass: ".listitem",
                searchParameterName: "key",
                maxItemsToDisplay: "1",
                minCharsToTypeForSearch: "2",

                watermark: "Type Login/Name",
                dropdownHTML: "<a><table><tr><td>Login(Name)</td></tr></table></a>",
                fields: {
                    ValueField: 'ID', DisplayField: 'Login', DescriptionField: 'Name'
                },
                enableCache: false,
                onClear: function () {

                }
               , displayTextFormat: "Login"
                // watermark: "Type Name/Designation",
                // dropdownHTML: "<a><table><tr><td>NME(Desg)</td></tr></table></a>",
                // fields: {
                //     ValueField: 'ID', DisplayField: 'Desg', DescriptionField: 'NME'
                // },
                // enableCache: false,
                // onClear: function () {

                // }
                //, displayTextFormat: "NME(Desg)"
               , itemOnClick: function (item) {
                   var li = $('<li class="dd-handle" uid=' + item.ID + '>' + item.Desg + '  <i class="btn-group pull-right fa fa-times bigger-110 red2 removeContributor"></i></li>');
                   $("#sortable").append(li);
                   li.find(".removeContributor").click(function () {
                       $(this).closest('li').remove();
                   });


               }
            });

            mainAutocompleteObj1.InitializeControl();
        }
    }
    function BindEvents() {

        $("#txtUserName").watermark('User Name');

        $('#lnkLogin').click(function () {
            LoginAsOtherUsers();
            return false;
        });


        $('#txtUserName').bind('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                LoginAsOtherUsers();
                e.preventDefault();
                return false;
            }
            else if (code == 81) {
                $("#txtUserName").autocomplete({
                    source: "/Admin/AutocompleteSuggestions",
                    minLength: 1,
                    select: function (event, ui) {
                        if (ui.item) {
                            $("#txtUserName").val(ui.item.value);
                            $("form").submit();
                        }
                    }
                });
                e.preventDefault();
                return false;
            }

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

        if (uName != null && uName != "") {

            $('#txtUserName').val(uName);
            $("#chkKeepMeLoggedIn").attr("checked", true);
        }
    }

    function LoginAsOtherUsers() {

        var userName = $('#txtUserName').val();

        if ($.trim(userName) === '') {
            MyWebApp.UI.showRoasterMessage("You must enter a user name.", Enums.MessageType.Error);
            $('#txtUserName').focus();
            return;
        }

        if ($("#chkKeepMeLoggedIn").is(":checked") == true) {
            $.cookie("UserName", userName, { expires: 5 });
        }
        else {
            $.cookie("UserName", null);
        }

        var login = {
            UserName: userName
        }

        var data = JSON.stringify(login);

        var url = "Admin/ValidateUser";

        MyWebApp.Globals.MakeAjaxCall("POST", url, data, function (result) {
            console.log(result);
            if (result.success === true) {

                MyWebApp.UI.showRoasterMessage("Login is successful, entering into the application...", Enums.MessageType.Success);

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
    }//End of LoginAsOtherUsers function

    return {

        readyMain: function () {
            initialisePage();
        }
    };
}());