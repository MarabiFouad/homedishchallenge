using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeDishTest.Models
{
    public class Product
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} should be a positive number.")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public int DiscountQuantity { get; set; }
    }
}
