using FitChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Inserir_Receita_NewStep : System.Web.UI.Page
    {

        private Facade fac = new Facade();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<int, string> ings = fac.GetAllIngredientes();
                List<string> desc_ings = ings.Values.ToList();

                int size = desc_ings.Count;
                for (int i = 0; i < size; i++)
                {
                    DropDownList1.Items.Add(desc_ings.ElementAt(i));
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            int id_passo; 

            if (TextBox6.Text != null && TextBox7 != null)
            {
                fac.addIngrediente(404, TextBox6.Text, TextBox7.Text, 0);
            }

            if(TextBox1.Text != null && TextBox2.Text != null && TextBox3.Text != null 
               && TextBox5 != null && DropDownList1.SelectedIndex > -1)
            {
                string explanation = TextBox1.Text;
                string duvida = TextBox2.Text;
                string tag = TextBox3.Text;
                string quantidade = TextBox5.Text;

                // adiciona passo
                id_passo = fac.addPasso(404, explanation);
                
                // adiciona explicação, caso possua dúvida com video ou não 
                if (CheckBox1.Checked)
                {
                    fac.addExplicacao(404, duvida, true, tag, id_passo);
                } else
                {
                    fac.addExplicacao(404, duvida, false, tag, id_passo);
                }

                Response.Redirect("Inserir_Receita.aspx");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}