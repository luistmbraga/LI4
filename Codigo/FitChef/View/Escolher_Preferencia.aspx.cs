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
    public partial class Escolher_Preferencia : System.Web.UI.Page
    {
        private Facade fac = new Facade();
        private List<int> id_ings;

        private static Collection<int> ids_like = new Collection<int>();
        private static Collection<int> ids_dislike = new Collection<int>();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                id_ings = new List<int>();

                // faz clear da listbox a cada page load
                for (int i = box_like.Items.Count - 1; i > -1; i--)
                {
                    box_like.Items.RemoveAt(i);
                    box_dislike.Items.RemoveAt(i);
                }

                // Retorona o map de todos os ingredientes presentes na BD
                Dictionary<int, String> ings = fac.GetAllIngredientes();
                ICollection<String> nome_ings = ings.Values;

                // percorrer e guardar as chaves dos ingredientes 
                foreach (int k in ings.Keys)
                {
                    id_ings.Add(k);
                }

                // passa o nome dos ingredientes todos para uma lista
                List<String> nome_ings_list = nome_ings.ToList();

                // adiciona os nomes dos ingredientes a listbox
                int size = nome_ings_list.Count;
                for (int i = 0; i < size; i++)
                {
                    box_like.Items.Add(nome_ings_list.ElementAt(i));
                    box_dislike.Items.Add(nome_ings_list.ElementAt(i));
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

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int index = (int) box_like.SelectedIndex + 1;
            ids_like.Add(index);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int index = (int) box_dislike.SelectedIndex + 1;
            ids_dislike.Add(index);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            try
            {
                fac.SetPreferencesUtilizador(id, ids_like, ids_dislike);
            } catch(InvalidOperationException exception)
            {
                MsgBox(exception.Message, this.Page, this);
            }

            ids_like = new Collection<int>();
            ids_dislike = new Collection<int>();

            string id_uti = Request.QueryString["id"];
            Response.Redirect("Altear_Perfil.aspx?id=" + id_uti);
        }
    }
}