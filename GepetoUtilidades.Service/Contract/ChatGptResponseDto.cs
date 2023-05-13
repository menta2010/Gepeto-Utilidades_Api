using Newtonsoft.Json;

namespace GepetoUtilidades.Service.Contract
{
    public class ChatGptResponseDto
    {
        [JsonProperty("choices")]
        public List<ChoiceDto> Choices { get; set; }
    }
}
