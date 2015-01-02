using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.Retornos
{
    public class RetornoListarUsuarios : Retorno
    {
        public List<Usuario> Usuarios { get; set; }
    }
}