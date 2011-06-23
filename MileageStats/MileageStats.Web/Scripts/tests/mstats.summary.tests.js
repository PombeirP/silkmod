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
    module('MileageStats Summary Pane Widget Tests', {
        setup: function () {
            $('#qunit-fixture').append(
                '<div id="summary" class="article framed">' +
                    '<div id="summary-content">' +
                        '<div id="registration">' +
                            '<div id="registration-content"/>' +
                        '</div>' +

                        '<div id="statistics" class="statistics section">' +
                            '<div id="statistics-content"/>' +
                        '</div>' +

                        '<div class="section" id="reminders">' +
                            '<div id="summary-reminders-content"/>' +
                        '</div>' +
                    '</div>' +
                    '</div>'
            );
        },
        teardown: function () {
        }
    });

    test('when created, registers mstats.summaryPane', function () {
        expect(1);
        $('#summary').summaryPane();
        ok(mstats.summaryPane, 'summary pane added to mstats namespace');
    });

    test('when moveOffscreen is called, then moves off-screen and hides.', function () {
        expect(2);
        var originalOpacity, newOpacity, newLeft;

        $('#summary').summaryPane();
        originalOpacity = parseInt($('.mstats-summary').css('opacity'), 10);

        $('.mstats-summary').summaryPane('moveOffScreen');

        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();

            newOpacity = parseInt($('.mstats-summary').css('opacity'), 10);
            newLeft = parseInt($('.mstats-summary').css('left'), 10);

            equal(newOpacity, 0, 'opacity set');
            equal(newLeft, -350, 'moved to position');
            start();
        }, 500);
        stop(1000);
    });

    test('when moveOffscreen is called, then sets isOnScreen to false.', function () {
        expect(1);
        var summary = $('#summary').summaryPane();

        summary.summaryPane('moveOffScreen');

        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();

            equal(summary.summaryPane('option', 'isOnScreen'), false, 'set isOnScreen to false');
            start();
        }, 500);
        stop(1000);
    });


    test('when moveOnscreen is called, then shows and moves on-screen.', function () {
        expect(2);
        var newOpacity, newLeft;

        $('#summary').summaryPane();
        $('.mstats-summary').summaryPane('moveOffScreen');
        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();
        }, 500);

        // move back on screen
        $('.mstats-summary').summaryPane('moveOnScreen');

        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();

            newOpacity = parseInt($('.mstats-summary').css('opacity'), 10);
            newLeft = $('.mstats-summary').css('left');

            equal(newOpacity, 1, 'opacity set');
            equal(newLeft, '0px', 'moved to position');
            start();
        }, 500);
        stop(1000);
    });

    test('when moveOnscreen is called, then sets isOnScreen to true.', function () {
        expect(1);
        var summary = $('#summary').summaryPane();

        summary.summaryPane('moveOffScreen');

        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();
        }, 500);

        // move back on screen
        summary.summaryPane('moveOnScreen');

        // wait for delay to complete
        setTimeout(function () {
            forceCompletionOfAllAnimations();

            equal(summary.summaryPane('option', 'isOnScreen'), true, 'set isOnScreen to true');
            start();
        }, 500);
        stop(1000);
    });


    test('when created, then attached registration widget', function () {
        expect(1);
        $('#summary').summaryPane();
        equal($('.mstats-registration').length, 1, 'registration setup');
    });

    test('when created, then attached fleet statistics widget', function () {
        expect(1);
        $('#summary').summaryPane();

        equal($('.mstats-statistics').length, 1, 'statistics setup');
    });

    test('when created, then attached reminders widget', function () {
        expect(1);
        $('#summary').summaryPane();
        equal($('.mstats-reminders').length, 1, 'reminders setup');
    });

    test('when created, then defaults to on screen', function () {
        expect(1);
        var summary = $('#summary').summaryPane();
        equal(summary.summaryPane('option', 'isOnScreen'), true, 'defaults to off screen');
    });
}(jQuery));