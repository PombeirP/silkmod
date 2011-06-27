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
            return MvcHtmlString.Create(FormatDistanceTextWithAbbreviationFor(distance));
        }

        public static string FormatDistanceTextWithAbbreviationFor(double distanceInKm)
        {
            double distanceInUserUnits = UnitConversionHelper.ConvertDistanceToUserUnit(distanceInKm);
            return string.Format("{0:N0} {1}", distanceInUserUnits, FormatDistanceUnitAbbreviation(plural: Math.Abs(Math.Round(distanceInUserUnits) - 1) > 0.1));
        }

        public static MvcHtmlString DisplayQuantityFor(this HtmlHelper helper, double quantity,
                                                       FillupUnits unitOfMeasure)
        {
            return MvcHtmlString.Create(FormatQuantityFor(quantity, unitOfMeasure));
        }

        public static string FormatQuantityFor(double quantity, FillupUnits unitOfMeasure)
        {
            double liters = UnitConversionHelper.ConvertVolumeToLiters(quantity, unitOfMeasure);
            return string.Format(UnitConversionHelper.UserUnitOfMeasure == FillupUnits.Litres ? "{0:#00.00}{1}" : "{0:#00.000} {1}", UnitConversionHelper.ConvertVolumeToUserUnit(liters), FormatVolumeUnitAbbreviation());
        }

        private static string FormatVolumeUnitAbbreviation(bool plural = false)
        {
            switch (UnitConversionHelper.UserUnitOfMeasure)
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
            return MvcHtmlString.Create(FormatPricePerUnitFor(pricePerUnit, unitOfMeasure));
        }

        public static string FormatPricePerUnitFor(double pricePerUnit, FillupUnits unitOfMeasure)
        {
            double pricePerLiter = pricePerUnit/UnitConversionHelper.ConvertVolumeToLiters(1, unitOfMeasure);
            double pricePerUserUnit = pricePerLiter/UnitConversionHelper.ConvertVolumeToUserUnit(1.0);
            return string.Format("{0:C2}/{1}", pricePerUserUnit, FormatVolumeUnitAbbreviation(plural: false));
        }

        public static MvcHtmlString DisplayPriceFor(this HtmlHelper helper, double price)
        {
            return MvcHtmlString.Create(FormatPriceFor(price));
        }

        public static string FormatPriceFor(double price)
        {
            return string.Format("{0:C}", Math.Round(price, 2));
        }

        public static MvcHtmlString DisplayPriceInCentsFor(this HtmlHelper helper, double price)
        {
            return MvcHtmlString.Create(FormatPriceInCentsFor(price));
        }

        public static string FormatPriceInCentsFor(double price)
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

        public static string FormatPriceFor(double price, int decimalPlaces)
        {
            string format = string.Format("{{0:C{0}}}", decimalPlaces);
            return string.Format(format, price);
        }

        public static MvcHtmlString DisplayPriceFor(this HtmlHelper helper, double price, int decimalPlaces)
        {
            return MvcHtmlString.Create(FormatPriceFor(price, decimalPlaces));
        }

        public static string FormatFuelConsumptionAverageFor(double fuelConsumptionAverage)
        {
            double fuelConsumptionInUserUnits = UnitConversionHelper.ConvertConsumptionToUserUnits(fuelConsumptionAverage);

            string averageFuelEfficiencyText;

            switch (UnitConversionHelper.UserUnitOfMeasure)
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
            return MvcHtmlString.Create(FormatFuelConsumptionAverageFor(fuelConsumptionAverage));
        }

        public static string FormatFuelConsumptionAverageWithUnitAbbreviationFor(double fuelConsumptionAverage)
        {
            return string.Format("{0:N} {1}", UnitConversionHelper.ConvertConsumptionToUserUnits(fuelConsumptionAverage), FormatFuelConsumptionAverageAbbreviation());
        }

        public static MvcHtmlString DisplayFuelConsumptionAverageWithUnitAbbreviationFor(this HtmlHelper helper, double fuelConsumptionAverage)
        {
            return MvcHtmlString.Create(FormatFuelConsumptionAverageWithUnitAbbreviationFor(fuelConsumptionAverage));
        }

        public static string FormatFuelConsumptionAverageAbbreviation()
        {
            return UnitConversionHelper.UserUnitOfMeasure == FillupUnits.Litres ? Resources.UserDisplayPreferencesHelper_DisplayFuelConsumptionAverageAbbreviation_l_100km : Resources.UserDisplayPreferencesHelper_DisplayFuelConsumptionAverageAbbreviation_mpg;
        }

        public static MvcHtmlString DisplayFuelConsumptionAverageAbbreviation(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(FormatFuelConsumptionAverageAbbreviation());
        }

        public static string FormatDistanceUnitAbbreviation(bool plural = false)
        {
            switch (UnitConversionHelper.UserDistanceUnit)
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
            return MvcHtmlString.Create(FormatDistanceUnitAbbreviation(plural));
        }

        public static string FormatDistanceWithAbbreviation(int distance)
        {
            var distanceInUserUnits = Math.Round(UnitConversionHelper.ConvertDistanceToUserUnit(distance), 0);

            switch (UnitConversionHelper.UserDistanceUnit)
            {
                case DistanceUnits.Kilometer:
                    return string.Format("{0:N0} {1}", distanceInUserUnits, Math.Abs(distanceInUserUnits - 1.0) > 0.1 ? Resources.UserDisplayPreferencesHelper_DisplayDistanceWithAbbreviation_kilometers : Resources.UserDisplayPreferencesHelper_DisplayDistanceWithAbbreviation_kilometer);
                case DistanceUnits.Mile:
                    return string.Format("{0:N0} {1}", distanceInUserUnits, Math.Abs(distanceInUserUnits - 1.0) > 0.1 ? Resources.UserDisplayPreferencesHelper_DisplayDistanceWithAbbreviation_miles : Resources.UserDisplayPreferencesHelper_DisplayDistanceWithAbbreviation_mile);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
