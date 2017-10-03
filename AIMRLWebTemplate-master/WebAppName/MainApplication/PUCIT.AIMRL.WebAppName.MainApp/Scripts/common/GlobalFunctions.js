



function customEncode(query) {

    //debugger;
    var sRegExInput = new RegExp("/", "g");
    query = query.replace(sRegExInput, "@");
    return query;
}

function customDecode(query) {
    //debugger;
    var sRegExInput = new RegExp("@", "g");
    query = query.replace(sRegExInput, "/");
    return query;
}