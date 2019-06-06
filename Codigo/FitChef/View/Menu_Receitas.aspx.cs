using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Menu_Receitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Application.Clear();
            Application.Add("Gosto", 0);
            Response.Redirect("Lista_Receita.aspx?id=" + id);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Application.Clear();
            Application.Add("Gosto", 1);
            Response.Redirect("Lista_Receita.aspx?id=" + id);
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("Ementa_Semanal.aspx?id=" + id);
        }
    }
}