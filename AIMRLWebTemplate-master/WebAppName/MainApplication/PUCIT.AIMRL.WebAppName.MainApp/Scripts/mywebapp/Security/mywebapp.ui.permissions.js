MyWebApp.namespace("UI.Permission");

MyWebApp.UI.Permission = (function () {
    "use strict";
    var _isInitialized = false;
   
    var permissionsdata;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
            viewAllPermissions();
        }
    }

    function BindEvents() {
        
        $("#selectType1").change(function (e) {
            e.preventDefault();
            
            var selected_dropdownindex1 = parseInt($('#selectType1').val());
            var data = {};
            data.PermissionList = [];

            if (selected_dropdownindex1 == -1) {
                data = permissionsdata;
            }
            else {
                data.PermissionList = permissionsdata.PermissionList.filter(p=> p.IsActive == Boolean(selected_dropdownindex1));
            }

            displayAllPermissions(data);

        }); //End of Save Click
        
        $("#newpermission").click(function (e) {
            e.preventDefault();
            clearFeilds();
            $.bsmodal.show("#modal-form1");
        });

        $("#Saveper").unbind('click').bind('click', function (e) {
            e.preventDefault();
            
            if ($("#permissionname").val() == "" || $('#permissiondescription').val() == "") {
                MyWebApp.UI.showRoasterMessage("Empty Field(s)", Enums.MessageType.Error, 2000);
            }
            else {
                $.bsmodal.hide("#modal-form1");

                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: "Save",
                    bodyText: 'Do you want to Save this record?',
                    dataToPass: { 
                        PermId: $("#hiddenidp").val(),
                        Name: $("#permissionname").val(),
                        Description: $('#permissiondescription').val()
                    },
                    fnYesCallBack: function ($modalObj, dataObj) {
                        savePermission(dataObj);
                        $modalObj.hideMe()
                    }
                });
            }
            return false;
        });

        $("#ModalClose1, #CancelPer").click(function (e) {
            e.preventDefault();
            hideAll();
            return false;
        });
    }

    function savePermission(dataObj) {
        
        var permObjToSend = {
            Id: dataObj.PermId,
            Name: dataObj.Name,
            Description: dataObj.Description
        }

        var dataToSend = JSON.stringify(permObjToSend);
        var url = "Security/SavePermission";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);
                hideAll();

                var permObj = permissionsdata.PermissionList.find(p => p.Id == permObjToSend.Id);
                debugger;

                if(permObj){
                    permObj.Name = permObjToSend.Name;
                    permObj.Description = permObjToSend.Description;
                }
                else {
                    permObjToSend.Id = result.data.PermssionId;
                    permObjToSend.IsActive = true;
                    permissionsdata.PermissionList.push(permObjToSend);
                }
              
                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);
                hideAll();
            }
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this Permission: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });

    }
    function EnableDisablePermission(dataObj) {
        var permissionData = {
            Id: dataObj.PermId,
            IsActive: !dataObj.IsActive
        }

        var dataToSend = JSON.stringify(permissionData);
        var url = "Security/EnableDisablePermission";

        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);
                hideAll();
                var permObj = permissionsdata.PermissionList.find(p => p.Id == permissionData.Id);
                permObj.IsActive = permissionData.IsActive;
               
                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);
                hideAll();
            }
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this Permission: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });
    }
    function viewAllPermissions() {

        var url = "Security/getPermissions";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                displayAllPermissions(result.data);
                permissionsdata = result.data;

            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Permissions: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);

    }
    function displayAllPermissions(PermissionList) {

        $("#simple-table2").html("");

        if (!PermissionList)
            return;

        try {
            var source = $("#PermissionTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(PermissionList);
        } catch (e) {
            debugger;
        }

        $("#simple-table2").append(html);

        BindGridEvents();

    }
    function BindGridEvents(){
        $("#simple-table2 .lnkEdit" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            HandleEditPermission(id);
            return false;
        });

        $("#simple-table2 .lnkDelete" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            HandleEnableDisablePermission(id);
            return false;
        });
    }
 
    function HandleEditPermission(permid){

        if (permissionsdata ) {
            var permObj = permissionsdata.PermissionList.find(p => p.Id == permid);
            if(permObj){
                if(permObj.IsActive == false){
                    MyWebApp.UI.showRoasterMessage("Editing is not allowed as record is disabled", Enums.MessageType.Error);
                    return false;
                }

                $("#hiddenidp").val(permObj.Id);
                $("#permissionname").val(permObj.Name);
                $("#permissiondescription").val(permObj.Description);
           
                $.bsmodal.show("#modal-form1");
            }//end of permObj
        }//end of permissionsdata
    }

    function HandleEnableDisablePermission(permid){

        if (permissionsdata ) {
            var permObj = permissionsdata.PermissionList.find(p => p.Id == permid);
            if(permObj){
                
                var header = (permObj.IsActive == false ? "Enable" : "Disable");
                
                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: header,
                    bodyText: 'Do you want to ' + header + ' this record?',
                    dataToPass: { "PermId": permObj.Id, "IsActive" : permObj.IsActive },
                    fnYesCallBack: function ($modalObj,dataObj) {
                        EnableDisablePermission(dataObj);
                        $modalObj.hideMe();
                    }
                });
            }//end of permObj
        }//end of permissionsdata
    }

    function hideAll() {
        $.bsmodal.hide("#modal-form1");
        clearFeilds();
    }

    function clearFeilds() {
        
        $("#hiddenidp").val("");
        //$("#IsActive2").val("-1");
        $("#permissionname").val("");
        $("#permissiondescription").val("");
    }

    return {

        readyMain: function () {
            debugger;
            initialisePage();

        }
    };
}
());