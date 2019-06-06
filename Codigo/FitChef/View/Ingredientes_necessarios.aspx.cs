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
    public partial class Ingredientes_necessarios : System.Web.UI.Page
    {
        Facade fac = new Facade();
        private static Collection<int> ementa = new Collection<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ementa = new Collection<int>();
                ementa = (Collection<int>)Application["Ementa"];
                Dictionary<string, string> escrever = fac.GetIngredientesEmenta(ementa);
                foreach(string k in escrever.Keys)
                {
                    ListBox1.Items.Add(k + " , " + escrever[k]);
                }
            }
        }
    }
}