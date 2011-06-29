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
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Web.Helpers;
using MileageStats.Web.Models;

namespace MileageStats.Web.Controllers
{
    /// <summary>
    /// Provides base for controllers that need authorization and user information.
    /// </summary>
    /// <remarks>
    /// This base controller largely provides common methods to recover authorized user information.
    /// </remarks>
    public class AuthorizedController : Controller
    {
        protected readonly IUserServices UserServices;
        private readonly IServiceLocator serviceLocator;
        private User currentUser;

        public AuthorizedController(IUserServices userServices, IServiceLocator serviceLocator)
        {
            if (userServices == null)
            {
                throw new ArgumentNullException("userServices");
            }
            this.UserServices = userServices;
            this.serviceLocator = serviceLocator;
        }

        #region Properties

        /// <summary>
        /// Retrieves the CurrentUserId as stored in the <see cref="MileageStatsIdentity"/>
        /// </summary>
        /// <remarks>
        /// Using this method requires the user to be authorized.
        /// </remarks>
        protected int CurrentUserId
        {
            get { return User.MileageStatsIdentity().UserId; }
        }

        /// <summary>
        /// Returns the current user or recovers the user from the <see cref="MileageStatsIdentity"/>.
        /// </summary>
        /// <remarks>
        /// Using this method requires the user to be authorized.
        /// </remarks>
        public User CurrentUser
        {
            get
            {
                return this.currentUser ??
                       (this.currentUser = this.UserServices.GetUserFromIdentity(User.MileageStatsIdentity()));
            }
        }

        #endregion

        #region Overrides

        protected override void ExecuteCore()
        {
            if (User.Identity.IsAuthenticated)
            {
                SetCulturePreferences();
            }

            base.ExecuteCore();
        }

        #endregion

        #region Implementation

        private void SetCulturePreferences()
        {
            if (CurrentUser == null)
            {
                return;
            }

            // Try to use cached culture info objects
            var cultureInfo = (CultureInfo) Session["CultureInfo"];
            var regionInfo = (RegionInfo) Session["RegionInfo"];

            if (cultureInfo == null)
            {
                if (string.IsNullOrEmpty(CurrentUser.TwoLetterCountryCode))
                {
                    cultureInfo = Thread.CurrentThread.CurrentCulture;
                    regionInfo = new RegionInfo(cultureInfo.TwoLetterISOLanguageName);
                }
                else
                {
                    regionInfo = new RegionInfo(CurrentUser.TwoLetterCountryCode);

                    cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(x => x.Name.EndsWith(regionInfo.Name));
                }

                Session["CultureInfo"] = cultureInfo;
                Session["RegionInfo"] = regionInfo;
            }

            // Assign the correct culture for this account
            UnitConversionHelper.SetUnitsForRegion(regionInfo);

            if (cultureInfo != null)
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }

        protected void ClearCachedCultureInfo()
        {
            Session["CultureInfo"] = null;
            Session["RegionInfo"] = null;
        }

        protected T Using<T>() where T : class
        {
            var handler = this.serviceLocator.GetInstance<T>();
            if (handler == null)
            {
                throw new NullReferenceException("Unable to resolve type with service locator; type " + typeof (T).Name);
            }
            return handler;
        }

        #endregion
    }
}
