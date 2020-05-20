using Domain.SSO.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Domain.Security
{
    public class SmartAuthenticate : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += ContextAuthenticateRequest;
        }

        public void ContextAuthenticateRequest(object sender, EventArgs e)
        {
            //得到当前的HttpContext
            var context = ((HttpApplication)sender).Context;
            //得到FormsAuth的Cookie信息
            HttpCookie authCookie = context.Request.Cookies[FormsAuthCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = null;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (Exception ex)
                {

                }

                if (authTicket != null && !string.IsNullOrWhiteSpace(authTicket.UserData))
                {
                    int delimiter = authTicket.UserData.IndexOf(AUTH_TKT_USERDATA_DELIMITER);
                    if (delimiter != -1)
                    {
                        string securityValidationKey = authTicket.UserData.Substring(0, delimiter).Trim();

                        if (securityValidationKey == SecurityValidationKey)
                        {
                            SmartIdentity identity = new SmartIdentity(authTicket.Name, true);
                            SmartPrincipal principal = new SmartPrincipal(identity);

                            context.User = principal;
                            return;
                        }
                    }
                }
            }
            //创建一个空的未认证用户信息
            SmartIdentity unauthIdentity = new SmartIdentity(string.Empty, false);
            SmartPrincipal unauthPrincipal = new SmartPrincipal(unauthIdentity);
            context.User = unauthPrincipal;
        }

        #endregion

        #region 验证用户名和密码

        public static SSOUser ValidateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var userEntity = new LoginUserModel();
            userEntity.UserName = userName;
            string psw = MD5Helper.MD5Encrypt(password);
            DataSet result = SqlHelper.ExecuteDataset($"select * from login_user where Account='{userName}' and Password='{psw}'");
            if (result!=null&&result.Tables.Count>0&&result.Tables[0].Rows.Count>0)
            {
                DataRow row = result.Tables[0].Rows[0];
                SSOUser sSOUser = new SSOUser()
                {
                    UserName = row["UserName"].ToString(),
                    Account = row["Account"].ToString(),
                    Department = row["Department"].ToString(),
                    Orgin = row["Orgin"].ToString(),
                    Role = row["Role"].ToString()
                };
                return sSOUser;
            }
            return null;
        }

        #endregion

        #region 注册账号

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool RegistAccount(SSOUser user, out string message)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Account) || string.IsNullOrEmpty(user.Password))
                {
                    message = "注册失败,信息不全";
                    return false;
                }
                object result = SqlHelper.ExecuteScalar($"select * from login_user where Account='{user.Account}'");
                if (result != null)
                {
                    message = "注册失败,当前账号已存在";
                    return false;
                }
                user.Password = MD5Helper.MD5Encrypt(user.Password);
                int line = SqlHelper.ExecuteNonQuery($"insert into login_user values('{Guid.NewGuid()}','{user.UserName}','{user.Password}','{user.Account}','{DateTime.Now}','{DateTime.Now}','','','')");
                if (line > 0)
                {
                    message = "恭喜你，注册成功！";
                    return true;
                }
                else
                {
                    message = "注册失败，插入数据库失败";
                    return false;
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
                return false;
            }

        }

        #endregion

        #region 用户认证

        /// <summary>
        /// 保存在客户端的认证信息Cookie名称
        /// </summary>
        public static string FormsAuthCookieName
        {
            get { return "Domain.Security"; }
        }

        /// <summary>
        /// 用来分割用户票据中用户信息的分隔符
        /// </summary>
        public static string AUTH_TKT_USERDATA_DELIMITER
        {
            get { return "|"; }
        }

        /// <summary>
        /// 安全校验码
        /// </summary>
        public static string SecurityValidationKey
        {
            // Domain.Security 的MD5编码
            get { return "905ACE6698EE001A4F1F38D3BE1EA1A3"; }
        }

        /// <summary>
        /// 认证用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public static bool AuthenticateUser(string username, string password, bool rememberMe,out string myticket)
        {
            string un = (username ?? string.Empty).Trim();
            string pw = (password ?? string.Empty).Trim();


            if (!string.IsNullOrWhiteSpace(un) && !string.IsNullOrWhiteSpace(pw))
            {
                SSOUser isValidated = ValidateUser(un, pw);

                if (isValidated != null)
                {
                    HttpContext context = HttpContext.Current;
                    DateTime expirationDate = DateTime.Now.Add(FormsAuthentication.Timeout);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        un,
                        DateTime.Now,
                        expirationDate,
                        rememberMe,
                        string.Format("{0}{1}{2}{1}{3}{4}", SecurityValidationKey, AUTH_TKT_USERDATA_DELIMITER, un, pw,Guid.NewGuid()),
                        FormsAuthentication.FormsCookiePath
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //持久化Token
                    SSOToken token = new SSOToken(encryptedTicket);

                    token.User = isValidated;
                    //token.User.UserName = Domain.Security.SmartAuthenticate.LoginUser.UserName;
                    //token.LoginID = Session.SessionID;
                    Domain.SSO.Entity.SSOToken.SSOTokenList.Add(token);

                    HttpCookie cookie = new HttpCookie(FormsAuthCookieName, encryptedTicket);
                    cookie.Expires = rememberMe ? expirationDate : DateTime.MinValue;
                    cookie.HttpOnly = true;
                    cookie.Path = "/";
                    //cookie.Domain = "domain.com";
                    context.Response.Cookies.Set(cookie);

                    myticket = encryptedTicket;
                    return true;
                }
            }
            myticket = string.Empty;
            return false;
        }

        #endregion

        #region 注销登陆

        /// <summary>
        /// 注销登陆
        /// </summary>
        public static void SignOut()
        {
            // using a custom cookie name based on the current blog instance.
            HttpCookie cookie = new HttpCookie(FormsAuthCookieName, string.Empty);
            cookie.Expires = DateTime.Now.AddYears(-3);
            cookie.HttpOnly = true;
            cookie.Path = "/";
            //cookie.Domain = "domain.com";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        public static string LoginUserItemKey
        {
            get { return "__LoginUser"; }
        }

        public static LoginUserModel LoginUser
        {
            get
            {
                if (HttpContext.Current.User == null
                    || HttpContext.Current.User.Identity == null
                    || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    return null;
                }
                else
                {
                    if (!HttpContext.Current.Items.Contains(LoginUserItemKey))
                    {
                        var userEntity = new LoginUserModel();
                        userEntity.UserName = HttpContext.Current.User.Identity.Name;
                        HttpContext.Current.Items[LoginUserItemKey] = userEntity;
                    }
                    return HttpContext.Current.Items[LoginUserItemKey] as LoginUserModel;
                }
            }
        }
    }
}
