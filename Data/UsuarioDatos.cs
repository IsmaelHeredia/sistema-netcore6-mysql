using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Sistema.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Sistema.Functions;

namespace Sistema.Data
{
    public class UsuarioDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<UsuarioModel> Listar(string patron)
        {
            var usuarios = new List<UsuarioModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT u.id,u.nombre,u.clave,u.id_tipo,u.fecha_registro,tu.id AS tipoUsuarioId,tu.nombre AS tipoUsuarioNombre FROM usuarios u JOIN tipos_usuarios tu ON tu.id = u.id_tipo WHERE u.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
                    tipoUsuario.Id = Convert.ToInt32(dr["tipoUsuarioId"]);
                    tipoUsuario.Nombre = Convert.ToString(dr["tipoUsuarioNombre"]);

                    usuarios.Add(
                        new UsuarioModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"]),
                            Clave = Convert.ToString(dr["clave"]),
                            Id_tipo = Convert.ToInt32(dr["id_tipo"]),
                            Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                            TipoUsuario = tipoUsuario
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

            return usuarios;
        }

        public UsuarioModel ObtenerPorId(int id_usuario)
        {
            var usuario = new UsuarioModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT u.id,u.nombre,u.clave,u.id_tipo,u.fecha_registro,tu.id AS tipoUsuarioId,tu.nombre AS tipoUsuarioNombre FROM usuarios u JOIN tipos_usuarios tu ON tu.id = u.id_tipo WHERE u.id = @id_usuario", connection);
                query.Parameters.AddWithValue("@id_usuario", id_usuario);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
                    tipoUsuario.Id = Convert.ToInt32(dr["tipoUsuarioId"]);
                    tipoUsuario.Nombre = Convert.ToString(dr["tipoUsuarioNombre"]);

                    usuario = new UsuarioModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Id_tipo = Convert.ToInt32(dr["id_tipo"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                        TipoUsuario = tipoUsuario
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

            return usuario;
        }

        public UsuarioModel ObtenerPorNombre(string nombre)
        {
            var usuario = new UsuarioModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT u.id,u.nombre,u.clave,u.id_tipo,u.fecha_registro,tu.id AS tipoUsuarioId,tu.nombre AS tipoUsuarioNombre FROM usuarios u JOIN tipos_usuarios tu ON tu.id = u.id_tipo WHERE u.nombre = @nombre", connection);
                query.Parameters.AddWithValue("@nombre", nombre);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
                    tipoUsuario.Id = Convert.ToInt32(dr["tipoUsuarioId"]);
                    tipoUsuario.Nombre = Convert.ToString(dr["tipoUsuarioNombre"]);

                    usuario = new UsuarioModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Id_tipo = Convert.ToInt32(dr["id_tipo"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                        TipoUsuario = tipoUsuario
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

            return usuario;
        }

        public bool Agregar(UsuarioModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("INSERT INTO usuarios(nombre,clave,id_tipo,fecha_registro) VALUES(@p1,@p2,@p3,@p4)", connection);
                query.Parameters.AddWithValue("@p1", usuario.Nombre);
                query.Parameters.AddWithValue("@p2", usuario.Clave);
                query.Parameters.AddWithValue("@p3", usuario.Id_tipo);
                query.Parameters.AddWithValue("@p4", usuario.Fecha_registro);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarEstado(ActualizarUsuarioModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE usuarios SET id_tipo = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", usuario.Id_tipo);
                query.Parameters.AddWithValue("@p2", usuario.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarNombre(CambiarUsuarioModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE usuarios SET nombre = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", usuario.Nuevo_usuario);
                query.Parameters.AddWithValue("@p2", usuario.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarClave(CambiarClaveModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE usuarios SET clave = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", usuario.Nueva_clave);
                query.Parameters.AddWithValue("@p2", usuario.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Borrar(UsuarioModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM usuarios WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", usuario.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
