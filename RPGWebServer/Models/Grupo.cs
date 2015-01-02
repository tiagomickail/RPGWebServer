using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGWebServer.Envios;

namespace RPGWebServer.Models
{
    public class Grupo
    {
        public int GrupoId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }

        public Grupo() { }

        public Grupo(EnvioCriarGrupo envio)
        {
            UsuarioId = envio.UsuarioId;
            Nome = envio.NomeGrupo;
        }
    }
}