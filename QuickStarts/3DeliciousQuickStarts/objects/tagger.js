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

    var timer,
        hideAfter = 1000; // ms
        // could've been an option

    qs.tagger = {

        options: {
            offsetY: 20,
            offsetX: 20,
            infobox: null // could've been hardcoded in this 
                          // file or passed into init
        },

        init: function (selector, overrides) {

            var tags = $(selector),
                options = $.extend(this.options, overrides),
                infobox = options.infobox;

            // This example leans to the trivial side. If we
            // need to store element specific data, which is
            // common, we could do something similar to this
            //
            // tags.each(function() {
            //     var $this = $(this);
            //     $this.data['tagger'] = {name: $this.text()};
            // });
            //
            // so it can be pulled out when needed. But here, we
            // only need to add a class and some hover handlers.

            tags
            .addClass('qs-tagged')
            .hover(
                function(event) { // mouseenter
                    clearTimeout(timer);
                    if(options.infobox) {
                        infobox.displayTagLinks($(this).text(), event);
                    }
                },
                function() { // mouseleave
                    timer = setTimeout(function () {
                        infobox.hideTagLinks();
                    }, hideAfter);
                }
            );
        },

    };

} (this.qs = this.qs || {}, jQuery));