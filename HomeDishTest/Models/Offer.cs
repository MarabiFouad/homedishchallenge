using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HomeDishTest.Models
{
    public class Offer {
        [JsonPropertyName("products")]
        public IEnumerable<OfferItem> Products { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonIgnore]
        public int SumOfQuantities => Products.Sum(x => x.Quantity);

        [JsonIgnore]
        public double TotalDisountedPrice { get; set; }

    }
}
