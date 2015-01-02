using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using RPGWebServer.DAO;
using RPGWebServer.Envios;
using RPGWebServer.Models;
using RPGWebServer.Retornos;

namespace RPGWebServer
{
    /// <summary>
    /// Summary description for Servicos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicos : System.Web.Services.WebService
    {
        [WebMethod]
        public RetornoLogin Login(string login, string senha)
        {
            UsuarioDAO usuarioDao = new UsuarioDAO();
            Usuario user = usuarioDao.ObterUsuario(login, senha);

            RetornoLogin retorno = new RetornoLogin();
            
            if (user != null)
            {
                Acesso acesso = new Acesso();
                acesso.Token = Guid.NewGuid();
                acesso.UsuarioId = user.UsuarioId;
                acesso.DataGeracao = DateTime.Now;
                
                AcessoDAO acessoDao = new AcessoDAO();
                acessoDao.InserirToken(acesso);

                retorno.UsuarioId = acesso.UsuarioId;
                retorno.Token = acesso.Token;
                retorno.Sucesso = true;
            }
            else
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = "Usuário ou senha inválidos";
            }

            return retorno;
        }

        public bool ValidarToken (Envio envio)
        {
            return new AcessoDAO().VerificarTokenValido(envio.Token, envio.UsuarioId);
        }

        [WebMethod]
        public RetornoGrupo ObterGrupos(Envio envio)
        {
            if (!ValidarToken(envio))
                return new RetornoGrupo()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            RetornoGrupo retorno = new RetornoGrupo();
            retorno.Grupos = new GrupoDAO().ObterGrupo(envio.UsuarioId);
            retorno.Sucesso = true;

            return retorno;
        }

        [WebMethod]
        public Retorno EnviarMensagem(EnvioMensagem envioMensagem)
        {
            if (!ValidarToken(envioMensagem))
                return new RetornoGrupo()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            Mensagem mensagem = new Mensagem(envioMensagem);
            Retorno retorno = new Retorno();

            if (new MensagemDAO().InserirMensagem(mensagem) > 0)
            {
                retorno.Sucesso = true;
            }
            else
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = "Erro ao inserir a mensagem";
            }

            return retorno;
        }

        [WebMethod]
        public RetornoMensagem VerificarNovaMensagem(EnvioVerificarNovaMensagem envioVerificar)
        {
            if (!ValidarToken(envioVerificar))
                return new RetornoMensagem()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            RetornoMensagem retornoMensagem = new RetornoMensagem();

            retornoMensagem.Mensagens = new MensagemDAO().VerificarNovaMensagem(envioVerificar.GrupoId, envioVerificar.UsuarioId);
            retornoMensagem.Sucesso = true;

            return retornoMensagem;
        }

        [WebMethod]
        public Retorno CriarGrupo(EnvioCriarGrupo envio)
        {
            if (!ValidarToken(envio))
                return new Retorno()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            Grupo grupo = new Grupo(envio);
            Retorno retorno = new Retorno();

            if (new GrupoDAO().CriarGrupo(grupo) > 0)
            {
                retorno.Sucesso = true;
            }
            else
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = "Erro ao criar grupo";
            }

            return retorno;
        }

        [WebMethod]
        public Retorno AlterarPessoasNoGrupo(EnvioAlterarPessoasNoGrupo envio)
        {
            if (!ValidarToken(envio))
                return new Retorno()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            GrupoDAO grupoDao = new GrupoDAO();
            Retorno retorno = new Retorno();

            if (grupoDao.VerificarSeCriouGrupo(envio.GrupoId, envio.UsuarioId))
            {
                if (envio.Adicionar)
                    grupoDao.AdicionarPessoasNoGrupo(envio.Ids, envio.GrupoId);
                else
                    grupoDao.RemoverPessoasNoGrupo(envio.Ids, envio.GrupoId);

                retorno.Sucesso = true;
            }
            else
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = "O Usuário não pode excluir o grupo";
            }

            return retorno;
        }

        [WebMethod]
        public Retorno ListarUsuarios(Envio envio)
        {
            if (!ValidarToken(envio))
                return new Retorno()
                {
                    Sucesso = false,
                    MensagemRetorno = "Erro ao Validar o token."
                };

            RetornoListarUsuarios retorno = new RetornoListarUsuarios();
            retorno.Usuarios = new UsuarioDAO().ListarUsuarios();
            retorno.Sucesso = true;

            return retorno;
        }

    }
}
