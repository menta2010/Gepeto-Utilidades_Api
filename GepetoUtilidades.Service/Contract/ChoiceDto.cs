using Newtonsoft.Json;

namespace GepetoUtilidades.Service.Contract
{
    public class ChoiceDto
    {
        [JsonProperty("message")]
        public MessageDto Message { get; set; }
    }
}
