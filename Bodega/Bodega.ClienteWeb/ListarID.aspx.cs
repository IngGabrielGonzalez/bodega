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
    public partial class ListarID : System.Web.UI.Page
    {
        IProducto Prod;
        public ListarID()
        {
            Prod = new ProductoDAL();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Text = "Buscar";
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
        protected void Mensaje(string notification)
        {
            string script = "document.getElementById(" + "'msjnotif'" + ").innerHTML =" + notification + ";";
            ScriptManager.RegisterStartupScript(this, typeof(Page),"Notification", script, true);
        }
        protected async void Button1_Click(object sender, EventArgs e)
        {
            int Idprod;
            Producto producto = new Producto();

            try
            {
                Idprod = Convert.ToInt16(TextBox1.Text);
                if (Idprod < 0)
                { 
                    throw new Exception(); 
                }

                producto = await Prod.ObtenerProducto(Idprod);
                if (producto != null)
                {
                    Label1.Text = producto.Nombre;
                    Label2.Text = producto.Presentacion;
                    Label3.Text = producto.CostoUnitario.ToString();
                    Label4.Text = producto.PMayoreo.ToString();
                    Label5.Text = producto.Pmenudeo.ToString();
                }
                else
                {
                    Mensaje("'No se encontraron coincidencias'");
                    MuestraToast();
                }
            }
            catch (FormatException)
            {
                Mensaje("'Capture Ids positivos'");
                MuestraToast();
            }
            
        }
    }
}