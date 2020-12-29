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

                // Calculate the number of division for each offer`s product 
                foreach (var includedProduct in includedProducts)
                {
                    var specialProduct = special.Products.FirstOrDefault(x => x.Name == includedProduct.Name);
                    specialProduct.NumOfDivision = includedProduct.Quantity / specialProduct.Quantity;
                }

                var minimumNumOfDivision = special.Products.Min(x => x.NumOfDivision);

                // if this offer cannot be applied on the basket continue with the next offer
                if (minimumNumOfDivision < 1)
                    continue;

                var numOfApplicableOffer = (int)Math.Floor(minimumNumOfDivision);
                
                // Caculate the total price for products of this offer
                foreach (var specialProduct in special.Products)
                {
                    var includedProduct = includedProducts.FirstOrDefault(x => x.Name == specialProduct.Name);
                    
                    // Calculate deducted quantity 
                    specialProduct.ProductQuantity = includedProduct.Quantity - (numOfApplicableOffer * specialProduct.Quantity);
                    
                    // Store total price for each special product with applied offer
                    special.TotalDisountedPrice += (specialProduct.ProductQuantity * includedProduct.Price);
                }

                // Calculate the total price for products not included in this offer
                var notIncludedInOfferProducts = basket.Products
                   .Where(p => special.Products.All(sp => sp.Name != p.Name));

                // Sum up offer`s product price with other products in the basket
                special.TotalDisountedPrice =
                    ((numOfApplicableOffer) * special.Total) +
                    special.TotalDisountedPrice +
                    notIncludedInOfferProducts.Sum(x => x.Price * x.Quantity);

            }

            // return the minimum amount
            return basket.Specials.Min(x => x.TotalDisountedPrice);

        }
    }
}
