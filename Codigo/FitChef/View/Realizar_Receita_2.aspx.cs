using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FitChef.Models;

namespace FitChef.View
{
    public partial class Realizar_Receita_2 : System.Web.UI.Page
    {
        private Facade fac = new Facade();
        private static Collection<string> passos = new Collection<string>();
        private static int passo = 0;
        private static Collection<int> ids = new Collection<int>();
        private static Collection<string> dificuldades = new Collection<string>();
        private static Stopwatch sw = Stopwatch.StartNew(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                int id = (int)Application["Receita"];
                Dictionary<int, string> aux = fac.GetPassosFromReceita(id);
                foreach (int key in aux.Keys)
                {
                    ListBox2.Items.Add(aux[key]);
                    passos.Add(aux[key]);
                    ids.Add(key);
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (passo < passos.Count)
            {
                string s = "voice('" + passos[passo++] + "')";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "voice", s, true);
            }
            else
            {
                
                sw.Stop();
                long time = sw.ElapsedMilliseconds;
                int id = int.Parse(Request.QueryString["id"]);
                fac.ReceitaRealizada(id, (int)Application["Receita"], time, dificuldades);
                Application.Add("Realizada", 1);

                Response.Redirect("Lista_Receita.aspx?id=" + Request.QueryString["id"]);

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (passo > 0 && passo <= passos.Count)
            {
                Dictionary<int, Collection<string>> exp = fac.GetExpFromPasso(ids[passo-1]);
                string duvida = TextBox1.Text;
                if(duvida == "repeat" || duvida == "Repeat" || duvida == "Repeat the step" ||
                      duvida == "repeat the step" || duvida == "I don't understand nothing" ||
                      duvida == "i don't understand nothing" || duvida == "I don't understand" ||
                      duvida == "i don't understand")
                {
                    string s = "voice('" + passos[passo-1] + "')";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "voice", s, true);
                    return;
                }
                dificuldades.Add(duvida);
                foreach(int key in exp.Keys)
                {
                    foreach(string s in exp[key])
                    {
                        if (s.Contains(duvida))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "show window",
                                    "shwwindow('" + exp[key][1] + "');", true);
                            return;
                        }
                    }
                }
            }
        }
    }
}