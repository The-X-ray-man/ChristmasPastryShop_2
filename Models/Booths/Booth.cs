using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.WebSockets;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private double currentBill;
        private double turnover;
        private IRepository<ICocktail> cocktailRepository;
        private IRepository<IDelicacy> delicacyRepositoty;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
            cocktailRepository = new CocktailRepository();
            delicacyRepositoty = new DelicacyRepository();
        }

        public int BoothId { get; private set; }

        public int Capacity
        {
            get 
            {
                return capacity;
            }
            private set
            {
                if (value < 1) throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyRepositoty;

        public IRepository<ICocktail> CocktailMenu => cocktailRepository;

        public double CurrentBill { get => currentBill; private set => currentBill = value; }

        public double Turnover { get => turnover; private set => turnover = value; }

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            IsReserved = !IsReserved;
        }

        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach (var cocktail in cocktailRepository.Models) sb.AppendLine($"--{cocktail.ToString()}");
            sb.AppendLine($"-Delicacy menu:");
            foreach (var delicacy in delicacyRepositoty.Models) sb.AppendLine($"--{delicacy.ToString()}");
            return sb.ToString().TrimEnd();
        }
    }
}
