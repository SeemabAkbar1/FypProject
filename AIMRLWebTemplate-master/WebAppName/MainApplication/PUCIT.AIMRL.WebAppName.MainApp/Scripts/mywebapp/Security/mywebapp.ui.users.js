MyWebApp.namespace("UI.User");

MyWebApp.UI.User = (function () {
    "use strict";
    var _isInitialized = false;
   
    var usersData;
    var temp_roleData;

    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
            getAllUsers();
            getActiveRoles();
        }
    }

    function BindEvents() {
        
        $("#selectType1").change(function (e) {
            e.preventDefault();
            debugger;
            var selected_dropdownindex1 = parseInt($('#selectType1').val());
            var data = {};
            data.UserList = [];

            if (selected_dropdownindex1 == -1) {
                data = usersData;
            }
            else {
                data.UserList = usersData.UserList.filter(p=> p.IsActive == Boolean(selected_dropdownindex1));
            }

            displayAllUsers(data);

        }); //End of Save Click
        
        $("#newuser").click(function (e) {
            e.preventDefault();
            clearFeilds();
            $.bsmodal.show("#modal-form");
        });

        $("#Save").unbind('click').bind('click', function (e) {
            e.preventDefault();
            
            if ($("#txtName").val().trim() == "" 
                || $('#txtEmail').val().trim() == ""
                || $('#txtLogin').val().trim() == ""
                ) {
                MyWebApp.UI.showRoasterMessage("Empty Field(s)", Enums.MessageType.Error, 2000);
                return;
            }
            else {
                $.bsmodal.hide("#modal-form");

                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: "Save",
                    bodyText: 'Do you want to Save this record?',
                    dataToPass: { 
                        UserId: $("#hiddenid").val(),
                        Name: $("#txtName").val().trim(),
                        Email: $('#txtEmail').val().trim(),
                        Login: $('#txtLogin').val().trim()
                    },
                    fnYesCallBack: function ($modalObj, dataObj) {
                        saveUser(dataObj);
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
            SaveRoleMapping();
            return false;
        });

    }
    
    //-----------------User Management + Grid Event Handling
    function saveUser(dataObj) {
        debugger;
        var userObjToSend = {
            UserId: dataObj.UserId,
            Name: dataObj.Name,
            Email: dataObj.Email,
            Login: dataObj.Login
        }

        var dataToSend = JSON.stringify(userObjToSend);
        var url = "Security/SaveUsers";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);

                var userObj = usersData.UserList.find(p => p.UserId == userObjToSend.UserId);
                
                if(userObj){
                    userObj.Name = userObjToSend.Name;
                    userObj.Description = userObjToSend.Description;
                }
                else {
                    userObjToSend.UserId = result.data.UserId;
                    userObjToSend.IsActive = true;
                    usersData.UserList.push(userObjToSend);
                }
              
                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);
            }
            $.bsmodal.hide("#modal-form");
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this User: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });

    }
    function clearFeilds() {
        
        $("#hiddenid").val("");
        $("#txtName").val("");
        $("#txtEmail").val("");
        $("#txtLogin").val("");
    }

    function EnableDisableUser(dataObj) {
        var userData = {
            UserId: dataObj.UserId,
            IsActive: !dataObj.IsActive
        }

        var dataToSend = JSON.stringify(userData);
        var url = "Security/EnableDisableUser";

        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                
                var userObj = usersData.UserList.find(p => p.UserId == userData.UserId);
                userObj.IsActive = userData.IsActive;
               
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);

                $("#selectType1").trigger("change");

            } else {
                MyWebApp.UI.showRoasterMessage('some error has occurred', Enums.MessageType.Error);                
            }
        }, function (xhr, ajaxoptions, thrownerror) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this User: "' + thrownerror + '". Please try again.', Enums.MessageType.Error);
        });
    }
    function getAllUsers() {

        var url = "Security/getUsers";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                displayAllUsers(result.data);
                usersData = result.data;

            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Users: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);

    }
    function displayAllUsers(UserList) {

        $("#simple-table").html("");

        if (!UserList)
            return;

        try {
            var source = $("#UserTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(UserList);
        } catch (e) {
            debugger;
        }
        debugger;
        $("#simple-table").append(html);
        BindGridEvents();
    }
    

    function BindGridEvents(){
        $("#simple-table .lnkEdit" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            debugger;
            HandleEditUser(id);
            return false;
        });

        $("#simple-table .lnkDelete" ).unbind('click').bind('click', function(){
            debugger;
            var id = $(this).closest('tr').attr('id');
            HandleEnableDisableUser(id);
            return false;
        });

        $("#simple-table .lnkEditMapping" ).unbind('click').bind('click', function(){
            var id = $(this).closest('tr').attr('id');
            GenerateRolesHTML(temp_roleData);
            GetRolesByUserID(id);
            
            return false;
        });
    }
    function HandleEditUser(UserId){

        if (usersData ) {
            var userObj = usersData.UserList.find(p => p.UserId == UserId);
            if(userObj){
                if(userObj.IsActive == false){
                    MyWebApp.UI.showRoasterMessage("Editing is not allowed as record is disabled", Enums.MessageType.Error);
                    return false;
                }

                $("#hiddenid").val(userObj.UserId);
                $("#txtName").val(userObj.Name);
                $("#txtEmail").val(userObj.Email);
                $("#txtLogin").val(userObj.Login);
           
                $.bsmodal.show("#modal-form");
            }//end of userObj
        }//end of usersData
    }
    function HandleEnableDisableUser(UserId){

        if (usersData ) {
            var userObj = usersData.UserList.find(p => p.UserId == UserId);
            if(userObj){
                
                var header = (userObj.IsActive == false ? "Enable" : "Disable");
                
                MyWebApp.Globals.ShowYesNoPopup({
                    headerText: header,
                    bodyText: 'Do you want to ' + header + ' this record?',
                    dataToPass: { "UserId": userObj.UserId, "IsActive" : userObj.IsActive },
                    fnYesCallBack: function ($modalObj,dataObj) {
                        EnableDisableUser(dataObj);
                        $modalObj.hideMe();
                    }
                });
            }//end of userObj
        }//end of usersData
    }

    //-------------------------- Roles/Mappings Code

    function getActiveRoles() {

        var url = "Security/getActiveRoles";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                temp_roleData = result.data;
                GenerateRolesHTML(temp_roleData);
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Roles: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }
    function GenerateRolesHTML(Roles) {
        $("#sortable1").html("");

        if (!Roles)
            return;

        try {
            var source = $("#RoleTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(Roles);

            $("#sortable1").append(html);
        }
        catch (e) {
            ////debugger;
        }
    }
    function GetRolesByUserID(pUserID){

        var url = "Security/GetRolesByUserID?pUserID=" + pUserID;
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                SelectRolesInPopupForCurrentUser(result.data.Roles,pUserID);
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Roles: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);
    }
    function SelectRolesInPopupForCurrentUser(permissionIds,pUserID){

        $("#sortable1 li").each(function(){
            var id = parseInt($(this).attr("id"));
            if( permissionIds.indexOf(id) >= 0)
                $(this).find("input[type=checkbox]").attr("checked",true);
        });

        $("#EditRolesModal").data('UserID',pUserID);
        $.bsmodal.show("#EditRolesModal",{top:"5%",left:"25%",closeid:"#closeedit,#CancelPermModal"});
    }

    function SaveRoleMapping(){
        var permList = [];
        $("#sortable1 li :checked").each(function(){
            var permId = $(this).closest('li').attr("id");
            permList.push(parseInt(permId));
        });

        var userID = $("#EditRolesModal").data('UserID');
        var dataToSend = {};
        dataToSend.UserID = userID;
        dataToSend.Roles = permList;

        dataToSend = JSON.stringify(dataToSend);
        var url = "Security/SaveUserRoleMapping";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Success, 2000);
                debugger;
                $.bsmodal.hide("#EditRolesModal");
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