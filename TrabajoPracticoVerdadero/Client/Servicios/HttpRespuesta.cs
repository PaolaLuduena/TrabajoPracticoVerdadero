using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrabajoPracticoVerdadero.Client.Servicios
{
    public class HttpRespuesta<T>
    {
        public T Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage httpResponseMessage { get; }


        public HttpRespuesta(T Respuesta,
                             bool error,
                             HttpResponseMessage httpResponseMessage)

        {
            this.Respuesta = Respuesta;
            Error = error;
            this.httpResponseMessage = httpResponseMessage;
        }

       public async Task<String>GetBody()
        {
            var resp = await httpResponseMessage.Content.ReadAsStringAsync();
            return resp;

        }

    }
}
