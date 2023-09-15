using GepetoUtilidades.Service.Interfaces;
using GepetoUtilidades.Service.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GepetoUtilidades.Controllers
{
    [ApiController]
    [Route("api/audio")]
    public class ConsultaGptController : ControllerBase
    {
        private readonly IConsultaGptService _consultaGptService;
        private readonly IConsultaGptServiceImagem _consultaGptServiceImage;
        private readonly IConsultaAudioParaTexto _consultaAudioParaTexto;

        public ConsultaGptController(IConsultaGptService consultaGptService, IConsultaGptServiceImagem consultaGptServiceImage, IConsultaAudioParaTexto consultaAudioParaTexto)
        {
            _consultaGptService = consultaGptService;
            _consultaGptServiceImage = consultaGptServiceImage;
            _consultaAudioParaTexto = consultaAudioParaTexto;
        }

        [HttpPost("gerarAta")]
        public async Task<IActionResult> GerarAtaAtravesAudio(IFormFile audioFile)
        {
            var audioValido = VerificarAudioValido.ValidarAudio(audioFile);


            if (!audioValido.Ehvalido)
            {
                return BadRequest(audioValido.MensagemErro);
            }


            // return await _consultaGptService.ObterRespostaChatGptTexto();
        }
    }
}
