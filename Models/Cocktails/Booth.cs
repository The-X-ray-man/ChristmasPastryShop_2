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

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Booth : IBooth
    {
        private IRepository<ICocktail> _cocktailRepository;
        private IRepository<IDelicacy> _delicacyRepositoty;
        
        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
            _cocktailRepository = new CocktailRepository();
            _delicacyRepositoty = new DelicacyRepository();
        }

        public int BoothId {get; private set;}

        public int Capacity 
        { 
            get => Capacity;
            private set 
            {
                if (value < 1) throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                Capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => _delicacyRepositoty;

        public IRepository<ICocktail> CocktailMenu => _cocktailRepository;

        public double CurrentBill {  get; private set; }

        public double Turnover { get; private set; }

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
    }
}
