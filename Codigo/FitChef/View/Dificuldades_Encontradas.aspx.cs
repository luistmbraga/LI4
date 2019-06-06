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
    public partial class Dificuldades_Encontradas : System.Web.UI.Page
    {
        private Facade fac = new Facade();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int rec = (int)Application["HistRec"];
                Collection<string> dificuldades = fac.GetDificuldadesFromReceita(int.Parse(Request.QueryString["id"]), rec);
                foreach(string s in dificuldades)
                {
                    ListBox1.Items.Add(s);
                }
            }
        }
    }
}