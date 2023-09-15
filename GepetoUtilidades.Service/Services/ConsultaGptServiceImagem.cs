using GepetoUtilidades.Service.Config;
using GepetoUtilidades.Service.Contract;
using GepetoUtilidades.Service.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace GepetoUtilidades.Service.Services
{
    public class ConsultaGptServiceImagem : IConsultaGptServiceImagem
    {
   
            private readonly HttpClient _httpClient;
            private readonly OpenAiApiSettingsImage _apiSettings;

            public ConsultaGptServiceImagem(HttpClient httpClient, IOptions<OpenAiApiSettingsImage> apiSettings)
            {
                _httpClient = httpClient;
                _apiSettings = apiSettings.Value;
            }
            public async Task<string> ObterRespostaChatGptImagem()
        {
            var mensagens = new ConsultaGptImagemDto() { Texto = "criar logo com icone de gps com a escrita s.o.s petropolis", NumeroImagens = 3, Tamanho = "512x512" };
            var mensagemJson = PrepararSolicitacaoImagem(mensagens);
            var respostaJson = await EnviarSolicitacaoImagem(mensagemJson);
            var resposta = JsonConvert.DeserializeObject<ChatGptResponseDto>(respostaJson);
            return resposta.Choices[0].Message.Content;
        }

        private string PrepararSolicitacaoImagem(ConsultaGptImagemDto mensagens)
        {
            return JsonConvert.SerializeObject( mensagens );
        }

        private async Task<string> EnviarSolicitacaoImagem(string mensagemJson)
        {
            _httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiSettings.ApiKey}");
            var respostaJson = await _httpClient.PostAsync(_apiSettings.ApiImgUrl, new StringContent(mensagemJson, Encoding.UTF8, "application/json"));

            return await respostaJson.Content.ReadAsStringAsync();
        }
    }
}
