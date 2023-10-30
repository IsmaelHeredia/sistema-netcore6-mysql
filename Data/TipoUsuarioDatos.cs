using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Sistema.Models;
using Microsoft.Extensions.Configuration;
using Sistema.Functions;

namespace Sistema.Data
{
    public class TipoUsuarioDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<TipoUsuarioModel> Listar(string patron)
        {
            var tipo_usuarios = new List<TipoUsuarioModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT tu.id,tu.nombre FROM tipos_usuarios AS tu WHERE tu.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    tipo_usuarios.Add(
                        new TipoUsuarioModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"])
                        }
                    );
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return tipo_usuarios;
        }

        public TipoUsuarioModel Obtener(int id_tipoUsuario)
        {
            var tipoUsuario = new TipoUsuarioModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT tu.id,tu.nombre FROM tipos_usuarios AS tu WHERE tu.id = @id_tipousuario", connection);
                query.Parameters.AddWithValue("@id_tipousuario", id_tipoUsuario);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    tipoUsuario = new TipoUsuarioModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"])
                    };
                }
                else
                {
                    return null;
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return tipoUsuario;
        }
    }
}
