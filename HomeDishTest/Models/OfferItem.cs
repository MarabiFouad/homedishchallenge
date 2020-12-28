using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeDishTest.Models
{
    public class OfferItem
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} should be a positive number.")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public double DisountedPrice { get; set; }

        [JsonIgnore]
        public int ProductQuantity { get; set; }

    }
}
