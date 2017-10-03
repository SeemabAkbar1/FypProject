/// <reference path="../../_references.js" />

/*** 
* Used for defining the MyWebApp UI Main
* @module Main
* @namespace MyWebApp.UI
*/

MyWebApp.namespace("UI.Main");

MyWebApp.UI.Main = (function () {
    "use strict";

    function initialisePage() {

        $('#imgUserImage').unbind("click").bind("click", function () {
            $("#overlay").fadeToggle();
        });
        
    } // End of initialiseControls


    function SetToolTip(block) {
        block.tooltip({
            track: true,
            delay: 0,
            showURL: false,
            showBody: " - "
            //fade: 250
        });
    }//end of SetToolTip

    return {
        readyMain: function () {
            initialisePage();
        }
    };

} ());

