/*** 
* Used for defining the MyWebApp debugger
* @module Debugger
* @namespace MyWebApp
*/


MyWebApp.namespace("Debugger");

MyWebApp.Debugger = (function () {
    "use strict";
    return {
        log: function (message) {
            try {
                if(console)
                    console.log(message);
            } catch (exception) {
                return;
            }
        }
    };
} ());

