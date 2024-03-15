using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            //throw new NotImplementedException();
            int boothCount = booths.Models.Count;
            booths.AddModel(new Booth(boothCount+1, capacity));
            return string.Format(OutputMessages.NewBoothAdded, booths.Models.Count, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            //throw new NotImplementedException();
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine)) return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            if (size != "Large" &&  size != "Middle" && size != "Small") return string.Format(OutputMessages.InvalidCocktailSize, size);
            if (booths.Models.Any(x => x.CocktailMenu.Models.Any(y =>  y.Name == cocktailName))) return string.Format(OutputMessages.CocktailAlreadyAdded, cocktailName);
            IBooth selectedBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            ICocktail newCocktail;
            if (cocktailTypeName == nameof(Hibernation))
            {
                newCocktail = new Hibernation(cocktailName, size);
            }
            else
            {
                newCocktail = new MulledWine(cocktailName, size);
            }
            selectedBooth.CocktailMenu.AddModel(newCocktail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            //throw new NotImplementedException();
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen)) return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            if (booths.Models.Any(x => x.DelicacyMenu.Models.Any(y => y.Name == delicacyName))) return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            IBooth selectedBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            IDelicacy newDelicacy;
            if (delicacyTypeName == nameof(Gingerbread))
            {
                 newDelicacy = new Gingerbread(delicacyName);
            }
            else 
            {
                newDelicacy = new Stolen(delicacyName);
            }
            selectedBooth.DelicacyMenu.AddModel(newDelicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            //throw new NotImplementedException();
            return booths.Models.FirstOrDefault(x => x.BoothId == boothId).ToString();
        }

        public string LeaveBooth(int boothId)
        {
            //throw new NotImplementedException();
            IBooth selectedBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.GetBill, string.Format($"{selectedBooth.CurrentBill:d2}")));
            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, boothId));
            selectedBooth.Charge();
            if (selectedBooth.IsReserved) selectedBooth.ChangeStatus();
            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            //throw new NotImplementedException();
            IBooth selectedBooth = booths.Models
                .Where(x => x.IsReserved == false && x.Capacity >= countOfPeople)
                .OrderBy(x => x.Capacity).ThenByDescending(x => x.BoothId)
                .FirstOrDefault();
            if (selectedBooth == null) return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            selectedBooth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, selectedBooth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            //throw new NotImplementedException();
            string[] tokens = order.Split('/');
            string itemTypeName = tokens[0];
            string itemName = tokens[1];
            int itemCount = int.Parse(tokens[2]);
            string itemSize;
            bool isCocktail = false;
            double itemPrice;
            if (itemTypeName != nameof(Hibernation) 
                && itemTypeName != nameof(MulledWine) 
                && itemTypeName != nameof(Gingerbread)
                && itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }
            IBooth selectedBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            if (itemTypeName != nameof(Hibernation) && itemTypeName != nameof(MulledWine)
                && itemTypeName != nameof(Gingerbread) && itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }
            if (!selectedBooth.CocktailMenu.Models.Any(x => x.Name == itemName)
                && !selectedBooth.DelicacyMenu.Models.Any(x => x.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }
            if (itemTypeName == nameof(Hibernation) || itemTypeName == nameof(MulledWine)) isCocktail = true;
            if (isCocktail)
            {
                itemSize = tokens[3];
                if (!selectedBooth.CocktailMenu.Models.Any(x => x.Name == itemName && x.Size == itemSize)) return string.Format(OutputMessages.CocktailStillNotAdded, itemSize, itemName);
                itemPrice = selectedBooth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName && x.Size == itemSize).Price;
            }
            else
            {
                if (!selectedBooth.DelicacyMenu.Models.Any(x => x.Name == itemName)) return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                itemPrice = selectedBooth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName).Price;
            }
            selectedBooth.UpdateCurrentBill(itemPrice * itemCount);
            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, itemCount, itemName);
        }
    }
}
