using OfferConsoleApp.Models;
using OfferConsoleApp.Utils;

namespace OfferConsoleApp.Business
{
    public static class PackageShipment
    {

        //Get available packages
        public static List<Package> GetPackage()
        {
            List<Package> package = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 50, Distance =30},
                new Package { Id = "PKG2", Weight = 75, Distance =125},
                new Package { Id = "PKG3", Weight = 175, Distance =100},
                new Package { Id = "PKG4", Weight = 110, Distance =60 },
                new Package { Id = "PKG5", Weight = 155, Distance =95 },
            };
            return package;

        }

        // Metod to get maximum weight packages
        public static List<(string Pkg1, string Pkg2, double TotalWeight, List<Package> packageDetails)> GetMaxCombinedWeight(List<Package> packages)
        {
            List<Package> MaxPackage = new List<Package>();
            List<Package> packageDtls = new List<Package>();
            double maxWeight = 0;
            string pkgs1 = string.Empty;
            string pkgs2 = string.Empty;
            var result = new List<(string, string, double, List<Package>)>();

            foreach (var pkg in packages)
            {

                if (pkg.Weight >= 200)
                {
                    MaxPackage.Add(pkg);
                }
                else
                {
                    foreach (var pkg1 in packages)
                    {
                        if (pkg != pkg1)
                        {
                            double total = pkg.Weight + pkg1.Weight;
                            if (total <= 200)
                            {
                                if (total > maxWeight)
                                {
                                    maxWeight = total;
                                    pkgs1 = pkg.Id;
                                    pkgs2 = pkg1.Id;

                                    if (!packageDtls.Contains(pkg))
                                    {
                                        packageDtls.Add(pkg);
                                    }
                                    if (!packageDtls.Contains(pkg1))
                                    {
                                        packageDtls.Add(pkg1);
                                    }
                                    result.Add((pkg.Id, pkg1.Id, total, packageDtls));
                                }
                            }
                        }
                    }
                }
            }
            return result;

        }

        //Display Estimation time 
        public static void GetEstimationTimeDisplay(List<Package> packages, string Package1, string Package2, double totalTime)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("vehicle01");

            foreach (var item in packages)
            {
                if (item.Id == Package1 || (item.Id == Package2))
                {
                    double estimatedTime = 0;
                    var distance = item.Distance;
                    estimatedTime = item.Distance / GlobalValues.MaxSpeed; // Formula: Time = Distance / Speed
                    int hours = (int)estimatedTime;
                    int minutes = (int)((estimatedTime - hours) * 60);
                    Console.WriteLine($"Delivering {item.Id}\t {estimatedTime} hrs");
                    Console.WriteLine($"    {item.Distance}km / {GlobalValues.MaxSpeed}km/hr");
                    Console.WriteLine($"\n vehicle01 will be available after (2 * {totalTime}) = {2 * totalTime} hrs");
                }
            }
        }

        //calculate estimate time to delivered packages
        public static double GetEstimationTime(List<Package> packages, string Package1, string Package2)
        {
            double estimatedTime = 0;
            Package package1 = packages.FirstOrDefault(w => w.Id == Package1);
            Package package2 = packages.FirstOrDefault(w => w.Id == Package2);
            double time1 = package1.Distance / GlobalValues.MaxSpeed; // Formula: Time = Distance / Speed
            double time2 = package2.Distance / GlobalValues.MaxSpeed; // Formula: Time = Distance / Speed
            estimatedTime = time1 + time2;
            return estimatedTime;
        }

        //Return remaining packages after dilivered packages
        public static List<Package> RemoveDeliveredPackages(List<Package> packages, string Package1, string Package2)
        {
            packages.RemoveAll(p => p.Id == Package1 || p.Id == Package2);

            return packages;
        }
    }
}
