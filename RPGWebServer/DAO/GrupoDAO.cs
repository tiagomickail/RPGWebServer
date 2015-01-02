using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using RPGWebServer.Models;

namespace RPGWebServer.DAO
{
    public class GrupoDAO : BaseDAO
    {
        public List<Grupo> ObterGrupo(int UsuarioId)
        {
            string commandText = @"
                SELECT G.GRUPOID, G.NOME, G.USUARIOID
                FROM dbo.GRUPO AS G (NOLOCK)
                JOIN dbo.USUARIOGRUPO AS UG (NOLOCK)
	                ON U.GRUPOID = G.GRUPOID
                WHERE UG.USUARIOID = @USUARIOID ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = UsuarioId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Grupo> grupos = new List<Grupo>();

                if (reader.Read())
                {
                    Grupo grupo = new Grupo();

                    grupo.GrupoId = reader.GetInt32(0);
                    grupo.Nome = reader.GetString(1);
                    grupo.UsuarioId = reader.GetInt32(2);

                    grupos.Add(grupo);
                }

                return grupos;
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

        public bool VerificarSeCriouGrupo(int grupoid, int usuarioid)
        {
            string commandText = @"
                SELECT COUNT(*)
                FROM dbo.GRUPO AS G (NOLOCK)
                WHERE G.GRUPOID = @GRUPOID
	                AND G.USUARIOID = @USUARIOID ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = usuarioid;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "GRUPOID";
            parameter.Value = grupoid;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            try
            {
                Connection.Open();
                return ((Int32) command.ExecuteScalar()) > 0;
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

        public int CriarGrupo(Grupo grupo)
        {
            string commandText = @"
                INSERT INTO dbo.GRUPO
                        ( NOME, USUARIOID )
                VALUES
                        ( @NOME, @USUARIOID ) ";

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "USUARIOID";
            parameter.Value = grupo.UsuarioId;
            parameter.DbType = DbType.Int32;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "NOME";
            parameter.Value = grupo.Nome;
            parameter.DbType = DbType.String;
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

        public int AdicionarPessoasNoGrupo(int[] usuarios, int grupoid)
        {
            StringBuilder ids = new StringBuilder();
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (i != 0)
                    ids.Append("," + usuarios[i]);
                else
                    ids.Append(usuarios[i]);
            }

            string commandText = string.Format(@"
                INSERT INTO dbo.USUARIOGRUPO
                        ( USUARIOID, GRUPOID )
                SELECT U.USUARIOID,
		                @GRUPOID
                WHERE U.USUARIOID IN ({0}) ", ids );

            SqlCommand command = new SqlCommand(commandText, Connection);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "GRUPOID";
            parameter.Value = grupoid;
            parameter.DbType = DbType.Int32;
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

        public int RemoverPessoasNoGrupo(int[] usuarios, int grupoid)
        {
            StringBuilder ids = new StringBuilder();
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (i != 0)
                    ids.Append("," + usuarios[i]);
                else
                    ids.Append(usuarios[i]);
            }

            string commandText = string.Format(@"
                DELETE FROM dbo.USUARIOGRUPO
                WHERE USUARIOID IN ({0}) ", ids);

            SqlCommand command = new SqlCommand(commandText, Connection);
            
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

    }
}