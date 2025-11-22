using OfferConsoleApp;
using OfferConsoleApp.Common;

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
List<Package> avlPackages = PackageShipment.GetPackage();

var combinations = PackageShipment.GetMaxCombinedWeight(avlPackages);

Console.WriteLine("Packages Remaining: " + $"{avlPackages.Count}");
Console.WriteLine($"Vehicles Available: {GlobalValues.AvailableVehicle}  |  Current Time: {GlobalValues.CurrentTime}");


Console.WriteLine(new string('-', 40));
Console.WriteLine(new string('-', 40));
foreach (var combo in combinations)
{
    double weight1 = combo.packageDetails.FirstOrDefault(w => w.Name == combo.Pkg1).Weight;
    double weight2 = combo.packageDetails.FirstOrDefault(w => w.Name == combo.Pkg2).Weight;
    Console.WriteLine($"{combo.Pkg1}({weight1} kg) + {combo.Pkg2}({weight2} kg) = {combo.TotalWeight} kg");
}

var maxCombo = combinations.OrderByDescending(x => x.TotalWeight).First();

Console.WriteLine($"\nMax combined weight: {maxCombo.TotalWeight} kg ({maxCombo.Pkg1} + {maxCombo.Pkg2})");

double totalTime = PackageShipment.GetEstimationTime(combinations.FirstOrDefault().packageDetails, maxCombo.Pkg1, maxCombo.Pkg2);

PackageShipment.GetEstimationTimeDisplay(combinations.FirstOrDefault().packageDetails, maxCombo.Pkg1, maxCombo.Pkg2, totalTime);
PackageShipment.RemoveDeliveredPackages(combinations.FirstOrDefault().packageDetails, maxCombo.Pkg1, maxCombo.Pkg2);
Console.ReadLine();

