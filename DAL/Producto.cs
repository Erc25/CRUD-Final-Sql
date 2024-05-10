using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Producto
    {
        private int _Id;
        private string _Nombre;
        private decimal _Precio;
        private string _Descripcion;

        public int Id { get => _Id; set => _Id=value; }
        public string Nombre { get => _Nombre; set => _Nombre=value; }
        public decimal Precio { get => _Precio; set => _Precio=value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion=value; }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            string connectionString = Conexion.cadenaConexion;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /*string query = "SELECT Id, Nombre, Precio, Descripcion FROM Productos";
                SqlCommand command = new SqlCommand(query, connection);*/

                SqlCommand command = new SqlCommand("ObtenerProducto", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)reader["Id"];
                    producto.Nombre = reader["Nombre"].ToString();
                    producto.Precio = (decimal)reader["Precio"];
                    producto.Descripcion = reader["Descripcion"].ToString();
                    productos.Add(producto);
                }
                reader.Close();
            }

            return productos;
        }

        public void Insertar(Producto producto)
        {
            string connectionString = Conexion.cadenaConexion;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Utilizando procedimiento almacenado
                SqlCommand command = new SqlCommand("InsertarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
               
                /* en caso de que sea insercion desde programa
                string query = "INSERT INTO Productos (Nombre, Precio, Descripcion) VALUES (@Nombre, @Precio, @Descripcion)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);*/

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string connectionString = Conexion.cadenaConexion;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /*string query = "DELETE FROM Productos WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);*/

                SqlCommand command = new SqlCommand("DeleteProducto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Editar(Producto producto)
        {
            string connectionString = Conexion.cadenaConexion;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /*string query = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Descripcion = @Descripcion WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);*/

                SqlCommand command = new SqlCommand("UpdateProducto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Id", producto.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
