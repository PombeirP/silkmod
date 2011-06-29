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
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using MileageStats.Data.SqlCe.Properties;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe
{
    public partial class MileageStatsDbContext : ISeedDatabase
    {
        #region Seed Data Arrays

        private readonly Int32[] _distance = new[] {350, 310, 360, 220, 310, 360, 350, 340, 375, 410, 270, 330};
        private readonly Double[] _fee = new[] {.45, 0, .50, 0, 0, 0, .30, .45, .50, 0, .45, 0};
        private readonly Double[] _price = new[] {3.5, 3.75, 3.75, 3.65, 3.45, 3.75, 3.75, 3.70, 3.5, 3.65, 3.70, 3.35};
        private readonly Double[] _units = new[] {17, 14, 16, 12, 17, 18, 16.5, 17, 17, 19, 14, 17};

        private readonly String[] _vendor = new[]
                                                {
                                                    "Fabrikam", "Contoso", "Margie's Travel", "Adventure Works", "Fabrikam", "Contoso", "Margie's Travel", "Adventure Works", "Fabrikam", "Contoso",
                                                    "Margie's Travel", "Adventure Works"
                                                };

        #endregion

        #region ISeedDatabase Members

        public void Seed()
        {
#if DEBUG
            SeedVehicleManufacturers();
#endif

            SeedCountries();
            SaveChanges();

#if DEBUG
            SeedVehicles(SeedUser());
#endif
        }

        #endregion

        private void SeedCountries()
        {
            // Add all countries present in the .NET Framework
            var regionInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID)).ToList();

            foreach (var regionInfo in regionInfos.OrderBy(x => x.EnglishName).Distinct(RegionInfoEqualityComparer.Default))
            {
                Countries.Add(new Country {Name = regionInfo.EnglishName, TwoLetterRegionCode = regionInfo.Name});
            }
        }

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

        private void SeedVehicleManufacturers()
        {
            // Team cars
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 1997, MakeName = "Honda", ModelName = "Accord LX"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2003, MakeName = "BMW", ModelName = "330xi"});

            // Well-known cars
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Audi", ModelName = "A4"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Audi", ModelName = "A6"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Audi", ModelName = "A8"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "BMW", ModelName = "330i"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "BMW", ModelName = "335i"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "BMW", ModelName = "550i"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Honda", ModelName = "Accord"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Honda", ModelName = "CRV"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Toyota", ModelName = "Prius"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Toyota", ModelName = "Sienna"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Toyota", ModelName = "Tacoma"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2010, MakeName = "Toyota", ModelName = "Tundra"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Chevrolet", ModelName = "Camero"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Chevrolet", ModelName = "Colorado"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Chevrolet", ModelName = "Corvette"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Dodge", ModelName = "Challenger"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Dodge", ModelName = "Grand Caravan"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Dodge", ModelName = "Viper"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Ford", ModelName = "Explorer"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Ford", ModelName = "Focus"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Ford", ModelName = "Fusion"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Ford", ModelName = "Mustang"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Ford", ModelName = "Taurus"});

            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Jeep", ModelName = "Grand Cherokee"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Jeep", ModelName = "Liberty"});
            VehicleManufacturerInfos.Add(new VehicleManufacturerInfo {Year = 2011, MakeName = "Jeep", ModelName = "Wrangler"});
        }

        private Int32 SeedUser()
        {
            var user = new User {AuthorizationId = "http://oturner.myidprovider.org/", DisplayName = "Sample User", CountryTwoLetterCode = "United States"};
            Users.Add(user);
            SaveChanges();
            return user.UserId;
        }

        private VehiclePhoto CreateVehiclePhoto(Image image, Int32 vehicleId)
        {
            byte[] buffer;
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, new ImageFormat(image.RawFormat.Guid));
                buffer = memoryStream.ToArray();
            }
            var vehiclePhoto = new VehiclePhoto {ImageMimeType = "image/jpeg", Image = buffer, VehicleId = vehicleId};
            VehiclePhotos.Add(vehiclePhoto);
            SaveChanges();
            return vehiclePhoto;
        }

        private void SeedVehicles(Int32 userId)
        {
            var vehicle = new Vehicle {UserId = userId, Name = "Hot Rod", SortOrder = 1, Year = 2003, MakeName = "BMW", ModelName = "330xi"};
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.bmw, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(1000, DateTime.Now.AddDays(-365), vehicle, 1, 1);
            CreateReminders(vehicle);

            vehicle = new Vehicle {UserId = userId, Name = "Soccer Mom's Ride", SortOrder = 2, Year = 1997, MakeName = "Honda", ModelName = "Accord LX"};
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.soccermomcar, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(500, DateTime.Now.AddDays(-370), vehicle, .9, 1.2);
            CreateReminders(vehicle);

            vehicle = new Vehicle {UserId = userId, Name = "Mud Lover", SortOrder = 3, Year = 2011, MakeName = "Jeep", ModelName = "Wrangler"};
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.jeep, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(750, DateTime.Now.AddDays(-373), vehicle, 1.2, .8);
            CreateReminders(vehicle);
        }

        private void CreateReminders(Vehicle vehicle)
        {
            FillupEntry lastFillup = vehicle.Fillups.OrderByDescending(f => f.Date).FirstOrDefault();
            if (lastFillup == null)
            {
                return;
            }

            Reminder reminder;

            // create overdue by mileage reminder
            reminder = new Reminder
                           {
                               DueDate = null,
                               DueDistance = lastFillup.Odometer - 10,
                               IsFulfilled = false,
                               Remarks = "Check air filter when oil is changed",
                               Title = "Oil Change",
                               VehicleId = vehicle.VehicleId
                           };
            vehicle.Reminders.Add(reminder);

            // create overdue by date reminder
            reminder = new Reminder
                           {
                               DueDate = lastFillup.Date.AddDays(-10),
                               DueDistance = null,
                               IsFulfilled = false,
                               Remarks = "Check condition of the wipers",
                               Title = "Check Wiper Fluid",
                               VehicleId = vehicle.VehicleId
                           };
            vehicle.Reminders.Add(reminder);

            // create to be done soon by mileage reminder
            reminder = new Reminder
                           {DueDate = null, DueDistance = lastFillup.Odometer + 400, IsFulfilled = false, Remarks = "Check air pressure", Title = "Rotate Tires", VehicleId = vehicle.VehicleId};
            vehicle.Reminders.Add(reminder);

            // create to be done soon by date reminder
            reminder = new Reminder {DueDate = DateTime.Now.AddDays(+10), DueDistance = null, IsFulfilled = false, Remarks = "Check air freshener", Title = "Vacuum Car", VehicleId = vehicle.VehicleId};
            vehicle.Reminders.Add(reminder);
        }

        /// <summary>
        /// Randomizes the elements of the array.
        /// </summary>
        /// <param name="array">An array of integers.</param>
        /// <returns>Randomly sorted array</returns>
        private Int32[] RandomizeArray(Int32[] array)
        {
            var random = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int swapPosition = random.Next(i + 1);
                int temp = array[i];
                array[i] = array[swapPosition];
                array[swapPosition] = temp;
            }
            return array;
        }

        /// <summary>
        /// Creates the fillups.
        /// </summary>
        /// <param name="odometer">The initial odometer reading</param>
        /// <param name="date">The first date to create a fill up for</param>
        /// <param name="vehicle">The vehicle object to create the fill ups for</param>
        /// <param name="unitsModifier">The units modifier is applied to the total gallons calculation.
        ///   By supplying a different value for each vehicle, the data will be different for each vehicle.
        /// </param>
        /// <param name="distanceModifier">The distance modifier is applied to the distance calculation.
        ///   By supplying a different value for each vehicle, the data will be different for each vehicle.
        /// </param>
        /// <remarks>
        /// Creates random fill up sample data for the vehicle. 
        /// Consumes the data arrays at the top of this class.
        /// Randomizes the index used to access data arrays by creating an array then randomly sorting the array elements.
        /// The "while" loop runs while calculated date is less than the current date.
        /// The date is recalculated each cycle of the while loop, adding a random number of days between 3 and 18 days to the previous value.
        /// </remarks>
        private void CreateFillups(Int32 odometer, DateTime date, Vehicle vehicle, Double unitsModifier, Double distanceModifier)
        {
            int[] randomArray = RandomizeArray(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11});
            int currentIndex = 0;
            var random = new Random();
            bool isFirst = true;
            while (date < DateTime.Now)
            {
                int dataIndex = randomArray[currentIndex];
                var distance = (Int32) (this._distance[dataIndex]*distanceModifier);
                var fillup = new FillupEntry();
                fillup.Date = date;
                if (isFirst)
                {
                    isFirst = false;
                    fillup.Distance = null;
                }
                else
                {
                    fillup.Distance = distance;
                }
                fillup.Odometer = odometer;
                fillup.PricePerUnit = this._price[dataIndex];
                fillup.TotalUnits = this._units[dataIndex]*unitsModifier;
                fillup.TransactionFee = this._fee[dataIndex];
                fillup.VehicleId = vehicle.VehicleId;
                fillup.Vendor = this._vendor[dataIndex];
                odometer += distance;
                vehicle.Fillups.Add(fillup);
                currentIndex += 1;
                if (currentIndex > 11)
                {
                    currentIndex = 0;
                }
                date = date.AddDays(random.Next(3, 14));
            }
            SaveChanges();
        }
    }
}
