using OfferConsoleApp.Models;
using OfferConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferConsoleApp.Data
{
    public static class DummyDatabase
    {


        public static readonly List<Package> Packages = new List<Package>
        {
             new Package { Id = "PKG1", Weight = 50,  Distance = 30 },
            new Package { Id = "PKG2", Weight = 75,  Distance = 125 },
            new Package { Id = "PKG3", Weight = 175, Distance = 100 },
            new Package { Id = "PKG4", Weight = 110, Distance = 60 },
            new Package { Id = "PKG5", Weight = 155, Distance = 95 }
        };


        public static readonly List<Vehicle> Vehicles = new List<Vehicle>
        {
            new Vehicle { Id = "Vehicle 01", MaxSpeed = 70,  MaxLoad = 200 },
            new Vehicle { Id = "Vehicle 02", MaxSpeed = 70,  MaxLoad = 200 }
        };

        public static readonly List<DiscountRule> DiscountRules = new()
{
    new DiscountRule
    {
        OfferCode = OfferCode.OFR001,
        MaxWeight = 70,
        MinDistance = 0,
        MaxDistance = 200,
        DiscountPercentage = 0.10
    },
    new DiscountRule
    {
        OfferCode = OfferCode.OFR002,
        MaxWeight = 100,
        MinDistance = 50,
        MaxDistance = 150,
        DiscountPercentage = 0.07
    },
    new DiscountRule
    {
        OfferCode = OfferCode.OFR003,
        MaxWeight = 200,
        MinDistance = 50,
        MaxDistance = 250,
        DiscountPercentage = 0.05
    }
};

    }
}
