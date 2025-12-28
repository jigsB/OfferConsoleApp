using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OfferConsoleApp.Common
{
    public static class GlobalValues
    {
        public static readonly double BaseDeliveryCost = 100; // base charge
        public static string NoDiscountMessage = "(Offer not applicable as criteria not met)";
        public static string AppliedDiscountMessage = "({0}% of {1} i.e; Delivery Cost)";
        public static double AvailableVehicle = 2;
        public static double CurrentTime = 0; 
        public static double MaxSpeed = 70; // km/hr
        public static double MaxLoad = 200; // in kg

    }

}
