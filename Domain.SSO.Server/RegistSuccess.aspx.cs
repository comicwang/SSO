using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Domain.SSO.Server
{
    public partial class RegistSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string userName = Request.QueryString[0];
                string password = Request.QueryString[1];
                string token = string.Empty;
                if (Domain.Security.SmartAuthenticate.AuthenticateUser(userName, password, true,out token))
                {
                    string returnUrl = Request["returnUrl"];
                    if (string.IsNullOrEmpty(returnUrl))
                        Response.Redirect("default.aspx");
                    else
                        Response.Redirect(returnUrl);
                }
                else
                {

                }
            }
            catch
            {

            }
        }
    }
}