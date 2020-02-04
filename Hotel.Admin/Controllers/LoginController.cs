using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Common.BLL.UA;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : Controller
    {
        // GET: Login
        private ValidationCode vCode = new ValidationCode();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Log(string userName, string pwd, string code)
        {
            var apiResult = new APIResult();

            try {
                if (Session[vCode.SessionName] == null)
                    throw new OperationExceptionFacade("验证码已过期");
                else if (code.ToLower() != Session[vCode.SessionName].ToString().ToLower())
                    throw new OperationExceptionFacade("验证码错误");
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pwd))
                    throw new OperationExceptionFacade("用户名或密码不可为空");

                UAParserUserAgent userAgent = new UAParserUserAgent(HttpContext);
                int deviceType = 1;
                if (userAgent.IsMobileDevice)
                    deviceType = (int)DeviceTypeEnum.手机;
                else if (userAgent.IsTablet)
                    deviceType = (int)DeviceTypeEnum.平板;
                else
                    deviceType = (int)DeviceTypeEnum.电脑;
                var user = UserBll.Login(userName, pwd);
                if (user == null)
                    throw new OperationExceptionFacade("用户名或密码错误");

                string token = string.Empty;
                if (user != null)
                {
                    token = UserTokenBL.GetNewTokenAndToCache(UserTokenType.WebToken, user.Id.ToString(), "", 00001, "", 600);
                    SetTokenToCookies(token);
                    //更新权限
                    ContainerHelper.ResolvePerHttpRequest<IPermissionBL>().UpdateCache(user.GId);
                }

                LoginLog log = new LoginLog
                {
                    Id = IdBuilder.NextLongID(),
                    UserId = user.Id,
                    UserName = userName,
                    GroupId = user.Group.Id,
                    Phone = user.Tel,
                    Ip = GetClientIP.GetClientIPAddress(),
                    Address = string.Empty,

                    Device = deviceType,
                    OS = userAgent.OS.ToString(),
                    Agent = userAgent.UserAgent.ToString(),
                    IsSuccess = user == null ? false : true,
                    Token = token,
                    CDate = TypeConvert.DateTimeToInt(DateTime.Now)
                    //SysType = 2,
                    //LogType = 1,
                };
                LoginLogBL.Add(log,false);
            }
            catch (Exception ex) {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            return Json(apiResult);
        }

        /// <summary>
		/// 将token写入cookie
		/// </summary>
		/// <param name="model"></param>
		void SetTokenToCookies(string token, int time = 60 * 24)
        {
            //Cookies.WriteCookie(Cookies._CookieUser, Encryption.Set(token, Encryption._USER), time);
            NIU.Core.Cookies.WriteCookie(NIU.Core.Cookies._CookieAdminName,"token", Encryption.Set(token, Encryption._USER), time);
        }
    }
}