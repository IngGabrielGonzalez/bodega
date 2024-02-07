using Bodega.Bodega.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bodega.Bodega.Entities
{
    public interface IProducto
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<List<string>> ObtenerNombreProducto();
        Task<List<Producto>> ObtenerProductoPorNombre(string nombre);
    }
}
