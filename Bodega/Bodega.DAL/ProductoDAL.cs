using Bodega.Bodega.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Common;

namespace Bodega.Bodega.DAL
{
    public class ProductoDAL : IProducto
    {
        string dbconexion;

        public ProductoDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }
        public async Task<List<Producto>> ObtenerProductos()
        {
            List<Producto> ListProductos = new List<Producto>();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            ListProductos.Add(new Producto
                            {
                                Nombre = sdr["Nombre"].ToString(),
                                Presentacion = sdr["Presentacion"].ToString(),
                                CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"]),
                                ImagenPath = sdr["ImagenPath"].ToString()
                            });
                        }
                        con.Close();
                    }
                    else
                    {
                        ListProductos = null;
                    }
                }
                catch (Exception) {
                    con.Close(); 
                }
                return ListProductos;
            }
            
        }
        public async Task<Producto> ObtenerProducto(int id)
        {
            Producto Pr = new Producto();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductoId", id);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Pr.ProductoId = Convert.ToInt16(sdr["ProductoId"]);
                            Pr.Nombre = sdr["Nombre"].ToString();
                            Pr.Presentacion = sdr["Presentacion"].ToString();
                            Pr.PMayoreo = Convert.ToDouble(sdr["PMayoreo"]);
                            Pr.Pmenudeo = Convert.ToDouble(sdr["Pmenudeo"]);
                            Pr.CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"]);
                        }
                        con.Close();
                    }
                    else
                    {
                        Pr = null;
                    }
                }
                catch (Exception )
                {
                    con.Close();
                }
                return Pr;

            }
            
        }
        public async Task<List<string>> ObtenerNombreProducto()
        {
            List<string> NombreProducto = new List<string>();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("NombreProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            NombreProducto.Add(sdr["Nombre"].ToString());
                        }
                        con.Close();
                    }
                    else
                    {
                        NombreProducto = null;
                    }
                }
                catch (Exception ) { 
                    con.Close();
                }
            }
            return (NombreProducto);
        }


        public async Task<List<Producto>> ObtenerProductoPorNombre(string nombre)
        {
            List<Producto> ListProductos = new List<Producto>();

            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductosPorNombre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto", nombre);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    if (sdr.HasRows) {

                        while (sdr.Read())
                        {
                            ListProductos.Add(new Producto
                            {
                                Nombre = sdr["Nombre"].ToString(),
                                Presentacion = sdr["Presentacion"].ToString(),
                                PMayoreo = Convert.ToDouble(sdr["PMayoreo"]),
                                Pmenudeo = Convert.ToDouble(sdr["Pmenudeo"]),
                                Existencia = Convert.ToInt16(sdr["Existencia"]),
                                CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"])
                            });
                        }
                        con.Close();
                    }
                    else
                    {
                        ListProductos = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return ListProductos;

            }
            
        }

    }
}