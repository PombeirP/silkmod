using System;
using System.Web.Mvc;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Web.Helpers
{
    public static class UserDisplayPreferencesHelper
    {
        public static MvcHtmlString DistanceTextWithAbbreviationFor(this HtmlHelper helper, double distance)
        {
            return MvcHtmlString.Create(DistanceTextWithAbbreviationFor(distance));
        }

        public static string DistanceTextWithAbbreviationFor(double distanceInKm)
        {
            double distanceInUserUnits = FuelConsumptionHelper.ConvertDistanceToUserUnit(distanceInKm);
            return string.Format("{0:N0} {1}", distanceInUserUnits, DisplayDistanceUnitAbbreviation(plural: Math.Abs(Math.Round(distanceInUserUnits) - 1) > 0.1));
        }

        public static MvcHtmlString DisplayQuantityFor(this HtmlHelper helper, double quantity,
                                                       FillupUnits unitOfMeasure)
        {
            return MvcHtmlString.Create(DisplayQuantityFor(quantity, unitOfMeasure));
        }

        public static string DisplayQuantityFor(double quantity, FillupUnits unitOfMeasure)
        {
            double liters = FuelConsumptionHelper.ConvertVolumeToLiters(quantity, unitOfMeasure);
            return string.Format(FuelConsumptionHelper.UserUnitOfMeasure == FillupUnits.Litres ? "{0:#00.00}{1}" : "{0:#00.000} {1}", FuelConsumptionHelper.ConvertVolumeToUserUnit(liters), DisplayVolumeUnitAbbreviation());
        }

        private static string DisplayVolumeUnitAbbreviation(bool plural = false)
        {
            switch (FuelConsumptionHelper.UserUnitOfMeasure)
            {
                case FillupUnits.ImperialGallons:
                    return plural ? Resources.UserDisplayPreferencesHelper_DisplayVolumeUnitAbbreviation_ImpGals : Resources.UserDisplayPreferencesHelper_DisplayVolumeUnitAbbreviation_ImpGal;
                case FillupUnits.UsGallons:
                    return plural ? Resources.UserDisplayPreferencesHelper_DisplayVolumeUnitAbbreviation_UsGallons : Resources.UserDisplayPreferencesHelper_DisplayVolumeUnitAbbreviation_UsGallon;
                case FillupUnits.Litres:
                    return Resources.UserDisplayPreferencesHelper_DisplayVolumeUnitAbbreviation_Litres;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static MvcHtmlString DisplayPricePerUnitFor(this HtmlHelper helper, double pricePerUnit, FillupUnits unitOfMeasure)
        {
            return MvcHtmlString.Create(DisplayPricePerUnitFor(pricePerUnit, unitOfMeasure));
        }

        public static string DisplayPricePerUnitFor(double pricePerUnit, FillupUnits unitOfMeasure)
        {
            double pricePerLiter = pricePerUnit/FuelConsumptionHelper.ConvertVolumeToLiters(1, unitOfMeasure);
            double pricePerUserUnit = pricePerLiter/FuelConsumptionHelper.ConvertVolumeToUserUnit(1.0);
            return string.Format("{0:C2}/{1}", pricePerUserUnit, DisplayVolumeUnitAbbreviation(plural: false));
        }

        public static MvcHtmlString DisplayPriceFor(this HtmlHelper helper, double price)
        {
            return MvcHtmlString.Create(DisplayPriceFor(price));
        }

        public static string DisplayPriceFor(double price)
        {
            return string.Format("{0:C}", Math.Round(price, 2));
        }

        public static MvcHtmlString DisplayPriceInCentsFor(this HtmlHelper helper, double price)
        {
            return MvcHtmlString.Create(DisplayPriceInCentsFor(price));
        }

        public static string DisplayPriceInCentsFor(double price)
        {
            if (price >= 10)
            {
                return string.Format("{0:C0}", price);
            }

            if (price >= 1)
            {
                return string.Format("{0:C}", price);
            }

            return string.Format("{0:N0}¢", price*100);
        }

        public static string DisplayPriceFor(double price, int decimalPlaces)
        {
            string format = string.Format("{{0:C{0}}}", decimalPlaces);
            return string.Format(format, price);
        }

        public static MvcHtmlString DisplayPriceFor(this HtmlHelper helper, double price, int decimalPlaces)
        {
            return MvcHtmlString.Create(DisplayPriceFor(price, decimalPlaces));
        }

        public static string DisplayFuelConsumptionAverageFor(double fuelConsumptionAverage)
        {
            double fuelConsumptionInUserUnits = FuelConsumptionHelper.ConvertConsumptionToUserUnits(fuelConsumptionAverage);

            string averageFuelEfficiencyText;

            switch (FuelConsumptionHelper.UserUnitOfMeasure)
            {
                case FillupUnits.ImperialGallons:
                case FillupUnits.UsGallons:
                    averageFuelEfficiencyText = string.Format("{0:N0}", fuelConsumptionInUserUnits);
                    if (fuelConsumptionInUserUnits >= 99000)
                    {
                        averageFuelEfficiencyText = "99k+";
                    }
                    else if (fuelConsumptionInUserUnits >= 10000)
                    {
                        averageFuelEfficiencyText = string.Format("{0:N1}k", fuelConsumptionInUserUnits/1000);
                    }
                    break;
                case FillupUnits.Litres:
                    averageFuelEfficiencyText = string.Format(fuelConsumptionInUserUnits >= 10 ? "{0:N0}" : "{0:N1}", fuelConsumptionInUserUnits);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return averageFuelEfficiencyText;
        }

        public static MvcHtmlString DisplayFuelConsumptionAverageFor(this HtmlHelper helper, double fuelConsumptionAverage)
        {
            return MvcHtmlString.Create(DisplayFuelConsumptionAverageFor(fuelConsumptionAverage));
        }

        public static string DisplayFuelConsumptionAverageWithUnitAbbreviationFor(double fuelConsumptionAverage)
        {
            return string.Format("{0:N} {1}", FuelConsumptionHelper.ConvertConsumptionToUserUnits(fuelConsumptionAverage), DisplayFuelConsumptionAverageAbbreviation());
        }

        public static MvcHtmlString DisplayFuelConsumptionAverageWithUnitAbbreviationFor(this HtmlHelper helper, double fuelConsumptionAverage)
        {
            return MvcHtmlString.Create(DisplayFuelConsumptionAverageWithUnitAbbreviationFor(fuelConsumptionAverage));
        }

        public static string DisplayFuelConsumptionAverageAbbreviation()
        {
            return FuelConsumptionHelper.UserUnitOfMeasure == FillupUnits.Litres ? Resources.UserDisplayPreferencesHelper_DisplayFuelConsumptionAverageAbbreviation_l_100km : Resources.UserDisplayPreferencesHelper_DisplayFuelConsumptionAverageAbbreviation_mpg;
        }

        public static MvcHtmlString DisplayFuelConsumptionAverageAbbreviation(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(DisplayFuelConsumptionAverageAbbreviation());
        }

        public static string DisplayDistanceUnitAbbreviation(bool plural = false)
        {
            switch (FuelConsumptionHelper.UserDistanceUnit)
            {
                case DistanceUnits.Kilometer:
                    return plural ? Resources.UserDisplayPreferencesHelper_DisplayDistanceUnitAbbreviation_kms : Resources.UserDisplayPreferencesHelper_DisplayDistanceUnitAbbreviation_km;
                case DistanceUnits.Mile:
                    return plural ? Resources.UserDisplayPreferencesHelper_DisplayDistanceUnitAbbreviation_miles : Resources.UserDisplayPreferencesHelper_DisplayDistanceUnitAbbreviation_mile;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static MvcHtmlString DisplayDistanceUnitAbbreviation(this HtmlHelper helper, bool plural = false)
        {
            return MvcHtmlString.Create(DisplayDistanceUnitAbbreviation(plural));
        }
    }
}
