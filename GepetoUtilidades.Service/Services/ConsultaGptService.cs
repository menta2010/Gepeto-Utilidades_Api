using GepetoUtilidades.Service.Config;
using GepetoUtilidades.Service.Contract;
using GepetoUtilidades.Service.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace GepetoUtilidades.Service.Services
{
    public class ConsultaGptService : IConsultaGptService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAiApiSettings _apiSettings;

        public ConsultaGptService(HttpClient httpClient, IOptions<OpenAiApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }
        public async Task<string> ObterRespostaChatGptTexto()
        {
            var mensagens = new List<MessageDto>() { new MessageDto() { Role = "user", Content = " olá" } };
            var mensagemJson = PrepararSolicitacao(mensagens);
            var respostaJson = await EnviarSolicitacao(mensagemJson);
            var resposta = JsonConvert.DeserializeObject<ChatGptResponseDto>(respostaJson);
            return resposta.Choices[0].Message.Content;
        }

        private string PrepararSolicitacao(List<MessageDto> mensagens)
        {
            return JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = mensagens });
        }

        private async Task<string> EnviarSolicitacao(string mensagemJson)
        {
            _httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiSettings.ApiKey}");
            var respostaJson = await _httpClient.PostAsync(_apiSettings.ApiUrl, new StringContent(mensagemJson, Encoding.UTF8, "application/json"));

            return await respostaJson.Content.ReadAsStringAsync();
        }
    }
}
