using System.Threading.Tasks;

namespace TrabajoPracticoVerdadero.Client.Servicios
{
    public interface IHttpService1
    {
        Task<HttpRespuesta<object>> Delete(string url);
        Task<HttpRespuesta<T>> Get<T>(string url);
        Task<HttpRespuesta<object>> Post<T>(string url, T enviar);
        Task<HttpRespuesta<object>> Put<T>(string url, T enviar);
    }
}