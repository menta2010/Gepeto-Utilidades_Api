using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepetoUtilidades.Service.Utils
{
    public static class VerificarAudioValido
    {
        public class ResultadoValidacao
        {
            public bool Ehvalido { get; set; }
            public string MensagemErro { get; set; }
        }


        public static ResultadoValidacao ValidarAudio(IFormFile audio)
        {
            if (audio == null)
                return new ResultadoValidacao { Ehvalido = false, MensagemErro = "Nenhum arquivo fornecido." };

            if(audio.Length == 0)
                return new ResultadoValidacao { Ehvalido = false, MensagemErro = "Arquivo de áudio vazio." };

            return new ResultadoValidacao { Ehvalido = true };
        }

    }
}
