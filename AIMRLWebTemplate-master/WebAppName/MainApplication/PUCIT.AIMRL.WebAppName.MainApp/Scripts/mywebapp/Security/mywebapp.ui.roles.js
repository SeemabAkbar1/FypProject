MyWebApp.namespace("UI.Role");

MyWebApp.UI.Role = (function () {
    "use strict";
    var _isInitialized = false;
   
    var rolesData;
    var temp_perdata;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
            getAllRoles();
            getActivePermissions();
        }
    }

    function BindEvents() {
        
        $("#selectType1").change(function (e) {
            e.preventDefault();
            debugger;
            var selected_dropdownindex1 = parseInt($('#selectType1').val());
            var data = {};
            data.RoleList = [];

            if (selected_dropdownindex1 == -1) {
                data = rolesData;
            }
            else {
                data.RoleList = rolesData.RoleList.filter(p=> p.IsActive == Boolean(selected_dropdownindex1));
            }

            displayAllRoles(data);

        }); //End of Save Click
        
        $("#newrole").click(function (e) {
            e.preventDefault();
            clearFeilds();
            $.bsmodal.show("#modal-form");
        });

        $("#Save").unbind('click').bind('click', function (e) {
            e.preventDefault();
            
            if ($("#rolename").val() == "" || $('#roledescription').val() == "") {
                MyWebApp.UI.showRoasterMessage("Empty Field(s)", Enums.MessageType.Error, 2000);
            }
            else {
                $.bsmodal.hide("#modal-form");

                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: "Save",
                    bodyText: 'Do you want to Save this record?',
                    dataToPass: { 
                        RoleId: $("#hiddenid").val(),
                        Name: $("#rolename").val(),
                        Description: $('#roledescription').val()
                    },
                    fnYesCallBack: function ($modalObj, dataObj) {
                        saveRole(dataObj);
                        $modalObj.hideMe()
                    }
                });
            }
            return false;
        });

        $("#ModalClose, #Cancel").click(function (e) {
            e.preventDefault();
            $.bsmodal.hide("#modal-form");
            return false;
        });

        $("#SaveMappings").unbind('click').bind('click',function(){
            SavePermissionMapping();
            return false;
        });

    }
    
    //-----------------Role Management + Grid Event Handling
    function saveRole(dataObj) {
        debugger;
        var roleObjToSend = {
            Id: dataObj.RoleId,
            Name: dataObj.Name,
            Description: dataObj.Description
        }

        var dataToSend = JSON.stringify(roleObjToSend);
        var url = "Security/SaveRole";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);

                var roleObj = rolesData.RoleList.find(p => p.Id == roleObjToSend.Id);
                
                if(roleObj){
                    roleObj.Name = roleObjToSend.Name;
                    roleObj.Description = roleObjToSend.Description;
                }
                else {
                    roleObjToSend.Id = result.data.RoleId;
                    roleObjToSend.IsActive = true;
                    rolesData.RoleList.push(roleObjToSend);
                }
              
                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);
            }
            $.bsmodal.hide("#modal-form");
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this Role: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });

    }
    function clearFeilds() {
        
        $("#hiddenid").val("");
        //$("#IsActive2").val("-1");
        $("#rolename").val("");
        $("#roledescription").val("");
    }

    function EnableDisableRole(dataObj) {
        var roleData = {
            Id: dataObj.RoleId,
            IsActive: !dataObj.IsActive
        }

        var dataToSend = JSON.stringify(roleData);
        var url = "Security/EnableDisableRole";

        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                
                var roleObj = rolesData.RoleList.find(p => p.Id == roleData.Id);
                roleObj.IsActive = roleData.IsActive;
               
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);

                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);                
            }
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this Role: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });
    }
    function getAllRoles() {

        var url = "Security/getRoles";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                displayAllRoles(result.data);
                rolesData = result.data;

            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Roles: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);

    }
    function displayAllRoles(RoleList) {

        $("#simple-table").html("");

        if (!RoleList)
            return;

        try {
            var source = $("#RoleTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(RoleList);
        } catch (e) {
            debugger;
        }

        $("#simple-table").append(html);
        BindGridEvents();
    }
    

    function BindGridEvents(){
        $("#simple-table .lnkEdit" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            HandleEditRole(id);
            return false;
        });

        $("#simple-table .lnkDelete" ).unbind('click').bind('click', function(){
            debugger;
            var id = $(this).closest('tr').attr('id');
            HandleEnableDisableRole(id);
            return false;
        });

        $("#simple-table .lnkEditMapping" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            GeneratePermissionsHTML(temp_perdata);
            GetPermissionsByRoleID(id);
            
            return false;
        });
    }
    function HandleEditRole(RoleId){

        if (rolesData ) {
            var roleObj = rolesData.RoleList.find(p => p.Id == RoleId);
            if(roleObj){
                if(roleObj.IsActive == false){
                    MyWebApp.UI.showRoasterMessage("Editing is not allowed as record is disabled", Enums.MessageType.Error);
                    return false;
                }

                $("#hiddenid").val(roleObj.Id);
                $("#rolename").val(roleObj.Name);
                $("#roledescription").val(roleObj.Description);
           
                $.bsmodal.show("#modal-form");
            }//end of roleObj
        }//end of rolesData
    }
    function HandleEnableDisableRole(RoleId){

        if (rolesData ) {
            var roleObj = rolesData.RoleList.find(p => p.Id == RoleId);
            if(roleObj){
                
                var header = (roleObj.IsActive == false ? "Enable" : "Disable");
                
                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: header,
                    bodyText: 'Do you want to ' + header + ' this record?',
                    dataToPass: { "RoleId": roleObj.Id, "IsActive" : roleObj.IsActive },
                    fnYesCallBack: function ($modalObj,dataObj) {
                        EnableDisableRole(dataObj);
                        $modalObj.hideMe();
                    }
                });
            }//end of roleObj
        }//end of rolesData
    }

    //-------------------------- Permissions/Mappings Code

    function getActivePermissions() {

        var url = "Security/getActivePermissions";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                temp_perdata = result.data;
                GeneratePermissionsHTML(temp_perdata);
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Permissions: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }
    function GeneratePermissionsHTML(Permissions) {
        $("#sortable1").html("");

        if (!Permissions)
            return;

        try {
            var source = $("#PermissionTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(Permissions);

            $("#sortable1").append(html);
        }
        catch (e) {
            ////debugger;
        }
    }
    function GetPermissionsByRoleID(pRoleID){

        var url = "Security/GetPermissionsByRoleID?pRoleID=" + pRoleID;
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                SelectPermissionsInPopupForCurrentRole(result.data.Permissions,pRoleID);
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Permissions: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }
    function SelectPermissionsInPopupForCurrentRole(permissionIds,pRoleID){

        $("#sortable1 li").each(function(){
            var id = parseInt($(this).attr("id"));
            if( permissionIds.indexOf(id) >= 0)
                $(this).find("input[type=checkbox]").attr("checked",true);
        });

        $("#EditPermissionsModal").data('RoleID',pRoleID);
        $.bsmodal.show("#EditPermissionsModal",{top:"5%",left:"25%",closeid:"#closeedit,#CancelPermModal"});
    }

    function SavePermissionMapping(){
        var permList = [];
        $("#sortable1 li :checked").each(function(){
            var permId = $(this).closest('li').attr("id");
            permList.push(parseInt(permId));
        });

        var roleID = $("#EditPermissionsModal").data('RoleID');
        var dataToSend = {};
        dataToSend.RoleID = roleID;
        dataToSend.Permissions = permList;

        dataToSend = JSON.stringify(dataToSend);
        var url = "Security/SaveRolePermissionMapping";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);
                debugger;
                $.bsmodal.hide("#EditPermissionsModal");
            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);
            }
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });
        
    }

    return {

        readyMain: function () {
            initialisePage();

        }
    };
}
());