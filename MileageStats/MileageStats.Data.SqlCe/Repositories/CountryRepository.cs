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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly List<Country> countriesList = new List<Country>(128);

        public CountryRepository()
        {
            SeedCountries();
        }

        #region ICountryRepository Members

        public IEnumerable<Country> GetAll()
        {
            return this.countriesList.ToList();
        }

        #endregion

        private void SeedCountries()
        {
            // Add all countries present in the .NET Framework
            List<RegionInfo> regionInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID)).ToList();

            foreach (RegionInfo regionInfo in regionInfos.OrderBy(x => x.EnglishName).Distinct(RegionInfoEqualityComparer.Default))
            {
                this.countriesList.Add(new Country {Name = regionInfo.EnglishName, TwoLetterRegionCode = regionInfo.Name});
            }
        }

        #region Nested type: RegionInfoEqualityComparer

        private class RegionInfoEqualityComparer : EqualityComparer<RegionInfo>
        {
            public override bool Equals(RegionInfo x, RegionInfo y)
            {
                return x.GeoId == y.GeoId;
            }

            public override int GetHashCode(RegionInfo obj)
            {
                return obj.GetHashCode();
            }
        }

        #endregion
    }
}
