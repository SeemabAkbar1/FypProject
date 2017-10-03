/*** 
* Used for defining the MyWebApp namespace
* @namespace MyWebApp
*/

var MyWebApp = MyWebApp || {};

if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}

if (typeof Array.prototype.forEach !== 'function') {
    Array.prototype.forEach = function (fn, scope) {
        'use strict';
        var i, len;
        for (i = 0, len = this.length; i < len; ++i) {
            if (i in this) {
                fn.call(scope, this[i], i, this);
            }
        }
    };
}

MyWebApp.namespace = function (ns_string) {
    "use strict";
    var parts = ns_string.split('.'),
        parent = MyWebApp,
        i;

    // strip redundant leading global
    if (parts[0] === "MyWebApp") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        // create a property if it doesn't exist
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};