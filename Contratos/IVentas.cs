using Bodega.Bodega.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bodega.Contratos
{
    public interface IVentas
    {
        Task<Producto> ObtenerProducto(int id);
        Task<List<string>> ObtenerProductoId();
    }
}
