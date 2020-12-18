using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeDishTest.Models
{
    public class Basket
    {
        [JsonPropertyName("products")]
        public IEnumerable<Product> Products { get; set; }

        [JsonPropertyName("specials")]
        public IEnumerable<Offer> Specials { get; set; }
    }
}
