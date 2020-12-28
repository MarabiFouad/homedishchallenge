using HomeDishTest.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public class QuantityValue
    {
        public int Oty
        {
            get; set;
        }
        public int ForeseenQty { get; set; }
    }
    public class BasketCalculationService : IBasketCalculationService
    {

        public BasketCalculationService()
        {
        }

        public double MinimumGrandTotal(Basket basket)
        {

            if (basket == null) throw new ApiException("Basket cannot be empty.");

            foreach (var special in basket.Specials)
            {
                var includedProduct = basket.Products
                   .Where(p => special.Products.Any(sp => sp.Name == p.Name));

                var Qties = new Dictionary<string, QuantityValue>(
                   includedProduct.Select(x => new KeyValuePair<string, QuantityValue>(x.Name, new QuantityValue()
                   {
                       Oty = x.Quantity,
                       ForeseenQty = x.Quantity
                   }))
                   );
                int numOfApplies = 0;
                while (Qties.All(x => x.Value.ForeseenQty >= 0))
                {
                    numOfApplies++;
                    foreach (var qty in Qties)
                    {
                        qty.Value.Oty = qty.Value.ForeseenQty;
                    }
                    foreach (var specialOffer in special.Products)
                    {
                        
                        Qties[specialOffer.Name].ForeseenQty = Qties[specialOffer.Name].Oty - specialOffer.Quantity;
                    }

                }
                foreach (var specialOffer in special.Products)
                {
                    var basketProduct = basket.Products.FirstOrDefault(x => x.Name == specialOffer.Name);
                    specialOffer.DisountedPrice = (basketProduct.Price * Qties[specialOffer.Name].Oty);
                }
                var excludedBasket = basket.Products
                   .Where(p => special.Products.All(sp => sp.Name != p.Name));

                special.TotalDisountedPrice = ((numOfApplies-1) * special.Total) + special.Products.Sum(x => x.DisountedPrice) + excludedBasket.Sum(x => x.Price * x.Quantity);


            }
            return basket.Specials.Min(x => x.TotalDisountedPrice);

        }
    }
}
