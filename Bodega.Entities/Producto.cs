﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bodega.Bodega.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public double PMayoreo { get; set; }
        public double PMenudeo { get; set; }
        public int Existencia { get; set; }
        public double CostoUnitario { get; set; }
        public string ImagenPath { get; set; }


    }
}