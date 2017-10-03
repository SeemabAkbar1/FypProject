//Step 1: function add in jQuery
//Step 2: Default Settings
//Step 3: this.each
//Step 4: Write your plugin code

(function ($) {

    $.fn.bsmodal = function (options) {

        var defaults = {
            top: "25%",
            left: "35%",
            width: 200,
            closeid: '#btnclose',
            openid: '#btnopen',
            saveid: '#btnsave',
            height: 200,
            title: 'BS Modal',
            closeText: 'Close Me',
            saveText: 'Save Me',
            overlayClass: 'modaloverlay',
            SaveParams: {},
            CloseParams: {},
            OpenParams: {},
            onOpen: function () {
                //alert('open'); 
            },
            onclose: function () {
                //alert('closing');
            },
            onsave: function () {
                //alert('saving'); 
            }
        };

        var opt = $.extend(defaults, options);

        return this.each(function () {

            var $obj = $(this);

            $obj.click(function () {

                if (typeof opt.onOpen != 'undefined')
                    opt.onOpen(opt.OpenParams);
                //                debugger;
                var $over = {};
                if ($("#bsmodaloverlay").length == 0) {
                    $over = $("<div id='bsmodaloverlay'></div>").addClass(opt.overlayClass);
                    $("body").append($over);
                    $over.show();
                }
                else {
                    $over = $("#bsmodaloverlay");
                }

                var popupdiv_selector = $obj.attr("popupdiv");
                var $div = $(popupdiv_selector);
                                
                $div.addClass("popup-box");

                $div.fadeIn("slow");
                //debugger;
                $div.css({ "top": opt.top, "left": opt.left });

                //commented on 9th April 2013.
                //$div.appendTo("body");

                var $close = $(opt.closeid);
                $close.unbind('click');
                $close.click(function () {
                    //$div.fadeOut("slow").removeClass("popup-box");
                    $div.removeClass("popup-box");
                    $div.hide();
                    $over.remove();

                    if (opt.onclose != undefined)
                        opt.onclose(opt.CloseParams);
                    return false;
                }); //End of Close

                var $save = $(opt.saveid);
                $save.unbind('click');
                $save.click(function () {
                    $div.removeClass("popup-box");
                    $div.hide();
                    $over.remove();

                    if (opt.onsave != undefined)
                        opt.onsave(opt.SaveParams);
                    return false;
                }); //End of Close
                return false;
            }); //End of hyperlink clicked           

        }); //End of Each

    }; //End of plugin

    $.bsmodal = {};

    $.bsmodal.show = function (selector, options) {
        $("<a popupdiv='" + selector + "' />").bsmodal(options).trigger("click");
    }

    $.bsmodal.hide = function (selector) {

        $(selector).hide();
        $("#bsmodaloverlay").remove();
    }

})(jQuery);


