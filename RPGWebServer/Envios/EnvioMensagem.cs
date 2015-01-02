using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGWebServer.Envios
{
    public class EnvioMensagem : Envio
    {
        public string Conteudo { get; set; }
        public int GrupoDestino { get; set; }
    }
}