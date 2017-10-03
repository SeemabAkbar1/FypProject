MyWebApp.namespace("UI.Header");

MyWebApp.UI.Header = (function () {
    "use strict";
    var _isInitialized = false;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
        }
    }
    function BindEvents() {
        $("#uploadsignature").unbind('click').bind('click', function (e) {
            
            var sign = $("#signImage").get(0).files;
            // Add the uploaded image content to the form data collection
            if (sign.length == 0) {
                $('#_result_ID').text("Please upload image of your signature");
                $('#_result_ID').css("color", "red");
            }
            else {
                var data = new FormData();
                // Add the uploaded image content to the form data collection
                if (sign.length > 0) {
                    data.append("Signature", sign[0]);
                }

                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: window.MyWebAppBasePath + "aapi/Forms/UploadSignature",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        MyWebApp.UI.showRoasterMessage('Signature Uploaded', Enums.MessageType.Success);
                        setTimeout(function () { 
                            location.reload();
                        },1000);
                    },
                    error: function (response) {
                        hideAll();
                        MyWebApp.UI.showRoasterMessage('Some error', Enums.MessageType.Error);
                    }
                });
            }
        });
        //$('#logoutadmin').click(function () {
        //    debugger
        //    var url = "Admin/LogOut";

        //    MyWebApp.Globals.MakeAjaxCall("POST", url, {}, function (result) {
        //        console.log(result);
        //        if (result.success === true) {

        //            MyWebApp.UI.showRoasterMessage("Login is successful, entering into the application...", Enums.MessageType.Success);
        //            debugger
        //            var returnUrl = MyWebApp.UI.getURLParameterByName("ReturnURL");

                  
        //                window.location.href = "/PRMDev/Home";
                  
        //        } else {
        //            MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error, 5000);
        //        }
        //    }, function (xhr, ajaxOptions, thrownError) {
        //        //debugger;
        //        MyWebApp.UI.showRoasterMessage('There was a problem authenticating your credentials: "' + xhr.responseText + '". Please try again.', Enums.MessageType.Error);
        //    });
     //   e.preventDefault();
        //    return false;
        //});
        $("#SearchButton").click(function (e) {
            debugger;
            var number = $("#SearchDiary").val();
            window.location = window.MyWebAppBasePath + "Home/ApplicationView/" + number;
        });


        $(".user-menu .designation a").click(function (e) {
            e.preventDefault();

            if ($(this).hasClass("selected"))
                return false;

            var aid = $(this).attr("aid");

            var url = "UserInfoData/ChangeDesig?aid=" + aid;

            MyWebApp.Globals.MakeAjaxCall("GET", url, {}, function (result) {
                
                if (result.success == true) {
                    MyWebApp.UI.ShowLastMsgAndRefresh("Changed successfully.");
                } else {
                    MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
                }
            }, function (xhr, ajaxoptions, thrownerror) {
                MyWebApp.UI.showRoasterMessage('A problem has occurred."' + thrownerror + '". please try again.', Enums.MessageType.Error);
            });

            return false;
        });
    }

    return {

        readyMain: function () {
            initialisePage();
        }
    };
}
    ());