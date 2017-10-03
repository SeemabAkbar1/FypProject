MyWebApp.namespace("UI.Reports");

MyWebApp.UI.Reports = (function () {
    "use strict";
    var _isInitialized = false;
    var StatusList;
    var ApproverList;
    var StudentList;
    function initialisePage() {
        if (_isInitialized == false) {
            _isInitialized = true;
            BindEvents();
            debugger
            if($("#PageId").text()=="1")
            {
                viewAllApplicationStatuses();
            }
            if ($("#PageId").text() == "2") {
                viewAllLoginHistory();
            }
           
          
        }
    }
    function separatedata(){
        debugger
        ApproverList=  JSON.parse(JSON.stringify(StatusList));
        StudentList = JSON.parse(JSON.stringify(StatusList));
       

       
        for (var i = StatusList.StatusList.length - 1; i >= 0; i--) {
            if (StatusList.StatusList[i].Role === "Approver") {
                StudentList.StatusList.splice(i, 1);
            }
        }
        for (var i = StatusList.StatusList.length - 1; i >= 0; i--) {
            if (StatusList.StatusList[i].Role === "Student") {
                ApproverList.StatusList.splice(i, 1);
            }
        }
  
    }
 
    function showlist(clicked_item_ID)
    {
      separatedata();
        if(clicked_item_ID==0)
        {
            displayAllAppStatuses(StatusList)

        }
        else if(clicked_item_ID==1)
        {
            displayAllAppStatuses(ApproverList)
        }
        else if(clicked_item_ID==2)
        {
            displayAllAppStatuses(StudentList)
        }
    }
    function BindEvents() {
        $("#selectType").change(function (e) {
            e.preventDefault();
            debugger;
          
            var selected_dropdownindex= $('#selectType').find(":selected").attr('id');
            showlist(selected_dropdownindex);
           
        }); //End of Save Click
      
    }

    function  viewAllApplicationStatuses() {

        var url = "Reports/GetAppStatuses";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                debugger;
                console.log(result);
                StatusList = result.data;
                displayAllAppStatuses(StatusList);
             
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Report "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        },false);

    }
   
    function displayAllAppStatuses(StatusList) {

        debugger;

        $("#AppStatuses_table").html("");

        if (!StatusList)
            return;

        try {
            var source = $("#ApplicationStatusesTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(StatusList);
        } catch (e) {
            debugger;
        }


        $("#AppStatuses_table").append(html);
    }
   
    function viewAllLoginHistory() {

        var url = "Reports/getLoginHistory";
        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                debugger;
                console.log(result);
                displayAllLoginHistory(result.data);
                loginhistorydata = result.data;


            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting Login History: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        }, false);

    }

    function displayAllLoginHistory(LoginHistoryList) {

        debugger;

        $("#simple-table6").html("");

        if (!LoginHistoryList)
            return;

        try {
            var source = $("#LoginHistoryTemplate").html();
            var template = Handlebars.compile(source);
            var html = template(LoginHistoryList);
        } catch (e) {
            debugger;
        }


        $("#simple-table6").append(html);
    }


    return {

        readyMain: function () {
            debugger;
            initialisePage();

        }
    };
}
());