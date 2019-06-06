using FitChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FitChef.View
{
    public partial class Altear_Perfil : System.Web.UI.Page
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
            string id = Request.QueryString["id"];
            Response.Redirect("Escolher_Preferencia.aspx?id=" + id);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            bool result = false;
            int id = int.Parse(Request.QueryString["id"]);

            try
            {
                result = fac.SetUserInfo(id,TextBox1.Text, TextBox2.Text);

                if (result == false)
                {
                    MsgBox("Couldn't update your user info!", this.Page, this);
                }

            } catch(InvalidOperationException exception)
            {
                MsgBox(exception.Message, this.Page, this);
            }
        }

    }
}