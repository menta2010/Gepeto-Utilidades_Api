using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepetoUtilidades.Service.Interfaces
{
    public interface IConsultaGptService
    {
        Task<string> ObterRespostaChatGptTexto();
    }
}
