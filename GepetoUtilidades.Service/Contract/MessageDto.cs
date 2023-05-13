using Newtonsoft.Json;

namespace GepetoUtilidades.Service.Contract
{
    public class MessageDto
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
