using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.Retornos
{
    public class RetornoMensagem : Retorno
    {
        public List<Mensagem> Mensagens { get; set; }
        public bool PossuiMensagem
        { get { return Mensagens.Count > 0; } }
    }
}