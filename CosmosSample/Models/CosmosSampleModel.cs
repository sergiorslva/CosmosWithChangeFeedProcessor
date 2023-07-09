using Newtonsoft.Json;

namespace CosmosSample.Models
{
    public class CosmosSampleModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "images")]
        public List<string> Images { get; set; } = new();

        [JsonProperty(PropertyName = "Deleted")]
        public bool Deleted { get; set; } = false;
    }
}
