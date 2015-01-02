using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGWebServer.Envios
{
    public class Envio
    {
        public Guid Token { get; set; }
        public int UsuarioId { get; set; }
    }
}