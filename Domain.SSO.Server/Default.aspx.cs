﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Domain.SSO.Server
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["holder"] == null)
            {
                Session["holder"] = 1;
            }

            if (Domain.Security.SmartAuthenticate.LoginUser != null)
            {
                this.lblMessage.Text = "用户："
                    + Domain.Security.SmartAuthenticate.LoginUser.UserName;
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }
    }
}