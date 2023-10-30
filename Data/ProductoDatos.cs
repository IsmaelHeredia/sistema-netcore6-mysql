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
    public class ProductoDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<ProductoModel> Listar(string patron)
        {
            var productos = new List<ProductoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT prod.id,prod.nombre,prod.descripcion,prod.precio,prod.id_proveedor,prod.fecha_registro,prov.id AS proveedor_id,prov.nombre AS proveedor_nombre,prov.direccion AS proveedor_direccion,prov.telefono AS proveedor_telefono,prov.fecha_registro AS proveedor_fecha_registro FROM productos prod JOIN proveedores prov ON prov.id = prod.id_proveedor WHERE prod.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    ProveedorModel proveedor = new ProveedorModel();
                    proveedor.Id = Convert.ToInt32(dr["proveedor_id"]);
                    proveedor.Nombre = Convert.ToString(dr["proveedor_nombre"]);
                    proveedor.Direccion = Convert.ToString(dr["proveedor_direccion"]);
                    proveedor.Telefono = Convert.ToInt32(dr["proveedor_telefono"]);
                    proveedor.Fecha_registro = Convert.ToString(dr["proveedor_fecha_registro"]);

                    productos.Add(
                        new ProductoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"]),
                            Descripcion = Convert.ToString(dr["descripcion"]),
                            Precio = Convert.ToInt32(dr["precio"]),
                            Id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                            Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                            Proveedor = proveedor
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

            return productos;
        }

        public ProductoModel Obtener(int id_producto)
        {
            var producto = new ProductoModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT prod.id,prod.nombre,prod.descripcion,prod.precio,prod.id_proveedor,prod.fecha_registro,prov.id AS proveedor_id,prov.nombre AS proveedor_nombre,prov.direccion AS proveedor_direccion,prov.telefono AS proveedor_telefono,prov.fecha_registro AS proveedor_fecha_registro FROM productos prod JOIN proveedores prov ON prov.id = prod.id_proveedor WHERE prod.id = @id_producto", connection);
                query.Parameters.AddWithValue("@id_producto", id_producto);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    ProveedorModel proveedor = new ProveedorModel();
                    proveedor.Id = Convert.ToInt32(dr["proveedor_id"]);
                    proveedor.Nombre = Convert.ToString(dr["proveedor_nombre"]);
                    proveedor.Direccion = Convert.ToString(dr["proveedor_direccion"]);
                    proveedor.Telefono = Convert.ToInt32(dr["proveedor_telefono"]);
                    proveedor.Fecha_registro = Convert.ToString(dr["proveedor_fecha_registro"]);

                    producto = new ProductoModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Descripcion = Convert.ToString(dr["descripcion"]),
                        Precio = Convert.ToInt32(dr["precio"]),
                        Id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                        Proveedor = proveedor,
                        Fecha_registro = Convert.ToString(dr["fecha_registro"])
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

            return producto;
        }

        public bool Agregar(ProductoModel producto)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("INSERT INTO productos(nombre,descripcion,precio,id_proveedor,fecha_registro) VALUES(@p1,@p2,@p3,@p4,@p5)", connection);
                query.Parameters.AddWithValue("@p1", producto.Nombre);
                query.Parameters.AddWithValue("@p2", producto.Descripcion);
                query.Parameters.AddWithValue("@p3", producto.Precio);
                query.Parameters.AddWithValue("@p4", producto.Id_proveedor);
                query.Parameters.AddWithValue("@p5", producto.Fecha_registro);

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

        public bool Actualizar(ProductoModel producto)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE productos SET nombre = @p1, descripcion = @p2, precio = @p3, id_proveedor = @p4 WHERE id = @p5", connection);
                query.Parameters.AddWithValue("@p1", producto.Nombre);
                query.Parameters.AddWithValue("@p2", producto.Descripcion);
                query.Parameters.AddWithValue("@p3", producto.Precio);
                query.Parameters.AddWithValue("@p4", producto.Id_proveedor);
                query.Parameters.AddWithValue("@p5", producto.Id);

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

        public bool Borrar(ProductoModel producto)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM productos WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", producto.Id);

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
