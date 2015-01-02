using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.DAO
{
    public class AcessoDAO : BaseDAO
    {

        public int InserirToken(Acesso acesso)
        {
            string commandText = @"
                INSERT INTO dbo.ACESSO ( TOKEN, DATAGERACAO, USUARIOID )
                VALUES ( @TOKEN, @DATAGERACAO, @USUARIOID )";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "TOKEN";
            parameter.Value = acesso.Token;
            parameter.DbType = DbType.Guid;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "DATAGERACAO";
            parameter.Value = acesso.DataGeracao;
            parameter.DbType = DbType.DateTime;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = acesso.UsuarioId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                return command.ExecuteNonQuery(); ;
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


        public bool VerificarTokenValido(Guid token, int usuarioid)
        {
            string commandText = @"
                SELECT COUNT(*)
                FROM dbo.ACESSO AS A (NOLOCK)
                WHERE TOKEN = @TOKEN
	                AND USUARIOID = @USUARIOID
	                --AND DATEDIFF(MINUTE, DATAGERACAO, GETDATE()) > 30
                ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = usuarioid;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "TOKEN";
            parameter.Value = token;
            parameter.DbType = DbType.Guid;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                return ((Int32)command.ExecuteScalar()) > 0;
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