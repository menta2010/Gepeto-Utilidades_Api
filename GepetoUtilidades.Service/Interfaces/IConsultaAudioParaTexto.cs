

namespace GepetoUtilidades.Service.Interfaces
{
    public interface IConsultaAudioParaTexto
    {
        Task<string> ConverterAudioParaTexto(string caminhoEntrada);
    }
}
