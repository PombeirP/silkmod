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

    var timer,
        hideAfter = 1000; // ms
        // could've been an option

    $.fn.tagger = function () {

        return this.each(function () {
            var $this = $(this),
                tagName = $this.text();

            // If we needed to store element specific data,
            // we can just do something like this
            // $this.data['tagger'] = {name: $this.text()};
            // and pull it out the same way when needed.

            $this
            .addClass('qs-tagged')
            .bind('mouseenter', function (evnt) {
                clearTimeout(timer);
                $this.trigger('activate', {
                    pageX: evnt.pageX,
                    pageY: evnt.pageY,
                    tagName: tagName
                });
            })
            .bind('mouseleave', function () {
                timer = setTimeout(function () {
                    $this.trigger('deactivate');
                }, hideAfter);
            });
        });

    };

} (jQuery));