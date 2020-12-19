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
                foreach (var specialProduct in special.Products)
                {
                    var product = basket.Products.FirstOrDefault(x => x.Name == specialProduct.Name);

                    //if special product is included in basket skip the offer
                    if (product == null) break;

                    //calculate discount for each special product
                    specialProduct.DisountedPrice = specialProduct.Quantity * product.Price;

                    //calculate price with discount for each offer
                    special.TotalDisountedPrice += specialProduct.DisountedPrice;

                }

            }
            var specialWithMinimumDiscount = basket.Specials.OrderBy(x => x.TotalDisountedPrice).FirstOrDefault();
            var excludedMiniumSpecialProducts = basket.Products
                .Where(p => specialWithMinimumDiscount.Products.All(sp => sp.Name != p.Name));
            var totalPrice = excludedMiniumSpecialProducts.Sum(x => x.Price * x.Quantity);
            return (totalPrice + specialWithMinimumDiscount.TotalDisountedPrice);

        }
    }
}
