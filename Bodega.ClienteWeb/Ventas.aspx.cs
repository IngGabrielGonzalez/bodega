using Bodega.Bodega.Entities;
using Bodega.Bodega.DAL;
using Bodega.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Bodega.Bodega.ClienteWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        IVentas Prod;
        Producto producto;
        int Idprod;

        Venta venta;
        IAgregar AgregarVenta;
        static double total;
        public Ventas()
        {
            Prod = new VentasDAL();
            producto = new Producto();

            AgregarVenta = new AgregarDAL();
            venta = new Venta();
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

        protected void Modal(string nommodal)
        {
            string csname1 = "VentaModal";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            StringBuilder cstext1 = new StringBuilder();

            cstext1.Append("<script type=text/javascript>");
            cstext1.Append("const myModal = new bootstrap.Modal(" +nommodal + ",{keyboard: true, focus: true});");
            cstext1.Append("myModal.show();");
            cstext1.Append("</script>");

            cs.RegisterStartupScript(cstype, csname1, cstext1.ToString());

        }
        protected async void Page_Load(object sender, EventArgs e)
        {
            List<string> ProdId = new List<string>();
            Label2.Text = "Cantidad";

            Button2.Text = "Registrar_venta";
            Button2.Enabled = false;

            if (!IsPostBack)
            {
                RadioButton1.GroupName = "Radio";
                RadioButton2.GroupName = "Radio";
                DropDownList1.AutoPostBack = true;

                Button1.Text = "Calcular";
                Label3.Text = "Total a Pagar";
                ProdId = await Prod.ObtenerProductoId();//Se consulta la BD, retorna Ids de productos y almacena en lista

                if (ProdId.Count > 0 ) //condicional si hay ids de productos
                {
                    DropDownList1.Items.Clear(); //Se limpia el dropdown
                    DropDownList1.Items.Add("Seleccione Id");

                    for ( int i = 0; i< ProdId.Count; ++i)
                    {
                        DropDownList1.Items.Add(ProdId[i]);
                    }

                }
                else
                {
                    Mensaje("'No se devolvieron Ids de los productos'");
                    MuestraToast();
                }

            }

        }

        protected async void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Idprod = Convert.ToInt16(DropDownList1.SelectedItem.ToString());
                if( Idprod < 0) throw new Exception();

                producto = await Prod.ObtenerProducto(Idprod);

                if (Prod != null) Label1.Text = "Descripción: " + producto.Nombre + " " + producto.Presentacion;
                else
                {
                    Mensaje("'No se encontraron coincidencias'");
                    MuestraToast();
                }

            }
            catch   (Exception x)
            {
                Mensaje("'" + x.Message + "'");
                MuestraToast();
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            int cantidad;
            Label3.Text = "";
            try
            {
                Idprod = Convert.ToInt16(DropDownList1.SelectedItem.ToString());
                producto = await Prod.ObtenerProducto(Idprod);
                cantidad = Convert.ToInt16(TextBox1.Text);

                if (cantidad < 0) throw new ArithmeticException();
                else
                {
                    if ((!RadioButton1.Checked) && !RadioButton2.Checked)
                    {
                        Mensaje("'Seleccione Precio de Mayoreo o Menudeo'");
                        MuestraToast();
                    }
                    else
                    {

                        if (RadioButton1.Checked)
                        {
                            total = producto.PMayoreo * cantidad;

                            if (CheckBox1.Checked)
                            {
                                total = total - (total * 0.20);
                            }

                        }
                        if (RadioButton2.Checked)
                        {
                            total = producto.PMenudeo * cantidad;
                            if (CheckBox1.Checked)
                            {
                                total = total - (total * 0.20);
                            }
                        }

                    }
                    Label3.Text = "Total a Pagar: " + total.ToString();
                    Button2.Enabled = true;
                }


            }
            catch (FormatException)
            {
                Mensaje("'Seleccione Id o capture cantidad'");
                MuestraToast();
            }
            catch (ArithmeticException)
            {
                Mensaje("'Cantidades Positivas'");
                MuestraToast();
            }
            catch (Exception)
            {
                Mensaje("'Seleccione Id del producto'");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string modal = "'#Pregunta'";
            Modal(modal);

        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            venta.ProductoId = Convert.ToInt16(DropDownList1.SelectedItem.ToString());
            venta.Cantidad = Convert.ToInt16(TextBox1.Text);
            venta.TotalVenta = (float)total;
            venta.Fecha = DateTime.Now;

            if (await AgregarVenta.RegistrarVenta(venta))
            {
                Mensaje("'La venta Se Registró  Satisfactoriamente'");
                MuestraToast();
            }
            else
            {
                Mensaje("'Ocurrió un Error al Registrar la Venta'");
                MuestraToast();
            }

        }
    }
}