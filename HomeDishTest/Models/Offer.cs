using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace HomeDishTest.Models
{
    public class Offer
    {
        [JsonPropertyName("products")]
        public IEnumerable<OfferItem> Products { get; set; }

        [JsonPropertyName("total")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} should be a positive number.")]
        public int Total { get; set; }

        [JsonIgnore]
        public double TotalDisountedPrice { get; set; }
    }
}
