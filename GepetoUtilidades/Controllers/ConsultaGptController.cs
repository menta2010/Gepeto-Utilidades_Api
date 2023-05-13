using GepetoUtilidades.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GepetoUtilidades.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaGptController : ControllerBase
    {
        private readonly IConsultaGptService _consultaGptService;

        public ConsultaGptController(IConsultaGptService consultaGptService)
        {
            _consultaGptService = consultaGptService;
        }

        [HttpGet(Name = "ObterResposta")]
        public async Task<string> ConsultarChatBot()
        {
            return await _consultaGptService.ObterRespostaChatGptTexto();
        }
    }
}
