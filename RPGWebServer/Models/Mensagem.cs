using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGWebServer.Envios;

namespace RPGWebServer.Models
{
    public class Mensagem
    {
        public int MensagemId { get; set; }
        public string Conteudo { get; set; }
        public int UsuarioRemetente { get; set; }
        public int GrupoDestino { get; set; }
        public DateTime DataEnvio { get; set; }
        public string Usuario { get; set; }


        public Mensagem() { }

        public Mensagem(EnvioMensagem envioMensagem)
        {
            Conteudo = envioMensagem.Conteudo;
            UsuarioRemetente = envioMensagem.UsuarioId;
            GrupoDestino = envioMensagem.GrupoDestino;
            DataEnvio = DateTime.Now;
        }
    }
}