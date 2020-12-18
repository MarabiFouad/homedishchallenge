using System.Text.Json.Serialization;

namespace HomeDishTest.Models
{
    public class OfferItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public double DisountedPrice { get; set; }
    }
}
