using Bodega.Bodega.Entities;
using Bodega.Bodega.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bodega.Bodega.ClienteWeb
{
    public partial class ListarPorNombre : System.Web.UI.Page
    {
        IProducto ListNombre;
        public ListarPorNombre()
        {
            ListNombre = new ProductoDAL();
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Notification", script, true);
        }

        protected void GeneraColumnasGrid()
        {
            BoundField colum = new BoundField();
            colum = new BoundField //Se crea la columna del gridview
            {
                HeaderText = "Nombre",
                DataField = "Nombre"
            };
            GridView1.Columns.Add(colum); //Se agrega la columna al gridview

            colum = new BoundField();// se crea una nueva instancia
            colum.HeaderText = "Presentación";
            colum.DataField = "Presentacion";
            GridView1.Columns.Add(colum);

            colum = new BoundField();// se crea una nueva instancia
            colum.HeaderText = "PrecioMayoreo";
            colum.DataField = "PMayoreo";
            GridView1.Columns.Add(colum);

            colum = new BoundField();// se crea una nueva instancia
            colum.HeaderText = "PrecioMenudeo";
            colum.DataField = "Pmenudeo";
            GridView1.Columns.Add(colum);

            colum = new BoundField();// se crea una nueva instancia
            colum.HeaderText = "Existencia";
            colum.DataField = "Existencia";
            GridView1.Columns.Add(colum);


            colum = new BoundField();// se crea una nueva instancia
            colum.HeaderText = "CostoUnitario";
            colum.DataField = "CostoUnitario";
            GridView1.Columns.Add(colum);
           
        }
        protected async void Page_Load(object sender, EventArgs e)
        {
            List<string> NombreProd = new List<string>();

            if (!IsPostBack)
            {
                DropDownList1.AutoPostBack = true; //Se habilita el postback del dropdownlist

                NombreProd = await ListNombre.ObtenerNombreProducto();//Se consulta la BD
                if (NombreProd.Count > 0)
                {
                    DropDownList1.Items.Clear();
                    DropDownList1.Items.Add("Seleccione Producto");

                    for (int i = 0; i < NombreProd.Count; ++i)
                    {
                        DropDownList1.Items.Add(NombreProd[i]);
                    }
                }
                else
                {
                    Mensaje("No se devolviero nombres de los productos");
                    MuestraToast();
                }

            }
            
        }

        protected async void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Nombre;
            List<Producto> ListProductos = new List<Producto>();

            Nombre = DropDownList1.SelectedItem.ToString();
            ListProductos = await ListNombre.ObtenerProductoPorNombre(Nombre);

            if (ListProductos != null)
            {
                GeneraColumnasGrid();
                GridView1.DataSource = ListProductos;
                GridView1.DataBind();
                //GridView1.Columns.Clear();
            }
            else
            {
                Mensaje("'No se encontraron coincidencias'");
                MuestraToast();
            }
        }
    }
}