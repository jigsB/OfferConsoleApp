using OfferConsoleApp.Utils;

namespace OfferConsoleApp.Models
{
    public class DiscountRule
    {
        public OfferCode OfferCode { get; set; }
        public double MaxWeight { get; set; }
        public double MinDistance { get; set; }
        public double MaxDistance { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
