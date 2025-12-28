using OfferConsoleApp.Data;
using OfferConsoleApp.Models;
using OfferConsoleApp.Utils;

namespace OfferConsoleApp.Business
{
    public static class DeliveryCost
    {
        public const double WeightRate = 10;
        public const double DistanceRate = 5;

        // Method to calculate delivery cost
        public static double CalculateCost(double baseCost, double weight, double distance)
        {
            return baseCost + (weight * WeightRate) + (distance * DistanceRate);
        }

        // Method to calculate discount
        public static double GetDiscount(double cost, OfferCode offerCode, double weight, double distance)
        {

            var rule = DummyDatabase.DiscountRules.FirstOrDefault(r =>
         r.OfferCode == offerCode &&
         weight <= r.MaxWeight &&
         distance >= r.MinDistance &&
         distance <= r.MaxDistance);

            return rule == null ? 0 : cost * rule.DiscountPercentage;
        }

        // Method to display available offer
        public static void DisplayAvailableOffer()
        {
            Console.WriteLine("Available Offer Codes:\n");
            foreach (OfferCode offer in Enum.GetValues(typeof(OfferCode)))
            {
                Console.WriteLine($"{offer} - {(int)offer}% discount");
            }
        }


        // Method to display formatted output
        public static void DisplayResult(double baseCost, double weight, double distance, string offerCode, double deliveryCost, double discount)
        {
            string discountMessage = string.Empty;
            double total = deliveryCost - discount;

            Console.WriteLine($"Base.delivery cost: {baseCost}");
            Console.WriteLine($"Weight: {weight}kg  |  Distance: {distance}km");
            Console.WriteLine($"Offer code: {offerCode}");
            Console.WriteLine();

            discountMessage = discount > 0 ? string.Format(GlobalValues.AppliedDiscountMessage, (int)OfferCode.OFR001, deliveryCost)
                                        : GlobalValues.NoDiscountMessage;

            Console.WriteLine("Delivery Cost".PadRight(25) + $"{deliveryCost,10:0.00}");
            Console.WriteLine($"{baseCost} + ({weight} * {WeightRate}) + ({distance} * {DistanceRate})");
            Console.WriteLine(new string('-', 40));

            Console.WriteLine("Discount".PadRight(25) + $"{-discount,10:0.00}");
            Console.WriteLine(discountMessage);
            Console.WriteLine();

            Console.WriteLine("Total cost".PadRight(25) + $"{total,10:0.00}");
        }
    }
}
