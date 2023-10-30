using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using Sistema.Functions;
using Sistema.Models;

namespace Sistema.Data
{
    public class AccesoDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public bool Ingreso(IngresoModel ingreso)
        {
            var producto = new ProductoModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,clave FROM usuarios WHERE nombre = @usuario", connection);
                query.Parameters.AddWithValue("@usuario", ingreso.Usuario);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    int id = Convert.ToInt32(dr["id"]);
                    string nombre = Convert.ToString(dr["nombre"]);
                    string clave = Convert.ToString(dr["clave"]);

                    try
                    {
                        if (BCrypt.Net.BCrypt.Verify(ingreso.Clave, clave))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }
        }
    }
}
