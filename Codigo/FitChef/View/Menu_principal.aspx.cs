using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Menu_principal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Personal_Info_But_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("Altear_Perfil.aspx?id=" + id);
        }

        protected void Receita_But_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("Menu_Receitas.aspx?id=" + id);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("Historico_receitas.aspx?id=" + id);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}