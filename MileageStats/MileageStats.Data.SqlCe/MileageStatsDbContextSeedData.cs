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
using System.Drawing;
using System.Drawing.Imaging;
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
            var countries = new[]
                                {
                                    new Country {Name = "Afghanistan"},
                                    new Country
                                        {
                                            Name = "Albania"
                                        },
                                    new Country {Name = "Algeria"},
                                    new Country {Name = "American Samoa"},
                                    new Country {Name = "Andorra"},
                                    new Country {Name = "Angola"},
                                    new Country {Name = "Anguilla"},
                                    new Country {Name = "Antarctica"},
                                    new Country {Name = "Antigua and Barbuda"},
                                    new Country {Name = "Argentina"},
                                    new Country {Name = "Armenia"},
                                    new Country {Name = "Aruba"},
                                    new Country {Name = "Australia"},
                                    new Country {Name = "Austria"},
                                    new Country {Name = "Azerbaijan"},
                                    new Country {Name = "Bahamas, The"},
                                    new Country {Name = "Bahrain"},
                                    new Country {Name = "Bangladesh"},
                                    new Country {Name = "Barbados"},
                                    new Country {Name = "Belarus"},
                                    new Country {Name = "Belgium"},
                                    new Country {Name = "Belize"},
                                    new Country {Name = "Benin"},
                                    new Country {Name = "Bermuda"},
                                    new Country {Name = "Bhutan"},
                                    new Country {Name = "Bolivia"},
                                    new Country {Name = "Bosnia and Herzegovina"},
                                    new Country {Name = "Botswana"},
                                    new Country {Name = "Bouvet Island"},
                                    new Country {Name = "Brazil"},
                                    new Country {Name = "British Indian Ocean Territory"},
                                    new Country {Name = "Brunei"},
                                    new Country {Name = "Bulgaria"},
                                    new Country {Name = "Burkina Faso"},
                                    new Country {Name = "Burundi"},
                                    new Country {Name = "Cambodia"},
                                    new Country {Name = "Cameroon"},
                                    new Country {Name = "Canada"},
                                    new Country {Name = "Cape Verde"},
                                    new Country {Name = "Cayman Islands"},
                                    new Country {Name = "Central African Republic"},
                                    new Country {Name = "Chad"},
                                    new Country {Name = "Chile"},
                                    new Country {Name = "China"},
                                    new Country {Name = "Christmas Island"},
                                    new Country {Name = "Cocos Islands"},
                                    new Country {Name = "Colombia"},
                                    new Country {Name = "Comoros"},
                                    new Country {Name = "Congo"},
                                    new Country {Name = "Cook Islands"},
                                    new Country {Name = "Costa Rica"},
                                    new Country {Name = "Cote d'Ivoire"},
                                    new Country {Name = "Croatia"},
                                    new Country {Name = "Cyprus"},
                                    new Country {Name = "Czech Republic"},
                                    new Country {Name = "Denmark"},
                                    new Country {Name = "Djibouti"},
                                    new Country {Name = "Dominica"},
                                    new Country {Name = "Dominican Republic"},
                                    new Country {Name = "Ecuador"},
                                    new Country {Name = "Egypt"},
                                    new Country {Name = "El Salvador"},
                                    new Country {Name = "Equatorial Guinea"},
                                    new Country {Name = "Eritrea"},
                                    new Country {Name = "Estonia"},
                                    new Country {Name = "Ethiopia"},
                                    new Country {Name = "Falkland Islands"},
                                    new Country {Name = "Faroe Islands"},
                                    new Country {Name = "Fiji"},
                                    new Country {Name = "Finland"},
                                    new Country {Name = "France"},
                                    new Country {Name = "French Guiana"},
                                    new Country {Name = "French Polynesia"},
                                    new Country {Name = "French Southern and Antarctic Lands"},
                                    new Country {Name = "Gabon"},
                                    new Country {Name = "Gambia, The"},
                                    new Country {Name = "Georgia"},
                                    new Country {Name = "Germany"},
                                    new Country {Name = "Ghana"},
                                    new Country {Name = "Gibraltar"},
                                    new Country {Name = "Greece"},
                                    new Country {Name = "Greenland"},
                                    new Country {Name = "Grenada"},
                                    new Country {Name = "Guadeloupe"},
                                    new Country {Name = "Guam"},
                                    new Country {Name = "Guatemala"},
                                    new Country {Name = "Guernsey"},
                                    new Country {Name = "Guinea"},
                                    new Country {Name = "Guinea-Bissau"},
                                    new Country {Name = "Guyana"},
                                    new Country {Name = "Haiti"},
                                    new Country {Name = "Heard Island and McDonald Islands"},
                                    new Country {Name = "Honduras"},
                                    new Country {Name = "Hong Kong SAR"},
                                    new Country {Name = "Hungary"},
                                    new Country {Name = "Iceland"},
                                    new Country {Name = "India"},
                                    new Country {Name = "Indonesia"},
                                    new Country {Name = "Iraq"},
                                    new Country {Name = "Ireland"},
                                    new Country {Name = "Isle of Man"},
                                    new Country {Name = "Israel"},
                                    new Country {Name = "Italy"},
                                    new Country {Name = "Jamaica"},
                                    new Country {Name = "Japan"},
                                    new Country {Name = "Jersey"},
                                    new Country {Name = "Jordan"},
                                    new Country {Name = "Kazakhstan"},
                                    new Country {Name = "Kenya"},
                                    new Country {Name = "Kiribati"},
                                    new Country {Name = "Korea"},
                                    new Country {Name = "Kuwait"},
                                    new Country {Name = "Kyrgyzstan"},
                                    new Country {Name = "Laos"},
                                    new Country {Name = "Latvia"},
                                    new Country {Name = "Lebanon"},
                                    new Country {Name = "Lesotho"},
                                    new Country {Name = "Liberia"},
                                    new Country {Name = "Libya"},
                                    new Country {Name = "Liechtenstein"},
                                    new Country {Name = "Lithuania"},
                                    new Country {Name = "Luxembourg"},
                                    new Country {Name = "Macao SAR"},
                                    new Country {Name = "Macedonia, Former Yugoslav Republic of"},
                                    new Country {Name = "Madagascar"},
                                    new Country {Name = "Malawi"},
                                    new Country {Name = "Malaysia"},
                                    new Country {Name = "Maldives"},
                                    new Country {Name = "Mali"},
                                    new Country {Name = "Malta"},
                                    new Country {Name = "Marshall Islands"},
                                    new Country {Name = "Martinique"},
                                    new Country {Name = "Mauritania"},
                                    new Country {Name = "Mauritius"},
                                    new Country {Name = "Mayotte"},
                                    new Country {Name = "Mexico"},
                                    new Country {Name = "Micronesia"},
                                    new Country {Name = "Moldova"},
                                    new Country {Name = "Monaco"},
                                    new Country {Name = "Mongolia"},
                                    new Country {Name = "Montenegro"},
                                    new Country {Name = "Montserrat"},
                                    new Country {Name = "Morocco"},
                                    new Country {Name = "Mozambique"},
                                    new Country {Name = "Myanmar"},
                                    new Country {Name = "Namibia"},
                                    new Country {Name = "Nauru"},
                                    new Country {Name = "Nepal"},
                                    new Country {Name = "Netherlands"},
                                    new Country {Name = "Netherlands Antilles"},
                                    new Country {Name = "New Caledonia"},
                                    new Country {Name = "New Zealand"},
                                    new Country {Name = "Nicaragua"},
                                    new Country {Name = "Niger"},
                                    new Country {Name = "Nigeria"},
                                    new Country {Name = "Niue"},
                                    new Country {Name = "Norfolk Island"},
                                    new Country {Name = "Northern Mariana Islands"},
                                    new Country {Name = "Norway"},
                                    new Country {Name = "Oman"},
                                    new Country {Name = "Pakistan"},
                                    new Country {Name = "Palau"},
                                    new Country {Name = "Palestinian Authority"},
                                    new Country {Name = "Panama"},
                                    new Country {Name = "Papua New Guinea"},
                                    new Country {Name = "Paraguay"},
                                    new Country {Name = "Peru"},
                                    new Country {Name = "Philippines"},
                                    new Country {Name = "Pitcairn Islands"},
                                    new Country {Name = "Poland"},
                                    new Country {Name = "Portugal"},
                                    new Country {Name = "Puerto Rico"},
                                    new Country {Name = "Qatar"},
                                    new Country {Name = "Reunion"},
                                    new Country {Name = "Romania"},
                                    new Country {Name = "Russia"},
                                    new Country {Name = "Rwanda"},
                                    new Country {Name = "Saint Helena"},
                                    new Country {Name = "Saint Kitts and Nevis"},
                                    new Country {Name = "Saint Lucia"},
                                    new Country {Name = "Saint Pierre and Miquelon"},
                                    new Country {Name = "Saint Vincent and the Grenadines"},
                                    new Country {Name = "Samoa"},
                                    new Country {Name = "San Marino"},
                                    new Country {Name = "Sao Tome and Principe"},
                                    new Country {Name = "Saudi Arabia"},
                                    new Country {Name = "Senegal"},
                                    new Country {Name = "Serbia"},
                                    new Country {Name = "Seychelles"},
                                    new Country {Name = "Sierra Leone"},
                                    new Country {Name = "Singapore"},
                                    new Country {Name = "Slovakia"},
                                    new Country {Name = "Slovenia"},
                                    new Country {Name = "Solomon Islands"},
                                    new Country {Name = "Somalia"},
                                    new Country {Name = "South Africa"},
                                    new Country {Name = "South Georgia and the South Sandwich Islands"},
                                    new Country {Name = "Spain"},
                                    new Country {Name = "Sri Lanka"},
                                    new Country {Name = "Suriname"},
                                    new Country {Name = "Svalbard"},
                                    new Country {Name = "Swaziland"},
                                    new Country {Name = "Sweden"},
                                    new Country {Name = "Switzerland"},
                                    new Country {Name = "Taiwan"},
                                    new Country {Name = "Tajikistan"},
                                    new Country {Name = "Tanzania"},
                                    new Country {Name = "Thailand"},
                                    new Country {Name = "Timor-Leste"},
                                    new Country {Name = "Togo"},
                                    new Country {Name = "Tokelau"},
                                    new Country {Name = "Tonga"},
                                    new Country {Name = "Trinidad and Tobago"},
                                    new Country {Name = "Tunisia"},
                                    new Country {Name = "Turkey"},
                                    new Country {Name = "Turkmenistan"},
                                    new Country {Name = "Turks and Caicos Islands"},
                                    new Country {Name = "Tuvalu"},
                                    new Country {Name = "U.S. Minor Outlying Islands"},
                                    new Country {Name = "Uganda"},
                                    new Country {Name = "Ukraine"},
                                    new Country {Name = "United Arab Emirates"},
                                    new Country {Name = "United Kingdom"},
                                    new Country {Name = "United States"},
                                    new Country {Name = "Uruguay"},
                                    new Country {Name = "Uzbekistan"},
                                    new Country {Name = "Vanuatu"},
                                    new Country {Name = "Holy See"},
                                    new Country {Name = "Venezuela"},
                                    new Country {Name = "Vietnam"},
                                    new Country {Name = "Virgin Islands, British"},
                                    new Country {Name = "Virgin Islands"},
                                    new Country {Name = "Wallis and Futuna"},
                                    new Country {Name = "Yemen"},
                                    new Country {Name = "Zambia"},
                                    new Country {Name = "Zimbabwe"},
                                    new Country {Name = "Saint Barthelemy"},
                                    new Country {Name = "Saint Martin"}
                                };

            foreach (var country in countries)
            {
                Countries.Add(country);
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
            var user = new User {AuthorizationId = "http://oturner.myidprovider.org/", DisplayName = "Sample User", Country = "United States"};
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
