using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using RPGWebServer.Models;

namespace RPGWebServer.Retornos
{
    public class RetornoGrupo : Retorno
    {
        public List<Grupo> Grupos { get; set; }
    }
}