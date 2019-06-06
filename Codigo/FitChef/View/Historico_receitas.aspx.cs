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
    public partial class Historico_receitas : System.Web.UI.Page
    {
        private Facade fac = new Facade();
        private static Collection<int> ids = new Collection<int>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<int, string> hist = fac.GetHistorico(int.Parse(Request.QueryString["id"]));
                foreach(int key in hist.Keys)
                {
                    ids.Add(key);
                    ListBox1.Items.Add(hist[key]);
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

        protected void Dific_Encontradas_Click(object sender, EventArgs e)
        {
            int selecionado = ListBox1.SelectedIndex;
            if (selecionado > -1)
            {
                Application.Remove("HistRec");
                Application.Add("HistRec", ids[selecionado]);
                Response.Redirect("Dificuldades_Encontradas.aspx?id=" + Request.QueryString["id"]);
            }
            else
            {
                MsgBox("Yet not recipe consult selected!", this.Page, this);
            }
        }
    }
}