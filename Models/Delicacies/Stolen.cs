using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy, IDelicacy
    {
        private const double stolenPrice = 3.5d;

        public Stolen(string delicacyName) : base(delicacyName, stolenPrice)
        {
        }
    }
}
