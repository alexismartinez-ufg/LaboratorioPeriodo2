using LaboratorioPeriodo2.DTOs;
using System.Data.SqlClient;

namespace LaboratorioPeriodo2.DAOs
{
    public class DAOProducto
    {
        private string ConnectionString =
            "Data Source=WINDOWS-5SRBVKD;" +
            "Initial Catalog=laboratorio3;" +
            "User=sa;" +
            "Password=Admin123!";

        public List<DTOProducto> GetAllProductos()
        {
            var productos = new List<DTOProducto>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM producto", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new DTOProducto
                            {
                                CodigoProd = Convert.ToInt32(reader["codigoprod"]),
                                NombreProd = reader["nombreprod"].ToString(),
                                NombreProv = reader["nombreprov"].ToString(),
                                PrecioUnit = Convert.ToDecimal(reader["preciounit"]),
                                Unidades = Convert.ToInt32(reader["unidades"])
                            });
                        }
                    }
                }
            }
            return productos;
        }

        public DTOProducto GetById(int codigoProd)
        {
            DTOProducto producto = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM producto WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", codigoProd);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new DTOProducto
                            {
                                CodigoProd = Convert.ToInt32(reader["codigoprod"]),
                                NombreProd = reader["nombreprod"].ToString(),
                                NombreProv = reader["nombreprov"].ToString(),
                                PrecioUnit = Convert.ToDecimal(reader["preciounit"]),
                                Unidades = Convert.ToInt32(reader["unidades"])
                            };
                        }
                    }
                }
            }
            return producto;
        }

        public int Add(DTOProducto producto)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("INSERT INTO producto (codigoprod, nombreprod, nombreprov, preciounit, unidades) VALUES (@codigoProd, @nombreProd, @nombreProv, @precioUnit, @unidades)", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", producto.CodigoProd);
                    command.Parameters.AddWithValue("@nombreProd", producto.NombreProd);
                    command.Parameters.AddWithValue("@nombreProv", producto.NombreProv);
                    command.Parameters.AddWithValue("@precioUnit", producto.PrecioUnit);
                    command.Parameters.AddWithValue("@unidades", producto.Unidades);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public int Edit(DTOProducto producto)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("UPDATE producto SET nombreprod = @nombreProd, nombreprov = @nombreProv, preciounit = @precioUnit, unidades = @unidades WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", producto.CodigoProd);
                    command.Parameters.AddWithValue("@nombreProd", producto.NombreProd);
                    command.Parameters.AddWithValue("@nombreProv", producto.NombreProv);
                    command.Parameters.AddWithValue("@precioUnit", producto.PrecioUnit);
                    command.Parameters.AddWithValue("@unidades", producto.Unidades);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public int Delete(int codigoProd)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("DELETE FROM producto WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", codigoProd);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }

}
