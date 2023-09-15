using Newtonsoft.Json;

namespace GepetoUtilidades.Service.Contract
{
    public class ConsultaGptImagemDto
    {
        [JsonProperty("prompt")]
        public string Texto { get; set; }

        [JsonProperty("n")]
        public int NumeroImagens { get; set; }

        [JsonProperty("size")]
        public string Tamanho { get; set; }
    }
}
