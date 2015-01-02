using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGWebServer.Models
{
    public class Acesso
    {
        public Guid Token { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataGeracao { get; set; }

    }
}