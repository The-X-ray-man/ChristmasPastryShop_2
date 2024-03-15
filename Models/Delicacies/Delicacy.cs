using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy
    {
        private string name;
        private double price;
        public Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
            Price = price;
        }

        public string Name 
        {  
            get => name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                name = value;
            }
        }
        public double Price { get => price; private set => price = value; }
        public override string ToString()
        {
            return $"{Name} - {Price:d2} lv";
        }
    }
}
