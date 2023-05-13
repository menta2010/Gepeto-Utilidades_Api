using GepetoUtilidades.Service.Contract;
using GepetoUtilidades.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace GepetoUtilidades.Service.Services
{
    public class ConsultaGptService : IConsultaGptService
    {
        public async Task<string> ObterRespostaChatGptTexto()
        {
            var mensagens = new List<MessageDto>() { new MessageDto() {Role = "user", Content = " olá" } };
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authorization", "Bearer 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");
            var mensagemJson = JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = mensagens });

            var respostaJson = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", new StringContent(mensagemJson, Encoding.UTF8, "application/json"));

            var resposta = await respostaJson.Content.ReadAsStringAsync();


            var respostana = JsonConvert.DeserializeObject<ChatGptResponseDto>(resposta);

            return respostana.Choices[0].Message.Content;
        }
    }
}
