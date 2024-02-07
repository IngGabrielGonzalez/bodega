﻿using Bodega.Bodega.Entities;
using Bodega.Contratos; 
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bodega.Bodega.DAL
{
    public class AgregarDAL : IAgregar
    {
        string dbconexion;
        public AgregarDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }

        public async Task<bool> RegistrarVenta(Venta Vnta)
        {
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("AgregaVenta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductoId", Vnta.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", Vnta.Cantidad);
                cmd.Parameters.AddWithValue("@TotalVenta", Vnta.TotalVenta);
                cmd.Parameters.AddWithValue("@Fecha", Vnta.Fecha);

                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return true;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                    con.Close();
                    return false;
                }

            }
        }
    }
}