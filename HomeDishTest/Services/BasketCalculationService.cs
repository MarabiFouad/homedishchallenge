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
                var includedProducts = basket.Products
                   .Where(p => special.Products.Any(sp => sp.Name == p.Name));

                foreach (var includedProduct in includedProducts)
                {
                    var specialProduct = special.Products.FirstOrDefault(x => x.Name == includedProduct.Name);
                    specialProduct.ProductQuantity = includedProduct.Quantity;
                }

                int numOfAppliedOffer = 0;
                while (!special.Products.Any(x => (x.ProductQuantity / x.Quantity) < 1.0))
                {
                    numOfAppliedOffer++;
                    foreach (var specialOffer in special.Products)
                    {
                        specialOffer.ProductQuantity = specialOffer.ProductQuantity - specialOffer.Quantity;
                    }
                }
                foreach (var specialOffer in special.Products)
                {
                    var basketProduct = basket.Products.FirstOrDefault(x => x.Name == specialOffer.Name);
                    specialOffer.DisountedPrice = (basketProduct.Price * specialOffer.ProductQuantity);
                }

                var notIncludeInOfferProducts = basket.Products
                   .Where(p => special.Products.All(sp => sp.Name != p.Name));

                special.TotalDisountedPrice =
                    ((numOfAppliedOffer) * special.Total) +
                    special.Products.Sum(x => x.DisountedPrice) +
                    notIncludeInOfferProducts.Sum(x => x.Price * x.Quantity);

            }


            return basket.Specials.Min(x => x.TotalDisountedPrice);

        }
    }
}
