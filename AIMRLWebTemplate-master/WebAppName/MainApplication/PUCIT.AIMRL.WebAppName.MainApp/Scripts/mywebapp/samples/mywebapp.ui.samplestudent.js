MyWebApp.namespace("UI.SampleStudent");

MyWebApp.UI.SampleStudent = (function () {
    "use strict"; 
    var _isInitialized = false; 
    var mainAutocompleteObj1 = null;
    var mainAutocompleteObj2 = null;


    function initialisePage() { 
        if (_isInitialized == false) {
            _isInitialized = true;
            ClearFields();
            BindEvents();


            //Auto Complete Initiatization

            mainAutocompleteObj1 = new JQUIAutoCompleteWrapper({
                inputSelector: "#txtMainSearch1",
                dataSource: "SamplesData/SearchSampleStudentsForAuto",
                queryString: "",
                listItemClass: ".listitem",
                searchParameterName: "prefixText",
                maxItemsToDisplay: "1",
                minCharsToTypeForSearch: "2",
                watermark: "Type First or Last Name..",
                dropdownHTML: "<a><table><tr><td>TAG_LABEL (TAG_VALUE)</td></tr></table></a>",
                fields: {
                    ValueField: 'TAG_VALUE', DisplayField: 'TAG_LABEL'
                },
                enableCache: false,
                onClear: function () {
                    
                }
                , displayTextFormat: "TAG_LABEL (TAG_VALUE)"
            });

            mainAutocompleteObj1.InitializeControl();





            mainAutocompleteObj2 = new JQUIAutoCompleteWrapper({
                inputSelector: "#txtMainSearch2",
                dataSource: "SamplesData/SearchSampleStudentsForAuto",
                queryString: "",
                listItemClass: ".listitem",
                searchParameterName: "prefixText",
                maxItemsToDisplay: "5",
                minCharsToTypeForSearch: "2",
                watermark: "Type First or Last Name..",
                dropdownHTML: "<a><table><tr><td>TAG_LABEL (TAG_VALUE)</td></tr></table></a>",
                fields: {
                    ValueField: 'TAG_VALUE', DisplayField: 'TAG_LABEL'
                },
                enableCache: false,
                isEditable: false,
                onClear: function () {

                }
               , displayTextFormat: "TAG_LABEL (TAG_VALUE)"
            });

            mainAutocompleteObj2.InitializeControl();




            $("#btnShowSelVal").click(function () {
                var data = mainAutocompleteObj1.getSelectedValues();
                alert(data[0]);
            });

            $("#btnClearSelVal").click(function () {
                if (mainAutocompleteObj1)
                    mainAutocompleteObj1.ClearSelectedItems();

            });

        }
    } 
    function BindEvents() {

        //Save Event
        $("#btnSave").click(function (e) {
            e.preventDefault();
            debugger;
            SaveStudent();
            //return false;
        }); //End of Save Click

        //Cancel Event
        $("#btnCancel").click(function (e) {
            e.preventDefault();

            ClearFields();
            return false;
        }); //End of Cancel Click

        //Save Event
        $("#btnTopSearch").click(function (e) {
            e.preventDefault();

            SearchStudents();
            //return false;
        }); //End of Save Click

        //Confirm Deactivate
        $('#btnPopConfirm').unbind('click').bind('click', function (e) {
            e.preventDefault();

            var studentid = $("#txtPopStudentId").val();
            DeactivateStudent(studentid);
            $.bsmodal.hide("#divStudentDeactivatePopUp", { closeid: "#btnPopCancel" });
            return false;
        }); //End of Confirm Deactivate

        //Datepicker
        $("#txtDateOfBirth").datepicker();

        $("#txtDateOfBirth").bind('click', function () {
            //$(this).datepicker();
            var date = $(this).val();
            //debugger;
            if (Utilities.IsValidDate(date)) {
                date = Date.parse(date).toString("MM/dd/yyyy");
                $(this).val(date);
            }
            else
                $(this).val("");
        }); // End of Filling Dates in DatePicker#txt

    }

    function SearchStudents() {
       
        $("#studentbody").html("");

        var studentId = $("#txtStudentId").val();
        var firstName = $("#txtFirstName").val();
        var lastName = $("#txtLastName").val();
        var dateOfBirth = $("#txtDateOfBirth").val();

        var searchObj = {
            StudentId: studentId,
            FirstName: firstName,
            LastName: lastName,
            DateOfBirth: dateOfBirth
        }
          
        var url = "SamplesData/SearchSampleStudents/?pStudentId=" + studentId + "&pFirstName=" + firstName + "&pLastName=" + lastName + "&pDateOfBirth=" + dateOfBirth;

        MyWebApp.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.success === true) {
                console.log(result);
                DisplayStudentData(result.data); 
                
            } else {
                MyWebApp.UI.showRoasterMessage(result.error, Enums.MessageType.Error);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showRoasterMessage('A problem has occurred while getting students: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
        });

    }

    function DisplayStudentData(students) {

        $("#studentbody").html("");

        if (!students)
            return;

        var source = $("#studentrowtemplate").html();
        var template = Handlebars.compile(source);

        var html = template(students);
        $("#studentbody").append(html);

        BindAndFormat();
       
    }

    function BindAndFormat() {
         
        //Deactivate event
        $("#alldatacontainer a.delete").unbind("click").bind("click", function (e) {
            e.preventDefault();
            var studentid = $(this).closest("tr").find(".studentids").text();
            if (studentid != "") {
                $.bsmodal.show("#divStudentDeactivatePopUp", { closeid: "#btnPopCancel" });

                $("#txtPopStudentId").val(studentid);
            }
            else {
                $(this).closest("tr").remove();
            }
        });

         
        //Edit event
        $("#alldatacontainer a.edit").unbind("click").bind("click", function (e) {
            e.preventDefault();

             
            var row = $(this).closest("tr");

            var firstname = row.find(".fname").text();
            var lastname = row.find(".lname").text();
            var dateofbirth = row.find(".dob").text(); 
            var isactive = row.find(".iactive").val();
            var studentId = row.find(".studentids").text();

            $("#txtFirstName").val(firstname);
            $("#txtLastName").val(lastname);
            $("#txtDateOfBirth").val(dateofbirth);
            //$("#chkIsActive").prop("checked", isactive); 
            $("#txtStudentId").val(studentId);
        });

        //Format handle bar columns
        $("#alldatacontainer tr.datarows").each(function () {

            //Format date 
            var date = $(this).find(".dob").text(); 
            if (Utilities.IsValidDate(date)) {
                date = Date.parse(date).toString("MM/dd/yyyy");
                $(this).find(".dob").text(date);
            }
            else
                $(this).text("");

            //Disable Checkbox   
            var checkbox = $(this).find(".iactive");
            checkbox.attr("disabled", true);
            if (checkbox.val()) {
                checkbox.attr("checked", true);
            }
            else
                $(this).find(".tblActions").removeClass();
             
        }) 
    }

    function ValidateForm($trForTLA) {

        var isValid = true;
        var firstname = $("#txtFirstName").val();
        var lastname = $("#txtLastName").val();
        var dateofbirth = $("#txtDateOfBirth").val();
        var gender = $("#rdbMale:checked").val() || $("#rdbFemale:checked").val();
        var education = $("#ddlEducation").val();

        if (firstname == '' || lastname == '' || dateofbirth == '' || gender == '' || education == '') {
            MyWebApp.UI.showRoasterMessage("Please Enter values in all input controls.", Enums.MessageType.Warning);
            isValid = false;
        }

        if ($("#txtDateOfBirth").val() >= Date.today().toString("MM/dd/yyyy")) {
            MyWebApp.UI.showRoasterMessage("Please enter a valid date.", Enums.MessageType.Warning);
            isValid = false;
        }

        return isValid;
    }

    function DeactivateStudent(studentid) { 
        debugger;
        var pStudentID = {
            StudentID: studentid
        };
        var dataToSend = JSON.stringify(pStudentID);
        var url = "SamplesData/DeactivateSampleStudent";
        MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

            if (result.success === true) {
                MyWebApp.UI.showMessage("#spstatus", 'Student has been deactivated successfully', Enums.MessageType.Success);
                ClearFields();
                SearchStudents();
                
                return true;
            } else {
                MyWebApp.UI.showMessage("#spstatus", result.error, Enums.MessageType.Error);
                return false;
            }
        }, function (xhr, ajaxOptions, thrownError) {
            MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while deleting this Student: "' + thrownError + '". Please try again.', Enums.MessageType.Error);
            return false;
        }); 
    }
     
    function SaveStudent() { 
        if (ValidateForm()) {

            var studentobj = {
                StudentId: $("#txtStudentId").val(),
                FirstName: $("#txtFirstName").val(),
                LastName: $("#txtLastName").val(),
                DateOfBirth: $("#txtDateOfBirth").val(),
                Gender : $("#rdbMale:checked").val() || $("#rdbFemale:checked").val(),
                Education: $("#ddlEducation").val()
                //IsActive: $("#chkIsActive").is(":checked"), 
            }
             
            var dataToSend = JSON.stringify(studentobj);
            var url = "SamplesData/SaveSampleStudent";
                MyWebApp.Globals.MakeAjaxCall("POST", url, dataToSend, function (result) {

                    if (result.success === true) {
                        MyWebApp.UI.showMessage("#spstatus", 'Student has been saved successfully', Enums.MessageType.Success);
                        ClearFields();
                        SearchStudents();
                    } else {
                        MyWebApp.UI.showMessage("#spstatus", result.error, Enums.MessageType.Error);
                    }
                }, function (xhr, ajaxoptions, thrownerror) {
                    MyWebApp.UI.showMessage("#spstatus", 'A problem has occurred while saving this Student: "' + thrownerror + '". please try again.', Enums.MessageType.Error);
                });
        }
    }

    function ClearFields() {
        $("#txtStudentId").val("");
        $("#txtFirstName").val("").watermark("Enter First Name");
        $("#txtLastName").val("").watermark("Enter Last Name");
        $("#txtDateOfBirth").val("").watermark("Enter Date of Birth");
        $("#rdbMale").attr("checked", true);
        $("#ddlEducation").val(1);

        //$("#chkIsActive").prop("checked", false)

        //$(".DatePicker").each(function () {
        //    $(this).datepicker(); 
        //});  

    }  // end of ClearFields

    return {
        
        readyMain: function () { 
            initialisePage();
        }
    };
}
 ());
