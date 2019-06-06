using FitChef.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Lista_Receita : System.Web.UI.Page
    {

        private Facade fac = new Facade();
        private static Collection<int> ids;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application.AllKeys.Contains("Realizada"))
                {
                    if ((int)Application["Realizada"] == 1)
                    {
                        MsgBox("Recipe realized successfully!", this.Page, this);
                        Application.Remove("Realizada");
                    }
                }
                // faz clear da listbox a cada page load
                for (int i = ListBox1.Items.Count - 1; i > -1; i--)
                {
                    ListBox1.Items.RemoveAt(i);
                }
                Dictionary<int, string> recs = new Dictionary<int, string>();
                if ((int)Application["Gosto"] == 0)
                {
                     recs = fac.GetAllReceitas();
                }
                else
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    try
                    {
                        
                        recs = fac.GetReceitasPref(id);
                    }
                    catch(InvalidOperationException exception)
                    {
                        MsgBox(exception.Message, this.Page, this);
                        Response.Redirect("Menu_Receitas.aspx?id=" + id.ToString());
                    }
                }

                ids = new Collection<int>();
                foreach(int k in recs.Keys)
                {
                    ListBox1.Items.Add(recs[k]);
                    ids.Add(k);
                }
            }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int idR = ListBox1.SelectedIndex;
            if (idR!=-1)
            {
                //Application.Clear();
                Application.Remove("Receita");
                Application.Add("Receita", ids[idR]);
                Response.Redirect("Realizar_Receita_1.aspx?id=" + Request.QueryString["id"]);
            }
            else
            {
                MsgBox("Yet not have a select a recipe!", this.Page, this);
            }
        }
    }
}