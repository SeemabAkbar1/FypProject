//For watermark http://code.google.com/p/jquery-ui/
//http: //forums.asp.net/p/1682457/4426122.aspx/1?Re+JQuery+watermark+plugin+conflicts+with+autocomplete

function JQUIAutoCompleteWrapper(options) {

    //debugger;
    var defaults = {
        inputSelector: '',
        dataSource: '', //Main Controller Path
        queryString: '', //Query String Parameter Other than typed Text
        listItemClass: 'listitem',
        searchParameterName: 'prefixText',
        maxItemsToDisplay: 1,
        closeImageUrl: MyWebApp.Resources.Images.CloseImageURL,
        minCharsToTypeForSearch: 1,
        watermark: '',
        dropdownHTML: "<a>TAG_LABEL</a>",
        enableCache: false,
        prepopulatedList: '[]',
        itemOnClick: null,
        isEditable: true,
        fields: {
            ValueField: 'TAG_VALUE',
            DisplayField: 'TAG_LABEL',
            DescriptionField: 'TAG_DESCRIPTION',
            ImageField: 'TAG_IMAGE',
            URLField: 'TAG_URL',
            AdditionalField: 'TAG_ADDITIONAL'
        },
        onClear: function() {

        },
        /*When user wants to close some item either by clicking cross button
        or by backspance, this function will be called.
        */
        beforeItemRemoving: function(itemObj, $ObjToClose, fnToCloseItem) {
            fnToCloseItem($ObjToClose);
        },
        appendTo: null,
        displayTextFormat: "TAG_LABEL",
        searchInValueField: false
};

    var thisObj = this;
    var opts = $.extend(defaults, options);
    var cache = {}, terms = [];
    var browser_msie = $.browser.msie;
    var browser_chrome = /chrome/.test(navigator.userAgent.toLowerCase());

    var $inputContainer = $("<div></div>").addClass("listcontainer");
    var $inputControl = $(opts.inputSelector);
    $(opts.inputSelector).wrap($inputContainer);

    this.InitializeControl = function () {
        $inputContainer = $inputControl.closest("div.listcontainer");

        $inputContainer.click(function () {
            $inputControl.focus();
            return false;
        });

        $inputContainer.closest(".maincontainer").click(function () {
            $inputControl.focus();
            return false;
        });

        if ($.watermark) {
            $inputControl.watermark(opts.watermark);
        }
        $inputContainer.addClass("multiselect");

        //        if (browser_msie) {
        //            thisObj.fixPlaceholder();
        //            $inputControl.focus(function (event) {
        //                $(this).css("color", "#000");
        //                return false;
        //            });
        //        }

        if (browser_msie || browser_chrome) {

            $inputControl.unbind("keydown").bind("keydown", function (event) {
                HandleInputBoxKeyboardEvents(event);
            });
        }

        $inputControl.unbind("keypress").bind("keypress", function (event) {
            HandleInputBoxKeyboardEvents(event);
        });

        function HandleInputBoxKeyboardEvents(event) {

            var $this = $inputControl;
            if (event.which != 8 && event.which != 0 && event.which != 13) {
                var regex = new RegExp("^[a-zA-Z0-9 ]+$");
                var key = String.fromCharCode(event.which);
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            }

            //If Escape key is pressed
            if (event.which == 46 || event.which == 8) {

                if (opts.isEditable) {
                    if (thisObj.getSelectedValues().lenght > 0) {
                        $inputControl.val('').data("data", null);
                        thisObj.BoxIsCleared();
                    }
                    else {
                        $inputControl.val('').data("data", null);
                    }
                    return false;
                }

                var txtval = $inputControl.val();
                if (txtval == '') {
                    var $itemToClose = $inputContainer.find(".listitem:last");
                    var itemObj = $itemToClose.data("data");
                    opts.beforeItemRemoving(itemObj, $itemToClose, thisObj.CloseItem);

                    return false;
                } // if (txtval == '')
            } // End of if (event.which == 46 

            if ($inputContainer.find(".listitem").length == opts.maxItemsToDisplay) {
                $inputControl.val('').data("data", null);
                event.stopPropagation();
                event.preventDefault();
                return false;
            }
        } //End of HandleInputBoxKeyboardEvents

        $inputContainer.parent().unbind("click");
        $inputContainer.parent().bind("click", function (event) {
            $inputControl.focus();
            return false;
        });

        
        var $autoComplOpt = {
            minLength: opts.minCharsToTypeForSearch,
            source: function (request, response) {
                


                if (typeof opts.dataSource === 'function') {
                    debugger;
                    var result = opts.dataSource();

                    var matcher = new RegExp("" + $.ui.autocomplete.escapeRegex(request.term), "ig");
                    response($.grep(result, function (item) {
                        debugger;

                        var result =  matcher.test(item[opts.fields.DisplayField]);
                        
                        if (opts.searchInValueField) {
                            var r = matcher.test(item[opts.fields.ValueField]);
                            return (r || result);
                        }
                        return result;
                    }));
                    return;
                }
                var foundInCache = false;

                if (opts.enableCache === true) {
                    if (request.term in cache) {
                        var oldata = cache[request.term];
                        response(oldata);
                        foundInCache = true;
                    }
                }
                if (foundInCache == false) {
                    var u = opts.dataSource + "?" + opts.searchParameterName + "=" + request.term;

                    var qryString = "";
                    if (typeof opts.queryString === 'function') {
                        qryString = opts.queryString();
                    }
                    else {
                        qryString = opts.queryString;
                    }

                    if (qryString != "")
                        u = u + "&" + qryString;

                    u = decodeURIComponent(u);

                    MyWebApp.Globals.MakeAjaxCall("GET", u, "{}",
                        function (data) {
                            if (opts.enableCache === true) {
                                cache[request.term] = data;
                            }
                            response(data);
                        });
                }
            },
            change: function (event, ui) {
                $inputControl.autocomplete("search", "");

                if (!ui.item) {
                    // remove invalid value, as it didn't match anything
                    $inputControl.val("").data("data", null);
                    return false;
                }
            },
            focus: function (event, ui) {
                //$inputControl.focus();
                return false;
            },
            select: function (event, ui) {
                thisObj.clearDuplicateClass();
                if (thisObj.checkDuplicates(ui.item) == false) {

                    if (opts.itemOnClick) {
                        //debugger;
                        opts.itemOnClick(ui.item);
                    }
                    thisObj.addSelected(ui.item);
                } else {
                    $inputControl.val('').data("data", null);
                    thisObj.BoxIsCleared();
                }
                $inputControl.focus();
                return false;
            },
            search: function () {
                //$inputControl.addClass("msgLoading");
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                //$inputControl.removeClass("msgLoading");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                //$inputControl.removeClass("msgLoading");
            }
        };

        if (opts.appendTo)
            $autoComplOpt.appendTo = appendTo;

        var $autoCompObject = $inputControl.autocomplete($autoComplOpt);

        $autoCompObject.data("autocomplete")._renderItem = function (ul, item) {
            var htmltext = opts.dropdownHTML;

            htmltext = thisObj.replaceElementsInFormat(item, htmltext);

            //if (typeof item[opts.fields.DisplayField] !== "undefined")
            //    htmltext = htmltext.replace(new RegExp(opts.fields.DisplayField, 'g'), item[opts.fields.DisplayField]);
            //if (typeof item[opts.fields.ValueField] !== "undefined")
            //    htmltext = htmltext.replace(new RegExp(opts.fields.ValueField, 'g'), item[opts.fields.ValueField]);
            //if (typeof item[opts.fields.DescriptionField] !== "undefined")
            //    htmltext = htmltext.replace(new RegExp(opts.fields.DescriptionField, 'g'), item[opts.fields.DescriptionField]);
            //if (typeof item[opts.fields.ImageField] !== "undefined")
            //    htmltext = htmltext.replace(new RegExp(opts.fields.ImageField, 'g'), item[opts.fields.ImageField]);
            //if (typeof item[opts.fields.URLField] !== "undefined")
            //    htmltext = htmltext.replace(new RegExp(opts.fields.URLField, 'g'), item[opts.fields.URLField]);

            var $li = $("<li></li>")
            .css("cursor", "pointer")
            .addClass("dropdownitem")//css("background-color", "#fff").css("border-bottom", "1px dotted Gray").css("width","100px")
			.data("item.autocomplete", item)
			.append(htmltext)
			.appendTo(ul.addClass("dropdown"));
            //.css("background-color", "#fff").css("border", "1px solid Gray").css("list-style", "none").css("padding-left", "0px"));
            //debugger;
            $li.click(function () {

                //if (opts.itemOnClick) {
                //    debugger;
                //    opts.itemOnClick(item);
                //}
                $inputControl.focus();
            });

            return $li;
        }; //End of _renderItem function

        $autoCompObject.data("autocomplete")._resizeMenu = function () {
            //var ul = this.menu.element;
            //ul.outerWidth(this.element.outerWidth());
            var ow = $inputControl.outerWidth();
            var ul = this.menu.element;
            ul.outerWidth(ow);

        } // End of _resizeMenu

        //$inputContainer.find("");
        thisObj.setSelected();
    };

    this.replaceElementsInFormat = function (item, templateText) {

        if (typeof item[opts.fields.DisplayField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.DisplayField, 'g'), item[opts.fields.DisplayField]);
        if (typeof item[opts.fields.ValueField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.ValueField, 'g'), item[opts.fields.ValueField]);
        if (typeof item[opts.fields.DescriptionField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.DescriptionField, 'g'), item[opts.fields.DescriptionField]);
        if (typeof item[opts.fields.ImageField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.ImageField, 'g'), item[opts.fields.ImageField]);
        if (typeof item[opts.fields.URLField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.URLField, 'g'), item[opts.fields.URLField]);
        if (typeof item[opts.fields.AdditionalField] !== "undefined")
            templateText = templateText.replace(new RegExp(opts.fields.AdditionalField, 'g'), item[opts.fields.AdditionalField]);

        return templateText;

    },
    //Assign to those input elements that have 'placeholder' attribute
    this.fixPlaceholder = function () {

        var $input = $inputControl;
        $input.css("color", "#c0c0c0");
        //$input.val(opts.watermark);
        $input.focus(function () {
            if ($input.val() == opts.watermark) {
                $input.val('');
                $input.css("color", "#000");
                //alert(input.css("color"));
            }
        });

        $input.blur(function () {
            if (($input.val() == '' || $input.val() == opts.watermark) && ($inputContainer.find(".listitem").length == 0)) { //< opts.maxItemsToDisplay)) {
                //$input.val(opts.watermark);
                $input.css("color", "#c0c0c0");
            }
        });
    }; //end fixPlaceholder

    this.setSelected = function () {
        var temp = eval(opts.prepopulatedList);
        $inputContainer.find(".listitem").each((function () {
            $(this).remove();
        }));
        if (temp != null) {
            if (temp.length >= 0) {
                for (var i = 0; i < temp.length; i++) {
                    thisObj.addSelected(temp[i]);
                }
            }
        }
    } //end of setSelected

    this.addSelected = function (item) {

        var txtToDisp = "";
        var htmltext = opts.displayTextFormat;

        txtToDisp = this.replaceElementsInFormat(item, htmltext);

        //var txtToDisp = item[opts.fields.DisplayField];

        //var txtFormat = opts.displayTextFormat;

        //if (item[opts.fields.DisplayField] != "" && opts.displayValue == true)
        //    txtToDisp = txtToDisp + " [EmpID:" + item[opts.fields.ValueField] + "]";

        if (opts.isEditable) {
            //debugger;
            $inputControl.val(txtToDisp).removeAttr("placeholder").focus().data("data", item);
            return;
        }

        $inputControl.removeAttr("placeholder");

        var d = $("<div></div>").addClass("listitem").data("data", item);
        if (opts.maxItemsToDisplay == 1) {
            d.css('border-style', 'hidden');
            d.css('background-color', 'White');
            d.css('margin-top', '5px');
            d.css('padding-right', '5px');
        }

        var x = $("<span></span>").text(txtToDisp);
        d.append(x);

        var closeimg = $("<img></img>").attr("src", opts.closeImageUrl).attr("class", "closebtn");
        var close = $("<a></a>").attr("href", "#");
        close.append(closeimg);
        close.click(function (event) {
            //var obj = $(this).parent().data("data");
            var $itemToClose = $(this).closest("div.listitem");
            var itemObj = $itemToClose.data("data");
            opts.beforeItemRemoving(itemObj, $itemToClose, thisObj.CloseItem);

            return false;
        });
        if (opts.maxItemsToDisplay > 1) {
            d.append(close);
        }
        $inputControl.before(d);
        $inputControl.val('').data("data", null);

    }     // end of addSelected

    //For Internal Use
    this.CloseItem = function ($ObjToClose) {

        $ObjToClose.remove();

        if ($inputContainer.find(".listitem").length < opts.maxItemsToDisplay) {
            $inputControl.val('').data("data", null);
            if ($inputContainer.find(".listitem").length == 0) {
                thisObj.BoxIsCleared();
                if (browser_msie) {
                    thisObj.fixPlaceholder();
                }
            }
        }
    }

    this.getSelectedObjects = function () {
        var arrayObject = new Array();

        if (opts.isEditable) {

            var da = $inputControl.data("data")
            if (da) {
                arrayObject.push(da);
            }
        }
        else {
            $inputContainer.find(".listitem").each((function () {
                //debugger;
                var d = $(this).data("data");
                arrayObject.push(d);
            }));
        }
        return arrayObject;
    }         //end of getSelectedValues

    this.getSelectedValues = function () {
        //debugger;
        var arrayObject = new Array();

        if (opts.isEditable) {
            var d = $inputControl.data("data");
            if (d) {
                var v = d[opts.fields.ValueField];
                arrayObject.push(v);
            }
        }
        else {
            $inputContainer.find(".listitem").each((function () {
                var v = $(this).data("data")[opts.fields.ValueField];
                arrayObject.push(v);
            }));
        }
        return arrayObject;

    }        //end of getSelectedValuesArray

    this.checkDuplicates = function (item) {
        var isDuplicate = false;
        //debugger;
        $inputContainer.find(".listitem").each(function () {
            var d = $(this).data("data");
            if (d) {
                if (d[opts.fields.ValueField] == item[opts.fields.ValueField]) {
                    $(this).addClass('listitemduplicate');
                    isDuplicate = true;
                    return false;
                }
            }
        }); // End of Each
        return isDuplicate;
    }      // end of checkDuplicates

    this.clearDuplicateClass = function (item) {
        var arrayObject = new Array();
        $inputContainer.find(".listitem").each((function () {
            $(this).removeClass('listitemduplicate');
        }));
        return false;
    } //end of clearDuplicateClass

    this.ClearSelectedItems = function () {

        if (opts.isEditable) {
            $inputControl.val('').data("data", null);
        }
        else {
            $inputContainer.find(".listitem").each((function () {
                $(this).remove();
            }));
        }

        var e = jQuery.Event("keydown");
        e.which = 8; // Escape, so that watermark will be shown
        $inputControl.trigger(e);
    }

    this.SetOption = function (option, value) {
        if (opts.hasOwnProperty(option)) {
            opts[option] = value;
        }
    };
    this.ChangeWaterMark = function (newText) {
        opts.watermark = newText;
        if ($.watermark && $inputControl) {
            $inputControl.watermark(newText);
        }
    }
    this.BoxIsCleared = function () {
        if (opts.onClear)
            opts.onClear();
    }
} //end of CreateAutoCompleteUI