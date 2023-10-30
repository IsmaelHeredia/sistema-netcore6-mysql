using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Sistema.Models;
using Microsoft.Extensions.Configuration;
using Sistema.Functions;

namespace Sistema.Data
{
    public class ProveedorDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<ProveedorModel> Listar(string patron)
        {
            var proveedores = new List<ProveedorModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT prov.id,prov.nombre,prov.direccion,prov.telefono,prov.fecha_registro FROM proveedores prov WHERE prov.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    proveedores.Add(
                        new ProveedorModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"]),
                            Direccion = Convert.ToString(dr["direccion"]),
                            Telefono = Convert.ToInt32(dr["telefono"]),
                            Fecha_registro = Convert.ToString(dr["fecha_registro"])
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

            return proveedores;
        }

        public ProveedorModel Obtener(int id_proveedor)
        {
            var proveedor = new ProveedorModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT prov.id,prov.nombre,prov.direccion,prov.telefono,prov.fecha_registro FROM proveedores prov WHERE prov.id = @id_proveedor", connection);
                query.Parameters.AddWithValue("@id_proveedor", id_proveedor);

                var dr = query.ExecuteReader();

                dr.Read();

                if(dr.HasRows)
                {
                    proveedor = new ProveedorModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToInt32(dr["telefono"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"])
                    };
                } else
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

            return proveedor;
        }

        public bool Agregar(ProveedorModel proveedor)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("INSERT INTO proveedores(nombre,direccion,telefono,fecha_registro) VALUES(@p1,@p2,@p3,@p4)", connection);
                query.Parameters.AddWithValue("@p1", proveedor.Nombre);
                query.Parameters.AddWithValue("@p2", proveedor.Direccion);
                query.Parameters.AddWithValue("@p3", proveedor.Telefono);
                query.Parameters.AddWithValue("@p4", proveedor.Fecha_registro);

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

        public bool Actualizar(ProveedorModel proveedor)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE proveedores SET nombre = @p1, direccion = @p2, telefono = @p3 WHERE id = @p4", connection);
                query.Parameters.AddWithValue("@p1", proveedor.Nombre);
                query.Parameters.AddWithValue("@p2", proveedor.Direccion);
                query.Parameters.AddWithValue("@p3", proveedor.Telefono);
                query.Parameters.AddWithValue("@p4", proveedor.Id);

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

        public bool Borrar(ProveedorModel proveedor)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM proveedores WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", proveedor.Id);

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
