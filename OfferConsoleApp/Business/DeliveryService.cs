using OfferConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OfferConsoleApp.Business
{
   
    public class DeliveryService
    {
        public void DeliverPackages(
            List<Package> packages,
            List<Vehicle> vehicles)
        {
            double currentTime = 0;

            while (packages.Any())
            {
                // Get next available vehicle
                var vehicle = vehicles
                    .OrderBy(v => v.AvailableAt)
                    .First();

                currentTime = vehicle.AvailableAt;

                // Select packages that fit by weight
                double totalWeight = 0;
                var selectedPackages = new List<Package>();

                // Greedy selection: lowest first
                foreach (var pkg in packages.OrderBy(p => p.Weight))
                {
                    if (totalWeight + pkg.Weight <= vehicle.MaxLoad)
                    {
                        selectedPackages.Add(pkg);
                        totalWeight += pkg.Weight;
                    }
                }

                if (!selectedPackages.Any())
                    break;

                // Delivery time based on farthest package
                double maxDistance = selectedPackages.Max(p => p.Distance);
                double deliveryTime = maxDistance / vehicle.MaxSpeed;

                Console.WriteLine($"\n{vehicle.Id}");
                Console.WriteLine($"Packages: {string.Join(", ", selectedPackages.Select(p => p.Id))}");
                Console.WriteLine($"Total Weight: {totalWeight} kg");
                Console.WriteLine($"Delivery Time: {deliveryTime:F2} hrs");

                // Vehicle returns after round trip
                vehicle.AvailableAt = currentTime + (deliveryTime * 2);

                Console.WriteLine($"{vehicle.Id} available after {vehicle.AvailableAt:F2} hrs");

                // Remove delivered packages
                foreach (var pkg in selectedPackages)
                {
                    packages.Remove(pkg);
                }

            }
        }
    }

}
