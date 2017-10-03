
MyWebApp.namespace("Resources");

MyWebApp.Resources = (function () {
    "use strict";

    var RoleType = {
        None : 0
    }

    return {
        Views: {
            LoginURL: MyWebApp.Globals.baseURL + "Login",
            HelpURL: MyWebApp.Globals.baseURL + "help/main",
        },
        Images: {
            CloseImageURL: MyWebApp.Globals.baseURL + "content/autoComplete/images/close_icon.gif",
            ExpImageURL: MyWebApp.Globals.baseURL + "content/images/img-expand.gif",
            ColapseImageURL: MyWebApp.Globals.baseURL + "content/images/img-collapse.gif",
            ContentFolderImages: MyWebApp.Globals.baseURL + "Images/Content/images/",
            SignaturePath: MyWebApp.Globals.baseURL + "UploadedFiles/",
        },
        FilePath:{
            SWFPath: MyWebApp.Globals.baseURL + "Scripts/jquery-plugins/DataTables/media/swf/copy_csv_xls_pdf.swf"
        },
        TemplatePath:{
            TemplateImportEmployee: MyWebApp.Globals.baseURL + "ExcelTemplates/TemplateImportEmployee/TemplateImportEmployee.xls"
        },
        CurrentUserInfo: {
                LoginId: ''
        }
     };
} ());

