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
    public partial class Ementa_Semanal : System.Web.UI.Page
    {
        private Facade fac = new Facade();
        private Dictionary<int, Collection<string>> ementa;
        private static Collection<int> idsRecs = new Collection<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Collection<Collection<string>> escrever = new Collection<Collection<string>>();
                int id = int.Parse(Request.QueryString["id"]);
                ementa = fac.GetEmentaSemanal(id);
                int size = ementa.Count;
                int count = 14-size;
                idsRecs = new Collection<int>();
                foreach(int k in ementa.Keys){
                    escrever.Add(ementa[k]);
                    idsRecs.Add(k);
                }
                
                for(int i = 0; i<count;)
                {
                    int j = i;
                    foreach(int k in ementa.Keys)
                    {
                        if (j >= count) break;
                        escrever.Add(ementa[k]);
                        idsRecs.Add(k);
                        j++;
                    }
                    i = j;
                }

                string a0 = "Segunda Feira (almoço) -->";
                foreach (string s in escrever[0])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0+ " calories");
                string j0 = "Segunda Feira (jantar) -->";
                foreach(string s in escrever[1])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Terça Feira (almoço) -->";
                foreach(string s in escrever[2])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0 + " calories");
                j0 = "Terça Feira (jantar) -->";
                foreach(string s in escrever[3])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Quarta Feira (almoço) -->";
                foreach(string s in escrever[4])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0+ " calories");
                j0 = "Quarta Feira (jantar) -->";
                foreach (string s in escrever[5])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Quinta Feira (almoço) -->";
                foreach (string s in escrever[6])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0 + " calories");
                j0 = "Quinta Feira (jantar) -->";
                foreach (string s in escrever[7])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Sexta Feira (almoço) -->";
                foreach (string s in escrever[8])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0 + " calories");
                j0 = "Sexta Feira (jantar) -->";
                foreach (string s in escrever[9])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Sábado (almoço) --> ";
                foreach (string s in escrever[10])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0 + " calories");
                j0 = "Sábado (jantar) -->";
                foreach (string s in escrever[11])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
                a0 = "Domingo (almoço) -->";
                foreach (string s in escrever[12])
                {
                    a0 += " " + s;
                }
                ListBox1.Items.Add(a0 + " calories");
                j0 = "Domingo (jantar) -->";
                foreach (string s in escrever[11])
                {
                    j0 += " " + s;
                }
                ListBox1.Items.Add(j0 + " calories");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Application.Clear();
            Application.Add("Ementa", idsRecs);

            Response.Redirect("Ingredientes_necessarios.aspx");

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}