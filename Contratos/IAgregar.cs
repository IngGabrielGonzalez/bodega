using Bodega.Bodega.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bodega.Contratos
{
    public interface IAgregar
    {
        Task<bool> RegistrarVenta(Venta vnta);
    }
}
