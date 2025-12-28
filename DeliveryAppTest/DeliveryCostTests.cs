using global::OfferConsoleApp;
using OfferConsoleApp.Business;
using OfferConsoleApp.Utils;

namespace DeliveryAppTest.Tests
{
    public class DeliveryCostTests
    {
        [Theory]
        [InlineData(100, 5, 10, 200)] // base + (5*10) + (10*5)
        [InlineData(100, 2, 3, 135)]   // base + (2*10) + (3*5)
        public void CalculateCost_ValidInput_ReturnsExpected(double baseCost, double weight, double distance, double expected)
        {
            // Act
            double result = DeliveryCost.CalculateCost(baseCost, weight, distance);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(200, OfferCode.OFR001, 70, 100, 10)] // Suppose OFR001 gives 10% discount
        [InlineData(200, OfferCode.OFR002, 110, 150, 0)]  // Not eligible for discount
        public void GetDiscount_ValidOffers_ReturnsCorrectDiscount(double deliveryCost, OfferCode offerCode, double weight, double distance, double expectedDiscountPercent)
        {
            // Act
            double discount = DeliveryCost.GetDiscount(deliveryCost, offerCode, weight, distance);

            // Assert
            double expected = deliveryCost * expectedDiscountPercent / 100;
            Assert.Equal(expected, discount, 1);
        }

        [Fact]
        public void DisplayAvailableOffer_PrintsAllOffersAndDiscounts()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            DeliveryCost.DisplayAvailableOffer();
            var output = sw.ToString();

            // Assert
            Assert.Contains("Available Offer Codes:", output);

            // Check each enum value appears in output
            foreach (OfferCode offer in Enum.GetValues(typeof(OfferCode)))
            {
                string expectedLine = $"{offer} - {(int)offer}% discount";
                Assert.Contains(expectedLine, output);
            }
        }

        [Fact]
        public void DisplayAvailableOffer_OutputHasHeaderFirst()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            DeliveryCost.DisplayAvailableOffer();
            var output = sw.ToString().Trim();

            // Assert: header should appear before any offer lines
            var firstLine = output.Split('\n', StringSplitOptions.RemoveEmptyEntries)[0];
            Assert.StartsWith("Available Offer Codes", firstLine);
        }
        [Fact]
        public void DisplayResult_WithDiscount_PrintsExpectedOutput()
        {
            // Arrange
            double baseCost = 100;
            double weight = 10;
            double distance = 20;
            string offerCode = "OFR001";
            double deliveryCost = 250; // 100 + (10*10) + (20*5)
            double discount = 25;      // 10% discount example

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            DeliveryCost.DisplayResult(baseCost, weight, distance, offerCode, deliveryCost, discount);

            // Assert
            string output = sw.ToString();

            Assert.Contains("Base.delivery cost: 100", output);
            Assert.Contains("Weight: 10kg", output);
            Assert.Contains("Distance: 20km", output);
            Assert.Contains("Offer code: OFR001", output);
            Assert.Contains("Delivery Cost", output);
            Assert.Contains("Discount", output);
            Assert.Contains("Total cost", output);
        }

        [Fact]
        public void DisplayResult_WithoutDiscount_PrintsNoDiscountMessage()
        {
            // Arrange
            double baseCost = 50;
            double weight = 5;
            double distance = 10;
            string offerCode = "OFR002";
            double deliveryCost = 125; // 50 + (5*10) + (10*5)
            double discount = 0;

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            DeliveryCost.DisplayResult(baseCost, weight, distance, offerCode, deliveryCost, discount);

            // Assert
            string output = sw.ToString();

            Assert.Contains("Base.delivery cost: 50", output);
            Assert.Contains("Offer code: OFR002", output);
            Assert.Contains("Offer not applicable as criteria not met", output);
            Assert.Contains("Total cost", output);
            Assert.Contains("125.00", output); // total displayed
        }

    }
}

