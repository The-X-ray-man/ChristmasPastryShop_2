using ChristmasPastryShop.Models.Cocktails.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Hibernation : Cocktail, ICocktail
    {
        private const double mulledWine = 10.5;

        public Hibernation(string cocktailName, string size) : base(cocktailName, size, mulledWine)
        {
        }
    }
}
