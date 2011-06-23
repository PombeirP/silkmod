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
(function (qs, $) {

    var target,
        offsetX = 20,
        offsetY = 20,
        mouseOverBox = false,
        leftSideAdjustment = -270,
        // move this method to inside displayTagLinks
        displayResult = function (elem, html, top, left) {
            elem.html(html);
            elem.css({top: top, left: left});
            elem.show();
        };

    qs.infobox = {

        options: {
            maxItems: 10,
            dataUrl: 'http://feeds.delicious.com/v2/json/popular/'
        },

        element: target = target || $('<div class="qs-infobox" />'),

        init: function (overrides) {
            var self = this,
                elem = self.element;
            $.extend(self.options, overrides);
            $('body').append(elem);
            elem.hover(
                function () { // mouseenter
                    mouseOverBox = true;
                },
                function () { // mouseleave
                    mouseOverBox = false;
                    self.hideTagLinks();
                }
            );
            return this;
        },

        displayTagLinks: function (tag, event) {
            var i,
                html,
                elem = this.element,
                options = this.options,
                top = event.pageY + offsetY,
                left = event.pageX + offsetX,
                url = options.dataUrl + tag + '?count=' + options.maxItems;

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
                    displayResult(elem, html, top, left);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    html = '<h1>AJAX Error</h1><p>The AJAX call returned the following error: ' + jqXHR.statusText + '.</p>';
                    displayResult(elem, html, top, left);
                }
            });
        },

        hideTagLinks: function () {
            if (!mouseOverBox) {
                this.element.hide();
            }
        }

    };

} (this.qs = this.qs || {}, jQuery));