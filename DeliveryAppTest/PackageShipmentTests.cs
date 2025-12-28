using global::OfferConsoleApp;
using global::OfferConsoleApp.Utils;
using OfferConsoleApp.Business;
using OfferConsoleApp.Models;

namespace DeliveryAppTest.Tests
{
    public class PackageShipmentTests
    {
        [Fact]
        public void GetPackage_ReturnsListOfPackages()
        {
            // Act
            var result = PackageShipment.GetPackage();

            // Assert
            Assert.NotNull(result);
            Assert.All(result, p => Assert.False(string.IsNullOrEmpty(p.Id)));
        }

        [Fact]
        public void GetMaxCombinedWeight_ReturnsExpectedCombinations()
        {
            // Arrange
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 30 },
                new Package { Id = "PKG2", Weight = 40 },
                new Package { Id = "PKG3", Weight = 20 }
            };

            // Act
            var combinations = PackageShipment.GetMaxCombinedWeight(packages);

            // Assert
            Assert.NotEmpty(combinations);
            Assert.True(combinations.All(c => c.TotalWeight > 0));
        }

        [Fact]
        public void GetEstimationTime_ReturnsPositiveTime()
        {
            // Arrange
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 10, Distance = 60 },
                new Package { Id = "PKG2", Weight = 15, Distance = 80 }
            };

            // Act
            double result = PackageShipment.GetEstimationTime(packages, "PKG1", "PKG2");

            // Assert
            Assert.True(result > 0);
        }

        [Fact]
        public void GetMaxCombinedWeight_WhenPackagesWithinLimit_ReturnsCorrectCombination()
        {
            // Arrange
            List<Package> packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 50, Distance =30},
                new Package { Id = "PKG2", Weight = 120, Distance =125},
                new Package { Id = "PKG3", Weight = 100, Distance =100},
            };
            // Act
            var result = PackageShipment.GetMaxCombinedWeight(packages);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            // Find the max combination
            var maxCombo = result.OrderByDescending(r => r.TotalWeight).First();

            Assert.Equal("PKG1", maxCombo.Pkg1);
            Assert.Equal("PKG2", maxCombo.Pkg2);
            Assert.True(maxCombo.TotalWeight <= 200);
        }

        [Fact]
        public void GetMaxCombinedWeight_WhenSinglePackageExceeds200_AddsToMaxPackageList()
        {
            // Arrange
            var packages = PackageShipment.GetPackage();

            // Act
            var result = PackageShipment.GetMaxCombinedWeight(packages);

            // Assert
            // The heavy package should not create valid combinations under 200
            Assert.All(result, combo => Assert.True(combo.TotalWeight <= 200));
        }

        [Fact]
        public void GetMaxCombinedWeight_WhenAllOverLimit_ReturnsEmptyList()
        {

            // Arrange
            List<Package> packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 250, Distance =30},
                new Package { Id = "PKG2", Weight = 175, Distance =125},
            };

            // Act
            var result = PackageShipment.GetMaxCombinedWeight(packages);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // since no valid pairs ≤ 200
        }

        [Fact]
        public void GetMaxCombinedWeight_WhenNoPackages_ReturnsEmptyList()
        {
            // Arrange
            var packages = new List<Package>();

            // Act
            var result = PackageShipment.GetMaxCombinedWeight(packages);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public void GetEstimationTimeDisplay_ValidPackages_PrintsExpectedOutput()
        {
            // Arrange
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Distance = 70 },
                new Package { Id = "PKG2", Distance = 140 },
                new Package { Id = "PKG3", Distance = 90 }
            };

            string pkg1 = "PKG1";
            string pkg2 = "PKG2";
            double totalTime = 2.0;

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            PackageShipment.GetEstimationTimeDisplay(packages, pkg1, pkg2, totalTime);

            // Assert
            var output = sw.ToString();

            // Verify expected console lines
            Assert.Contains("vehicle01", output);
            Assert.Contains("Delivering PKG1", output);
            Assert.Contains("Delivering PKG2", output);
            Assert.Contains("70km /", output);
            Assert.Contains("140km /", output);
            Assert.Contains("vehicle01 will be available after (2 * 2)", output);
        }

        [Fact]
        public void GetEstimationTimeDisplay_WhenPackagesNotFound_PrintsOnlyHeader()
        {
            // Arrange
            var packages = new List<Package>
            {
                new Package { Id = "PKG5", Distance = 200 }
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            PackageShipment.GetEstimationTimeDisplay(packages, "PKG1", "PKG2", 1.5);

            // Assert
            var output = sw.ToString();
            Assert.Contains("vehicle01", output); // header always printed
            Assert.DoesNotContain("Delivering", output); // no package matches
        }

        [Fact]
        public void RemoveDeliveredPackages_WhenPackagesExist_RemovesSpecifiedOnes()
        {
            // Arrange
            var packages = PackageShipment.GetPackage();

            // Act
            var remaining = PackageShipment.RemoveDeliveredPackages(packages, "PKG1", "PKG2");

            // Assert
            Assert.Equal(3, remaining.Count);
            Assert.DoesNotContain(remaining, p => p.Id == "PKG1");
            Assert.DoesNotContain(remaining, p => p.Id == "PKG2");
            Assert.Contains(remaining, p => p.Id == "PKG3");
        }

        [Fact]
        public void RemoveDeliveredPackages_WhenEmptyList_ReturnsEmptyList()
        {
            // Arrange
            List<Package> packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 50, Distance =30},
                new Package { Id = "PKG2", Weight = 75, Distance =125},
            };
            // Act
            var remaining = PackageShipment.RemoveDeliveredPackages(packages, "PKG1", "PKG2");

            // Assert
            Assert.Empty(remaining);
        }
    }
}


