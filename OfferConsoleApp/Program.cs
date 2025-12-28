using OfferConsoleApp.Business;
using OfferConsoleApp.Data;
using OfferConsoleApp.Utils;

Console.WriteLine("=== Delivery Cost Calculator ===");

Console.Write("Enter your weight (in kg): ");
double weight = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter the distance (in km): ");
double distance = Convert.ToDouble(Console.ReadLine());

// Print Offer code
DeliveryCost.DisplayAvailableOffer();
Console.Write("Enter Offer Code (e.g. OFR001): ");
string offerCode = Console.ReadLine().ToUpper();

if (Enum.TryParse(offerCode, out OfferCode selectedOffer))
{
    Console.WriteLine($"\nYou selected {selectedOffer} with {(int)selectedOffer}% discount.");
}
else
{
    Console.WriteLine("\nInvalid offer code.");
}

// Use common class methods to calculate cost.
double deliveryCost = DeliveryCost.CalculateCost(GlobalValues.BaseDeliveryCost, weight, distance);

// Calculate logic for apply discount
double discount = DeliveryCost.GetDiscount(deliveryCost, selectedOffer, weight, distance);

// Print formatted result
DeliveryCost.DisplayResult(GlobalValues.BaseDeliveryCost, weight, distance, offerCode, deliveryCost, discount);

Console.WriteLine("=== Package Shipment Details ===");
Console.WriteLine(new string('-', 40));

var service = new DeliveryService();
var packages = DummyDatabase.Packages;
var vehicles = DummyDatabase.Vehicles;
service.DeliverPackages(packages, vehicles);

Console.WriteLine("\nAll deliveries completed.");
Console.ReadLine();

