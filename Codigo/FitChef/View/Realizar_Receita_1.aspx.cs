using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FitChef.Models;

namespace FitChef.View
{
    public partial class Realizar_Receita_1 : System.Web.UI.Page
    {
        private Facade fac = new Facade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Collection<int> ids = new Collection<int>();
                ids.Add((int)Application["Receita"]);
                Dictionary<string, string> ings = fac.GetIngredientesEmenta(ids);
                foreach(string k in ings.Keys)
                {
                    ListBox1.Items.Add(k + " , " + ings[k]);
                }
                Collection<string> utensilios = fac.GetUtensiliosFromReceita(ids[0]);
                foreach(string u in utensilios)
                {
                    ListBox2.Items.Add(u);
                }
            }
            
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("Realizar_Receita_2?id=" + id);
        }
    }
}