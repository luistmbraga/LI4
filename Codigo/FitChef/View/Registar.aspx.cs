using FitChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Registar : System.Web.UI.Page
    {

        private Facade fac = new Facade();

        protected void Page_Load(object sender, EventArgs e)
        {

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

            Boolean res = false;

            string name = Convert.ToString(Box_name.Value);
            string pass = Convert.ToString(Box_pass.Value);
            string user = Convert.ToString(Box_user.Value);
            string email = Convert.ToString(Box_email.Value);

            if (Box_name != null && !string.IsNullOrWhiteSpace(name) && 
               Box_pass != null && !string.IsNullOrWhiteSpace(pass) &&
               Box_user != null && !string.IsNullOrWhiteSpace(user) &&
               Box_email != null && !string.IsNullOrWhiteSpace(email))
            {

                try
                {
                    res = fac.AddUtilizador(404, user, pass, name, email, true);

                    if (res == true)
                    {
                        MsgBox("Account was created sucessfully!", this.Page, this);

                    }
                    else
                    {
                        MsgBox("There already exists a user with that username, try a new username!", this.Page, this);
                    }

                } catch(InvalidOperationException)
                {
                    throw new System.InvalidOperationException("User already exists! Insert a new username.");
                }

            }
        }

    }
}