using Hotel.Model;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.App_Start
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheck = true;
        public const int _PageSize = 10;
        protected Hotel.Model.User UserInfo = null;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsCheck)
                return;
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (flag)
                return;
            GetUser();
            if (UserInfo == null)
            {
                Login(filterContext);
            }
        }

        /// <summary>
        /// 跳转到登录界面
        /// </summary>
        void Login(AuthorizationContext filterContext)
        {
            string target = filterContext.HttpContext.Request.RawUrl.ToString();
            string msg = "对不起，您没有登录或者没有权限访问该页面，即将跳转到首页...";
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //code=-1001001代表未登录
                var ajaxRet = JsonConvert.SerializeObject(new { code = -1001001, msg = msg });
                var json = "{\"code\" : -1001001, \"msg\" : \"" + msg + "\"}";
                filterContext.HttpContext.Response.Write(json);
                filterContext.HttpContext.Response.End();
            }
            else
            {
                filterContext.Result = new RedirectResult($"/Error/Msg?code=1&msg={msg}&url=/");
            }
        }

        void GetUser()
        {
            var token = Cookies.GetCookie(Cookies._CookieUser);
            UserInfo = JsonConvert.DeserializeObject<Hotel.Model.User>(Encryption.Get(token, Encryption._USER));
        }
    }
}