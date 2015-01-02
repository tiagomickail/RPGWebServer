using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.DAO
{
    public class MensagemDAO : BaseDAO
    {
        public int InserirMensagem(Mensagem mensagem)
        {
            string commandText = @"
                INSERT INTO dbo.MENSAGEM
                        ( CONTEUDO, USUARIOREMETENTE, GRUPODESTINO, DATAENVIO )
                VALUES
                        ( @CONTEUDO, @USUARIOREMETENTE, @GRUPODESTINO, @DATAENVIO )";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "CONTEUDO";
            parameter.Value = mensagem.Conteudo;
            parameter.DbType = DbType.String;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOREMETENTE";
            parameter.Value = mensagem.UsuarioRemetente;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "GRUPODESTINO";
            parameter.Value = mensagem.GrupoDestino;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "DATAENVIO";
            parameter.Value = mensagem.DataEnvio;
            parameter.DbType = DbType.DateTime;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<Mensagem> VerificarNovaMensagem(int grupoId, int usuarioId)
        {
            string commandText = @"
                SELECT M.MENSAGEMID,
	                M.CONTEUDO,
	                M.USUARIOREMETENTE,
	                M.GRUPODESTINO,
	                M.DATAENVIO,
                    U.LOGIN
                FROM dbo.MENSAGEM AS M (NOLOCK)
                JOIN dbo.USUARIO AS U (NOLOCK)
                    ON U.USUARIOID = M.USUARIOREMETENTE
                LEFT JOIN dbo.MENSAGEMENTREGA AS ME (NOLOCK)
	                ON ME.MENSAGEMID = M.MENSAGEMID
		                AND ME.USUARIOID = @USUARIOID
                WHERE M.GRUPODESTINO = @GRUPOID
	                AND ME.MENSAGEMID IS NULL
                ORDER BY M.MENSAGEMID
                ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = grupoId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "GRUPOID";
            parameter.Value = usuarioId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Mensagem> mensagens = new List<Mensagem>();

                if (reader.Read())
                {
                    Mensagem mensagem = new Mensagem();

                    mensagem.MensagemId = reader.GetInt32(0);
                    mensagem.Conteudo = reader.GetString(1);
                    mensagem.UsuarioRemetente = reader.GetInt32(2);
                    mensagem.GrupoDestino = reader.GetInt32(3);
                    mensagem.DataEnvio = reader.GetDateTime(4);
                    mensagem.Usuario = reader.GetString(5);

                    mensagens.Add(mensagem);
                }

                return mensagens;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}