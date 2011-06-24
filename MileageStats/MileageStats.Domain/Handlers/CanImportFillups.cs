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
using System.Web;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class CanImportFillups
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFillupRepository _fillupRepository;

        public CanImportFillups(IVehicleRepository vehicleRepository, IFillupRepository fillupRepository)
        {
            _vehicleRepository = vehicleRepository;
            _fillupRepository = fillupRepository;
        }

        public virtual IEnumerable<ValidationResult> Execute(int userId, int vehicleId, IEnumerable<FillupEntry> importedFillups)
        {
            var foundVehicle = _vehicleRepository.GetVehicle(userId, vehicleId);

            if (foundVehicle == null)
            {
                yield return new ValidationResult(Resources.VehicleNotFound);
            }
            else
            {
                var fillups = _fillupRepository.GetFillups(vehicleId);

                if (!fillups.Any()) yield break;

                foreach (var importedFillup in importedFillups)
                {
                    var priorFillup = fillups.FirstOrDefault(f => f.Date < importedFillup.Date);
                    var posteriorFillup = fillups.FirstOrDefault(f => f.Date > importedFillup.Date);

                    if ((priorFillup != null) && (priorFillup.Odometer >= importedFillup.Odometer))
                    {
                        yield return new ValidationResult(
                            "Odometer",
                            string.Format(CultureInfo.CurrentUICulture,
                                          Resources.OdometerNotGreaterThanPrior,
                                          priorFillup.Odometer));
                    }

                    if ((posteriorFillup != null) && (posteriorFillup.Odometer <= importedFillup.Odometer))
                    {
                        yield return new ValidationResult(
                            "Odometer",
                            string.Format(CultureInfo.CurrentUICulture,
                                          Resources.OdometerGreaterThanPosterior,
                                          posteriorFillup.Odometer));
                    }
                }
            }
        }
    }
}
