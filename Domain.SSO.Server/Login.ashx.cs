using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.SSO.Server
{
    /// <summary>
    /// Login1 的摘要说明
    /// </summary>
    public class Login1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string userName = context.Request.QueryString["useName"];
            string password = context.Request.QueryString["password"];


            if (Domain.Security.SmartAuthenticate.AuthenticateUser(userName, password, true))
            {
                string returnUrl = context.Request["returnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                    context.Response.Redirect("default.aspx");
                else
                    context.Response.Redirect(returnUrl);
            }
            else
            {
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}