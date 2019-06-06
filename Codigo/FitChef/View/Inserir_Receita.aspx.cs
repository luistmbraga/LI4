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
    public partial class Inserir_Receita : System.Web.UI.Page
    {

        private Facade fac = new Facade();

        private List<int> id_passos = new List<int>();
        private List<int> id_ings = new List<int>();

        private static Collection<int> ids_passos = new Collection<int>();
        private static Collection<int> ids_ings = new Collection<int>(); 

        // dicionario com o id do passo, e com id do ingrediente e quantidade no value
        private static Dictionary<int,Dictionary<int,int>> map = new Dictionary<int,Dictionary<int,int>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<int, string> passos = fac.getAllPassos();
                List<string> desc_passos = passos.Values.ToList();

                id_passos = new List<int>();

                int size_p = desc_passos.Count; 
                for(int i = 0; i<size_p; i++)
                {
                    DropDownList1.Items.Add(desc_passos.ElementAt(i));
                }

                foreach(int k in passos.Keys)
                {
                    id_passos.Add(k);
                }

                Dictionary<int, string> ings = fac.GetAllIngredientes();
                List<string> desc_ings = ings.Values.ToList();
                id_ings = new List<int>();

                int size_i = desc_ings.Count; 
                for(int i = 0; i < size_i; i++)
                {
                    DropDownList2.Items.Add(desc_ings.ElementAt(i));
                }
                foreach(int key in ings.Keys)
                {
                    id_ings.Add(key);
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            int id_receita;
            int id_utensilio;

            if (TextBox1.Text != null && TextBox2.Text != null && TextBox3.Text != null) {

                // adiciona receita e retorna o id da receita adicionada
                string nome = TextBox1.Text;
                int nutricional = Int32.Parse(TextBox2.Text);
                id_receita = fac.AddReceita(nome, nutricional);
                // guarda o id da receita
                Application.Clear();
                Application.Add("ID RECEITA", id_receita);

                // adiciona utensilio e retorna o id do utensilio adicionado
                string utensilio = TextBox3.Text;
                id_utensilio = fac.addUtensilio(404,utensilio);

                // adiciona o id da receita criado e do utensilio a tabela receita utensilio
                fac.addReceitaUtensilio(id_receita, id_utensilio);

                fac.insertReceitaPassoIngrediente(id_receita, map);
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // adiciona receita e retorna o id da receita adicionada
            string nome = TextBox1.Text;
            int nutricional = Int32.Parse(TextBox2.Text);
            int id_receita = fac.AddReceita(nome, nutricional);
            Response.Redirect("Inserir_Receita_NewStep.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            int index = (int) DropDownList1.SelectedIndex + 1;
            /*
            ids_passos.Add(index); 
            */
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)DropDownList2.SelectedIndex + 1;
            ids_ings.Add(index);
        }

        // guarda os passos
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (map.ContainsKey(DropDownList1.SelectedIndex + 1))
            {
                map[DropDownList1.SelectedIndex + 1].Add(DropDownList2.SelectedIndex + 1, Int32.Parse(TextBox4.Text));
            } else
            {
                map.Add(DropDownList1.SelectedIndex + 1, new Dictionary<int, int>());
                map[DropDownList1.SelectedIndex + 1].Add(DropDownList2.SelectedIndex + 1, Int32.Parse(TextBox4.Text));
            }
        }
    }
}