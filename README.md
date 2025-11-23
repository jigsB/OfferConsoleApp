# 📦 Package Delivery App — README

A C# console application that calculates delivery costs based on weight, distance, and applicable offer codes. The application also manages package shipments and determines optimal combinations for maximum vehicle weight utilization.

---

## 🚀 Overview

The **Package Delivery App** is designed to:

* Calculate delivery cost using weight and distance
* Apply discounts based on valid offer codes
* Display structured delivery summaries
* Manage package shipments
* Determine maximum weight combinations for vehicle utilization

Project follows clean architecture with helper classes, models, and enums under the `OfferConsoleApp` namespace.

---

## ✨ Features

* Interactive console prompts
* Delivery cost calculation
* Offer code validation & discount logic
* Package shipment & combination logic
* Clean formatted output

---

## 🧠 Core Logic

### 1. **User Input**

```csharp
Console.Write("Enter your weight (in kg): ");
double weight = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter the distance (in km): ");
double distance = Convert.ToDouble(Console.ReadLine());
```

---

### 2. **Offer Code Validation**

```csharp
if (Enum.TryParse(offerCode, out OfferCode selectedOffer))
{
    Console.WriteLine($"\nYou selected {selectedOffer} with {(int)selectedOffer}% discount.");
}
else
{
    Console.WriteLine("\nInvalid offer code.");
}
```

---

### 3. **Cost Calculation**

Formula:

```
Delivery Cost = BaseCost + (Weight * 10) + (Distance * 5)
```

Code:

```csharp
double deliveryCost = DeliveryCost.CalculateCost(GlobalValues.BaseDeliveryCost, weight, distance);
```

---

### 4. **Discount Application**

```csharp
double discount = DeliveryCost.GetDiscount(deliveryCost, selectedOffer, weight, distance);
```

Discount is applied based on offer eligibility (weight & distance conditions).

---

### 5. **Formatted Output**

```csharp
DeliveryCost.DisplayResult(GlobalValues.BaseDeliveryCost, weight, distance, offerCode, deliveryCost, discount);
```

Example:

```
Base delivery cost: 100
Weight: 5kg | Distance: 20km
Offer Code: OFR001
Total Cost: 300
Discount: 30
Final Cost: 270
```

---

### 6. **Package Shipment Management**

```csharp
List<Package> avlPackages = PackageShipment.GetPackage();
var combinations = PackageShipment.GetMaxCombinedWeight(avlPackages);
```

* Retrieves packages
* Determines best combinations for vehicle capacity
* Shows remaining packages and vehicles

---

## 📁 Project Structure

```
OfferConsoleApp/
├── Program.cs
├── Common/
│   ├── GlobalValues.cs
│   ├── DeliveryCost.cs
│   ├── PackageShipment.cs
│   └── OfferCode.cs
└── Models/
    └── Package.cs
```

---

## ▶️ How to Run

### **Prerequisites**

* Visual Studio 2022+
* .NET 6.0+

### **Steps**

1. Open solution in Visual Studio
2. Set **OfferConsoleApp** as startup project
3. Press **Ctrl + F5**
4. Follow console instructions

---

## 🧪 Running Test Cases (Code Coverage)

Open terminal in `DeliveryAppTest` project folder:

1. Generate coverage:

```
dotnet test --collect:"XPlat Code Coverage"
```

2. Generate HTML report:

```
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

3. Open:

```
coveragereport/index.html
```

---

## 📝 Example Run

```
=== Delivery Cost Calculator ===
Enter your weight (in kg): 10
Enter the distance (in km): 50
Enter Offer Code (e.g. OFR001): OFR002

You selected OFR002 with 10% discount.

Base delivery cost: 100
Weight: 10kg | Distance: 50km
Offer Code: OFR002
Discount: 75
Total Cost: 675

=== Package Shipment Details ===
----------------------------------------
Packages Remaining: 2
Vehicles Available: 3 | Current Time: 09:00
```

---

## 🔧 Extending the Project

You can easily extend functionality by:

* Adding offer codes in `OfferCode.cs`
* Updating cost formula in `DeliveryCost`
* Adjusting package/vehicle logic in `PackageShipment.cs`

---

## 📌 Developer Notes

* Use `GlobalValues` to manage configurable constants
* Add input validation wherever needed
* Follow consistent C# naming conventions (PascalCase, camelCase)
