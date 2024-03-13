using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy
    {
        public Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
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
        public double Price { get; private set; }
        public override string ToString()
        {
            return $"{Name} - {Price:d2} lv";
        }
    }
}
