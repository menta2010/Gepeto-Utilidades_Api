namespace GepetoUtilidades.Service.Interfaces
{
    public interface IConsultaGptService
    {
        Task<string> ObterRespostaChatGptTexto();
    }
}
