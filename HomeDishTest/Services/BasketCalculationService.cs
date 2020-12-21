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

                    // if special product is not included in basket skip the offer
                    if (product == null) break;

                    // calculate discount for each special product
                    specialProduct.DisountedPrice = (specialProduct.Quantity) * product.Price;

                    // calculate price with discount for each offer
                    special.TotalDisountedPrice += specialProduct.DisountedPrice;
                }
            }

            // select an offer with maximum discount
            var offerWithMaxDiscount = basket.Specials.OrderByDescending(x => x.TotalDisountedPrice).FirstOrDefault();

            // exclude all product which are in selected offer from basket
            var excludedProductFromOffer = basket.Products
                   .Where(p => offerWithMaxDiscount.Products.All(sp => sp.Name != p.Name));

            // sum of all excluded products and maximum discount.
            return excludedProductFromOffer.Sum(x => x.Quantity * x.Price) + offerWithMaxDiscount.TotalDisountedPrice;

        }
    }
}
