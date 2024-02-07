using Bodega.Bodega.DAL;
using Bodega.Bodega.Entities;
//using Bodega.Bodega.Contratos;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bodega.Bodega.ClienteWeb
{
    public partial class Listar : System.Web.UI.Page
    {
        IProducto Prod;
        public Listar()
        {
            Prod = new ProductoDAL();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MuestraToast()
        {
            String csname1 = "Toast";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            StringBuilder CadenaToast = new StringBuilder();

            CadenaToast.Append("<script type=text/javascript>");
            CadenaToast.Append("let myAlert = document.querySelector('.toast');");
            CadenaToast.Append("let bsAlert = new bootstrap.Toast(myAlert,{autohide: true, delay:2000});");
            CadenaToast.Append("bsAlert.show();");
            CadenaToast.Append("</script>");

            cs.RegisterStartupScript(cstype, csname1, CadenaToast.ToString());

        }
        protected void Mensaje(string notification) {
            string script = "document.getElementById(" + "'msjnotif'" + ").innerHTML =" + notification + ";";
            ScriptManager.RegisterStartupScript(this, typeof(Page),"Notification", script, true);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            List<Producto> ListProductos = new List<Producto>();
            ListProductos = await Prod.ObtenerProductos();

            if (ListProductos == null)
            {
                Mensaje("'No se devolvieron registros'");
                MuestraToast();
                return null;
            }
            return ListProductos;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        //public IQueryable<Bodega.Bodega.Entities.Producto> ObtenerProductos()
        //{
        //    return null;
        //}
    }
}