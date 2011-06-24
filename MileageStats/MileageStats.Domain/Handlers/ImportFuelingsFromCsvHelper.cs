using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class ImportFuelingsFromCsvHelper
    {
        private static readonly CultureInfo FileCultureInfo = CultureInfo.GetCultureInfo("de");

        public static IEnumerable<FillupEntry> ReadFillupEntriesFromFile(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string[] fieldNames = null;
                for (;;)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    if (fieldNames == null)
                    {
                        fieldNames = line.Split(';');
                        continue;
                    }

                    string[] fieldValues = line.Split(';');
                    var fillupEntry = new FillupEntry {UnitOfMeasure = FillupUnits.Litres};
                    int index = 0;
                    foreach (string fieldValue in fieldValues)
                    {
                        ParseField(fillupEntry, fieldNames[index++], fieldValue);
                    }

                    yield return fillupEntry;
                }
            }
        }

        private static void ParseField(FillupEntry fillupEntry, string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "Date":
                    fillupEntry.Date = ParseDate(fieldValue);
                    break;
                case "Odometer":
                    fillupEntry.Odometer = (int) ParseDouble(fieldValue);
                    break;
                case "Trip":
                    fillupEntry.Distance = (int) ParseDouble(fieldValue);
                    break;
                case "Quantity":
                    fillupEntry.TotalUnits = ParseDouble(fieldValue);
                    break;
                case "Total price":
                    // This is a calculated field in FillupEntry, so we'll just update PricePerUnit
                    fillupEntry.PricePerUnit = ParseDouble(fieldValue)/fillupEntry.TotalUnits;
                    break;
                case "Currency":
                    break;
                case "Type":
                    break;
                case "Tires":
                    break;
                case "Roads":
                    break;
                case "Driving style":
                    break;
                case "Fuel":
                    break;
                case "Note":
                    fillupEntry.Remarks = fieldValue.Trim(new[] {'\"'});
                    break;
                case "Consumption":
                    break;
            }
        }

        private static DateTime ParseDate(string fieldValue)
        {
            return DateTime.ParseExact(fieldValue, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeLocal);
        }

        private static double ParseDouble(string fieldValue)
        {
            return Double.Parse(fieldValue, NumberStyles.AllowDecimalPoint, FileCultureInfo);
        }
    }
}