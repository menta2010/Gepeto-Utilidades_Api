using Newtonsoft.Json;

namespace GepetoUtilidades.Service.Contract
{
    public class ConsultarAudioDto
    {
        [JsonProperty("file")]
        public string Audio { get; set; }

        [JsonProperty("model")]
        public string Modelo { get; set; }
    }
}
