/*** 
* Used for defining the MyWebApp UI
* @module UI
* @namespace MyWebApp
*/


MyWebApp.namespace("Globals");

MyWebApp.Globals = (function () {
    "use strict";
    var counter = 0;
    var host1 = window.location.host;
    var webapiurlbase = window.MyWebAppBasePath + "aapi/";
    var normalurlbase = window.MyWebAppBasePath;
    var loginBoxOpened = false;

    var test = 0;
    $.ajaxSetup({
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        cache: false
    });

    function ShowSpinner() {
        var opts = {
            lines: 11, // The number of lines to draw
            length: 3, // The length of each line
            width: 3, // The line thickness
            radius: 24, // The radius of the inner circle
            corners: 0, // Corner roundness (0..1)
            rotate: 30, // The rotation offset
            color: '#FFF', // #rgb or #rrggbb
            speed: 0.6, // Rounds per second
            trail: 64, // Afterglow percentage
            shadow: true, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };

        //$("#divProgressStatus").spin(opts);
    }
    function HideSpinner() {
        //$("#divProgressStatus").spin(false);
    }

    return {
        val: test
        , baseURL: normalurlbase
        , ShowSpinner: function (bShowOverlay) {
            $('#divProgressStatus').show();
            if (bShowOverlay) {
                $('#divProgressOverlay').show();
            }
            ShowSpinner();
        }
        , HideSpinner: function () {
            $('#divProgressStatus').hide();
            $('#divProgressOverlay').hide();
            HideSpinner();
        }
        , MakeAjaxCall: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {

            //MyWebApp.Debugger.log('ajax call: ' + URL + ' with data: ' + data + ' at ' + new Date());

            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = true;

            if (showProgress) {
                counter++;
                if (counter > 0) {
                    $('#divProgressOverlay').show();
                    $('#divProgressStatus').show();
                    ShowSpinner();
                }
            }

            var urltocall = webapiurlbase + URL;

            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                beforeSend: function (jqXHR, settings) {

                },
                success: function (resp) {
                    try {
                        //var result = JSON.parse(resp);
                        //var result = resp;
                        if (successCallback)
                            successCallback(resp);
                    } catch (err) {
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 406) {

                        var retPath = "";
                        if (window.location.pathname) {
                            retPath = "?ReturnURL=" + window.location.pathname;
                        }
                        window.location.href = MyWebApp.Resources.Views.LoginURL + retPath;
                        return;
                    }
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);
                    else {
                        //alert(err.status + " - " + err.responseText + " - " + httpStatus);
                    }

                },
                complete: function () {
                    if (showProgress) {
                        counter--;
                        if (counter <= 0) {
                            $('#divProgressOverlay').hide();
                            $('#divProgressStatus').hide();
                            HideSpinner();
                        }
                    }
                }
            });

            return defObj;
        } //End of MakeAjaxCall

    , SetPageHeading: function (heading, PageHeadingSelector) {
        if (PageHeadingSelector == null) {
            PageHeadingSelector = "span#pageheading";
        }

        $(".page-heading.row").find(PageHeadingSelector).text(heading);
    }
    , SetPageHeadingIconClass: function (iconClass) {
        $("#spHeadingIcon").removeClass();
        $("#spHeadingIcon").addClass(iconClass);
    },
        GetControllerPath: function (actionName, controllerName) {
            return "".format("{0}{1}/{2}", MyWebApp.Globals.baseURL, controllerName, actionName);
        },
        ShowYesNoPopup: function (settings) {
            
            var $modal = $("#divYesNoCustomModal");

            if (settings.dataToPass) {
                $modal.data("dataToPass", settings.dataToPass)
            }

            $modal.find(".yesnoheader").text(settings.headerText);
            $modal.find(".yesnotext").text(settings.bodyText);

            $modal.find("#btnDivYesNoCustomYes").unbind("click").bind('click', function () {
                if (settings.fnYesCallBack)
                    settings.fnYesCallBack($modal, $modal.data("dataToPass"));
            });

            $modal.find("#btnDivYesNoCustomNo,#btnDivYesNoCustomNoCloseButton").unbind("click").bind('click', function () {
                if (settings.fnNoCallBack)
                    settings.fnNoCallBack($modal, $modal.data("dataToPass"));
                $.bsmodal.hide("#divYesNoCustomModal");
            });

            $.bsmodal.show("#divYesNoCustomModal");

            $modal.hideMe = function () {
                $.bsmodal.hide("#divYesNoCustomModal");
            }

        }
    };
}());

