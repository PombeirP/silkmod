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

using System.Diagnostics;

namespace MileageStats.Model
{
    [DebuggerDisplay("{Name}, {TwoLetterRegionCode}")]
    public class Country
    {
        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the name or ISO 3166 two-letter country/region code for the current Country object.
        /// </summary>
        public string TwoLetterRegionCode { get; set; }
    }
}