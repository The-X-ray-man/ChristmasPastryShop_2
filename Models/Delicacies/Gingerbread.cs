using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy, IDelicacy
    {
        private const double gingerbreadPrice = 4.0d;

        public Gingerbread(string delicacyName) : base(delicacyName, gingerbreadPrice)
        {
        }
    }
}
