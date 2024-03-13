using ChristmasPastryShop.Models.Cocktails.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine : Cocktail, ICocktail
    {
        private const double mulledWine = 13.5;

        public MulledWine(string cocktailName, string size) : base(cocktailName, size, mulledWine)
        {
        }
    }
}
