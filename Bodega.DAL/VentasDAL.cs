using Bodega.Bodega.Entities;
using Bodega.Contratos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bodega.Bodega.DAL
{
    public class VentasDAL : IVentas
    {
        string dbconexion;
        public VentasDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }
        public async Task<List<string>> ObtenerProductoId()
        {
            List<string> ProductoId = new List<string>();

            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductoId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            ProductoId.Add(sdr["ProductoId"].ToString());
                        }
                        con.Close();
                    }
                    else
                    {
                        ProductoId = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return ProductoId;
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
                            Pr.Nombre = sdr["Nombre"].ToString();
                            Pr.Presentacion = sdr["Presentacion"].ToString();
                            Pr.PMayoreo = Convert.ToDouble(sdr["PMayoreo"]);
                            Pr.PMenudeo = Convert.ToDouble(sdr["Pmenudeo"]);
                        }
                        con.Close();
                    }
                    else
                    {
                        Pr = null;
                    }
                }
                catch (Exception) 
                {
                    con.Close(); 
                }
                return Pr;
            }
        }
    }
}