namespace MileageStats.Web.Models
{
    public class JsonFleetStatisticsViewModel
    {
        public string AverageFillupPrice { get; set; }

        /// <summary>
        /// Gets the average fuel efficiency (e.g. Miles/Gallon or Liters/100km)
        /// </summary>
        public string AverageFuelEfficiency { get; set; }

        /// <summary>
        /// Gets the average cost to drive per distance (e.g. $/Mile or €/Kilometer)
        /// </summary>
        public string AverageCostToDrive { get; set; }

        /// <summary>
        /// Gets the average cost to drive per month (e.g. $/Month or €/Month) between the first entry and today.
        /// </summary>
        public string AverageCostPerMonth { get; set; }

        /// <summary>
        /// Gets the highest odometer value recorded.
        /// </summary>
        public string Odometer { get; set; }

        /// <summary>
        /// Gets the total vehicle distance traveled for fillup entries.
        /// </summary>
        public int TotalDistance { get; set; }

        /// <summary>
        /// Gets the total cost of all fillup entries, not including transaction fees.
        /// </summary>
        public double TotalFuelCost { get; set; }

        /// <summary>
        /// Gets the total units consumed based on all fillup entries.
        /// </summary>
        public double TotalUnits { get; set; }

        /// <summary>
        /// Gets the total cost of all fillup entries including transaction fees and service entries.
        /// </summary>
        public double TotalCost { get; set; }
    }
}