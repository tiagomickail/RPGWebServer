using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGWebServer.Retornos
{
    public class RetornoLogin : Retorno
    {
        public int UsuarioId { get; set; }
        public Guid Token { get; set; }
    }
}