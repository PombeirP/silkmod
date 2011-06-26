using System;
using MileageStats.Model;

namespace MileageStats.Web.Helpers
{
    public static class FuelConsumptionHelper
    {
        private const double LitersPerUsGallon = 3.78541178;
        private const double LitersPerImperialGallon = 4.54609188;
        private const double KilometersPerMile = 1.609344;

        public static FillupUnits UserUnitOfMeasure = FillupUnits.Litres;
        public static DistanceUnits UserDistanceUnit = DistanceUnits.Kilometer;

        public static double ConvertDistanceToUserUnit(double distanceInKm)
        {
            switch (UserDistanceUnit)
            {
                case DistanceUnits.Kilometer:
                    return distanceInKm;
                case DistanceUnits.Mile:
                    return distanceInKm / KilometersPerMile;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static double ConvertDistanceRatioToUserUnit(double distanceRatioInKms)
        {
            switch (UserDistanceUnit)
            {
                case DistanceUnits.Kilometer:
                    return distanceRatioInKms;
                case DistanceUnits.Mile:
                    return distanceRatioInKms * KilometersPerMile;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static double ConvertVolumeToUserUnit(double volumeInLiters)
        {
            switch (UserUnitOfMeasure)
            {
                case FillupUnits.ImperialGallons:
                    return volumeInLiters / LitersPerImperialGallon;
                case FillupUnits.UsGallons:
                    return volumeInLiters / LitersPerUsGallon;
                case FillupUnits.Litres:
                    return volumeInLiters;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static double ConvertVolumeToLiters(double volume, FillupUnits unitOfMeasure)
        {
            switch (unitOfMeasure)
            {
                case FillupUnits.ImperialGallons:
                    return volume * LitersPerImperialGallon;
                case FillupUnits.UsGallons:
                    return volume * LitersPerUsGallon;
                case FillupUnits.Litres:
                    return volume;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static double ConvertConsumptionToUserUnits(double fuelConsumptionInKmPerLiter)
        {
            switch (UserUnitOfMeasure)
            {
                case FillupUnits.ImperialGallons:
                case FillupUnits.UsGallons:
                    return Math.Round(ConvertDistanceToUserUnit(fuelConsumptionInKmPerLiter) * ConvertVolumeToLiters(1, UserUnitOfMeasure));
                case FillupUnits.Litres:
                    return Math.Round(100 / fuelConsumptionInKmPerLiter, 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum DistanceUnits
    {
        Kilometer,
        Mile
    }
}