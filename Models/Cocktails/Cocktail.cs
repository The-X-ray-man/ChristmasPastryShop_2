using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail
    {
        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public string Name 
        { 
            get => Name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                Name = value;
            }
        }
        public string Size { get; private set; }
        public double Price 
        {
            get => Price;
            private set 
            { 
                switch (Size)
                {
                    case "Large":
                        Price = Price;
                        break;
                    case "Middle":
                        Price = Price * 2 / 3;
                        break;
                    case "Small":
                        Price = Price / 3;
                        break;
                }
            }
        }
        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:d2} lv";
        }
    }
}
