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

using System;
using System.Web.Mvc;
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Web.Helpers
{
    public static class ReminderSummaryHelper
    {
        public static MvcHtmlString DisplayDueOnText(this HtmlHelper helper, ReminderSummaryModel reminderSummary)
        {
            var msg = FormatDueOnText(reminderSummary.Reminder);

            return MvcHtmlString.Create(msg);
        }

        public static string FormatDueOnText(Reminder reminder)
        {
            var msg = reminder.DueDate == null
                          ? string.Empty
                          : String.Format("on {0:d}", reminder.DueDate);

            msg += reminder.DueDate == null || reminder.DueDistance == null
                       ? string.Empty
                       : " or ";

            msg += reminder.DueDistance == null
                       ? string.Empty
                       : String.Format("at {0}", UserDisplayPreferencesHelper.FormatDistanceWithAbbreviation(reminder.DueDistance.Value));
            msg += ".";
            return msg;
        }
    }
}
