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
/*jslint onevar: true, undef: true, newcap: true, regexp: true, plusplus: true, bitwise: true, devel: true, maxerr: 50 */
(function ($) {

    module('Tile tests', {
        setup: function () {
            $('#qunit-fixture').append('<div id="vehicle-list-contents"><div class="wrapper" /></div>');
        }
    });

    test('when initialized, then the mstats-tile class is added', function () {
        expect(1);
        $('#vehicle-list-contents > div').tile();
        ok($('.wrapper').hasClass('mstats-tile'), 'tile has mstats-tile class added');
    });

    test('when moveTo is called, then gets moved to specified position and callback is called', function () {
        expect(2);
        var v = $('#vehicle-list-contents > div').tile();
        v.tile('moveTo', {
            position: { top: 50, left: 50 },
            queueName: 'test queue',
            duration: 500
        }, function () {
            equal($('.wrapper').css('top'), '50px', 'top has been set');
            equal($('.wrapper').css('left'), '50px', 'left has been set');
            start();
        });
        stop();
    });

    test('when moveTo is called with an array of animation infos, then gets moved to specified position', function () {
        expect(2);
        var v = $('#vehicle-list-contents > div').tile();
        v.tile('moveTo', [{
            position: { top: 50, left: 50 },
            queueName: 'test queue',
            duration: 10
        }, {
            position: { top: 150, left: 150 },
            queueName: 'test queue',
            duration: 10
        }], function () {
            equal($('.wrapper').css('top'), '150px', 'top has been set');
            equal($('.wrapper').css('left'), '150px', 'left has been set');
            start();
        });
        stop(2000);
    });

    // unlock tiles before animating them
    // respond to a move method (that accepts a side and a value)
    // respond to a collapse method
    // respond to an expand method
    // lock tiles after animating them (callback on the step2 queue)

}(jQuery));