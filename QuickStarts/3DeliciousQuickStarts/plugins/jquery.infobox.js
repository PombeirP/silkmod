//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
(function ($, undefined) {

    var target,
        offsetX = 20,
        offsetY = 20,
        mouseOverBox = false,
        leftSideAdjustment = -270;

    $.fn.infobox = function(options){

        this.options = { 
            maxItems: 10,
            dataUrl: 'http://feeds.delicious.com/v2/json/popular/'
        };

        this.element = target = target || $('<div class="qs-infobox" />');

        var self = this,

        methods = {

            displayTagLinks: function (info) {
                var i,
                    html,
                    elem = self.element,
                    options = self.options,
                    tag = $.trim(info.tagName),
                    top = info.pageY + offsetY,
                    left = info.pageX + offsetX,
                    url = options.dataUrl + tag + '?count=' + options.maxItems,
                    displayResult = function () {
                        elem.html(html);
                        elem.css({top: top, left: left});
                        elem.show();
                    };

                if (event.pageX > window.screenWidth / 2) {
                    left = event.pageX + leftSideAdjustment; 
                }

                $.ajax({
                    url: url,
                    dataType: 'jsonp',
                    success: function (data) {
                        if (data != null) {
                            html = '<h1>Popular Links for ' + tag + '</h1><ul>';
                            for (i = 0; i < data.length - 1; i += 1) {
                                html += '<li><a href="' + data[i].u + '" target="_blank">' + data[i].d + '</a></li>';
                            }
                            html += '</ul>';
                        } else {
                            html = '<h1>Data Error</h1><p>The AJAX call returned null. This happens when using non-Internet Explorer browsers and is the expected behavior when viewing file based HTML files.</p><p>Please use Internet Explorer 9 and click the "Allow Blocked Content" button to run this QuickStart.</p>';
                        }
                        displayResult();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        html = '<h1>AJAX Error</h1><p>The AJAX call returned the following error: ' + jqXHR.statusText + '.</p>';
                        displayResult();
                    }
                });
            },

            hideTagLinks: function () {
                if (!mouseOverBox) {
                    self.element.hide();
                }
            }

        }, // methods

        // this supports the jQuery method invoking our methods
        callMethod = function(name) {
            if(methods[name] && $.isFunction(methods[name])) {
                return methods[name].apply( self, Array.prototype.slice.call( arguments, 1 ));
            }   
        };

        // this function is either going to pass in
        // nothing: merging with options won't hurt anything
        // options: and they will be merged with the defaults
        // method name: invoke it with its arguments
        if(typeof options === 'string') {
            callMethod.apply(self, arguments);
            return;
        } else { // merge with defaults
            $.extend(this.options, options);
        }

        self.element.appendTo(this)
        .hover(
            function () { // mouseenter
                mouseOverBox = true;
            },
            function () { // mouseleave
                mouseOverBox = false;
                methods.hideTagLinks();
        });

        // support chaining
        return this;
    };

}(jQuery));
