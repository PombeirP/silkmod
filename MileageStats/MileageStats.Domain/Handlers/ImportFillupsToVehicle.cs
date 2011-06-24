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
using System.Collections.Generic;
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class ImportFillupsToVehicle
    {
        private readonly IFillupRepository _fillupRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public ImportFillupsToVehicle(IVehicleRepository vehicleRepository, IFillupRepository fillupRepository)
        {
            _vehicleRepository = vehicleRepository;
            _fillupRepository = fillupRepository;
        }

        public virtual void Execute(int userId, int vehicleId, IEnumerable<FillupEntry> importedFillups)
        {
            if (importedFillups == null) throw new ArgumentNullException("importedFillups");

            try
            {
                Vehicle vehicle = _vehicleRepository.GetVehicle(userId, vehicleId);

                if (vehicle != null)
                {
                    foreach (FillupEntry newFillup in importedFillups)
                    {
                        newFillup.VehicleId = vehicleId;

                        AdjustSurroundingFillupEntries(newFillup);

                        _fillupRepository.Create(userId, vehicleId, newFillup);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToAddFillupToVehicleExceptionMessage, ex);
            }
        }

        private void AdjustSurroundingFillupEntries(FillupEntry newFillup)
        {
            if (newFillup == null) throw new ArgumentNullException("newFillup");

            IEnumerable<FillupEntry> fillups = _fillupRepository.GetFillups(newFillup.VehicleId);

            // Prior fillups are ordered descending so that FirstOrDefault() chooses the one closest to the new fillup.
            // Secondary ordering is by entry ID ensure a consistent ordering/
            FillupEntry priorFillup = fillups
                .OrderByDescending(f => f.Date).ThenByDescending(f => f.FillupEntryId)
                .Where(f => (f.Date <= newFillup.Date) && (f.FillupEntryId != newFillup.FillupEntryId)).FirstOrDefault();

            // Prior fillups are ordered ascending that FirstOrDefault() chooses the one closest to the new fillup.
            // Secondary ordering is by entry ID ensure a consistent ordering.
            FillupEntry nextFillup = fillups
                .OrderBy(f => f.Date).ThenBy(f => f.FillupEntryId)
                .Where(f => (f.Date >= newFillup.Date) && (f.FillupEntryId != newFillup.FillupEntryId)).FirstOrDefault();

            CalculateInterFillupStatistics(newFillup, priorFillup);
            CalculateInterFillupStatistics(nextFillup, newFillup);
        }

        private static void CalculateInterFillupStatistics(FillupEntry fillup, FillupEntry priorFillup)
        {
            if (priorFillup != null && fillup != null)
            {
                fillup.Distance = Math.Abs(fillup.Odometer - priorFillup.Odometer);
            }
        }
    }
}