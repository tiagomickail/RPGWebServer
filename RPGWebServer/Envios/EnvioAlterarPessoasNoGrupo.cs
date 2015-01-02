using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGWebServer.Envios
{
    public class EnvioAlterarPessoasNoGrupo : Envio
    {
        public int[] Ids { get; set; }
        public int GrupoId { get; set; }
        public bool Adicionar { get; set; }
    }
}