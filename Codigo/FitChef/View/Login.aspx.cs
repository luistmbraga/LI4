using FitChef.Data_Access;
using FitChef.Models;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Login : System.Web.UI.Page
    {
        private Facade fac = new Facade();

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        public void Button1_Click(object sender, EventArgs e)
        {

            string user = Convert.ToString(Namebox.Value);
            string pass = Convert.ToString(Passbox.Value);

            if (Namebox != null && !string.IsNullOrWhiteSpace(user) && Passbox != null && !string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    //   string tipo_user = fac.Login(BoxUser.Text, BoxPass.Text);
                    
                    if (fac.Login(user, pass).Equals("Cliente"))
                    {
                        Response.Redirect("Menu_principal.aspx?id=" + fac.returnUtilizador(user));   // muda para ménu principal

                    }
                    else
                    {
                        Response.Redirect("Menu_admin.aspx?id=" + fac.returnUtilizador(user));
                    }


                } catch (InvalidOperationException exception)
                {
                       MsgBox(exception.Message, this.Page, this);
                }

            }
        } 

        /*
            protected void BoxUser_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BoxPass_TextChanged(object sender, EventArgs e)
        {

        }*/

        protected void Namebox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Passbox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registar.aspx");
        }
    }
}