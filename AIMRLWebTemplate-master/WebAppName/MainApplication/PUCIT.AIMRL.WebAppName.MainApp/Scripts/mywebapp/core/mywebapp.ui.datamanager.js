
MyWebApp.namespace("UI.DataManager");

MyWebApp.UI.DataManager = (function () {
    "use strict";
    var DataArray = {};

    //Public static Members
    return {

        initialisePage: function () {
            MyWebApp.UI.DataManager.LocalDataArray = {};
            MyWebApp.UI.DataManager.SessionDataArray = {};
        },
        LocalDataArray:{}
        ,SessionDataArray: {}
        , GetLocalData: function(key) {
            if (localStorage != undefined)
            {
                return eval(localStorage.getItem(key));
            }
            else
            {
                return LocalDataArray[key];
            }
        }, SetLocalData: function(key, dataValue) {
            if (localStorage != undefined)
            {
                return localStorage.setItem(key, dataValue);
            }
            else
            {
                LocalDataArray[key] = dataValue;
            }
        } , GetSessionData: function(key) {
            if (sessionStorage != undefined)
            {
                return eval(sessionStorage.getItem(key));
            }
            else
            {
                return SessionDataArray[key];
            }
        }, SetSessionData: function(key, dataValue) {
            if (sessionStorage != undefined)
            {
                return sessionStorage.setItem(key, dataValue);
            }
            else
            {
                SessionDataArray[key] = dataValue;
            }
        }, RemoveSessionData: function (key) {
            if (sessionStorage != undefined) {
                sessionStorage.removeItem(key);
            }
            else {
                SessionDataArray.removeItem(key);
            }
        }, RemoveLocalData: function (key) {
            if (localStorage != undefined) {
                localStorage.removeItem(key);
            }
            else {
                LocalDataArray.removeItem(key);
            }
        }
        
        };
}());

