using GepetoUtilidades.Service.Config;
using GepetoUtilidades.Service.Contract;
using GepetoUtilidades.Service.Interfaces;
using Microsoft.Extensions.Options;
using NAudio.Wave;
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
            ConverterAudioTexto();
            var mensagens = new List<MessageDto>() { new MessageDto() { Role = "user", Content = " olá" } };
            var mensagemJson = PrepararSolicitacao(mensagens);
            var respostaJson = await EnviarSolicitacao(mensagemJson);
            var resposta = JsonConvert.DeserializeObject<ChatGptResponseDto>(respostaJson);
            return resposta.Choices[0].Message.Content;
        }

        private void ConverterAudioTexto()
        {
            ConverterParaWav("C:\\Users\\NatalinoEstevesRodri\\Documents\\teste.mp3", "C:\\Users\\NatalinoEstevesRodri\\Documents\\SaidaAudio");
        }
        private void ConverterParaWav(string inputPath, string outputPath)
        {
            using (var reader = new MediaFoundationReader(inputPath))
            {
                WaveFileWriter.CreateWaveFile(outputPath, reader);
            }

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
