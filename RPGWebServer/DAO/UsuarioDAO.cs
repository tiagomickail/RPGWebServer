using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.DAO
{
    public class UsuarioDAO : BaseDAO
    {

        public Usuario ObterUsuario(string login, string senha)
        {
            string commandText = @"
                SELECT U.USUARIOID, U.LOGIN, U.SENHA
                FROM dbo.USUARIO AS U (NOLOCK)
                WHERE U.LOGIN = @LOGIN
	                AND U.SENHA = @SENHA
                ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "LOGIN";
            parameter.Value = login;
            parameter.DbType = DbType.String;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "SENHA";
            parameter.Value = senha;
            parameter.DbType = DbType.String;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Usuario user = null;

                if (reader.Read())
                {
                    user = new Usuario();

                    user.UsuarioId = reader.GetInt32(0);
                    user.Login = reader.GetString(1);
                    user.Senha = reader.GetString(2);
                }

                return user;
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

        public Usuario ObterUsuario(int usuarioId)
        {
            string commandText = @"
                SELECT U.USUARIOID, U.LOGIN, U.SENHA
                FROM dbo.USUARIO AS U (NOLOCK)
                WHERE U.USUARIOID = @USUARIOID
                ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = usuarioId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Usuario user = null;

                if (reader.Read())
                {
                    user = new Usuario();

                    user.UsuarioId = reader.GetInt32(0);
                    user.Login = reader.GetString(1);
                    user.Senha = reader.GetString(2);
                }

                return user;
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

        public List<Usuario> ListarUsuarios()
        {
            string commandText = @"
                SELECT U.USUARIOID, U.LOGIN
                FROM dbo.USUARIO AS U (NOLOCK)
                ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Usuario> usuarios = new List<Usuario>();

                if (reader.Read())
                {
                    Usuario user = new Usuario();

                    user.UsuarioId = reader.GetInt32(0);
                    user.Login = reader.GetString(1);

                    usuarios.Add(user);
                }

                return usuarios;
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