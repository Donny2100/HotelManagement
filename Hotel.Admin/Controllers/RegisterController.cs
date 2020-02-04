using Hotel.Bll;
using Hotel.Model;
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
    /// 注册
    /// </summary>
    public class RegisterController : Controller
    {
        // GET: Register

        private ValidationCode vCode = new ValidationCode();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Reg(string hotelName, string userName, string pwd, string CfmPwd, string handler, string tel, string code)
        {
            var apiResult = new APIResult();
            try
            {
                if (Session[vCode.SessionName] == null)
                    throw new OperationExceptionFacade("验证码已过期");
                else if (code.ToLower() != Session[vCode.SessionName].ToString().ToLower())
                    throw new OperationExceptionFacade("验证码错误");

                if (string.IsNullOrWhiteSpace(hotelName))
                    throw new OperationExceptionFacade("酒店名称不可为空");
                if (string.IsNullOrWhiteSpace(userName))
                    throw new OperationExceptionFacade("用户名不可为空");
                if (string.IsNullOrWhiteSpace(pwd))
                    throw new OperationExceptionFacade("密码不可为空");
                if (CfmPwd != pwd)
                    throw new OperationExceptionFacade("两次输入的密码不相同");
                if (string.IsNullOrWhiteSpace(handler))
                    throw new OperationExceptionFacade("联系人不可为空");
                if (string.IsNullOrWhiteSpace(tel))
                    throw new OperationExceptionFacade("联系电话不可为空");

                var hotelId = IdBuilder.NextLongID();

                var user = new Hotel.Model.User()
                {
                    Id = IdBuilder.NextLongID(),
                    UserName = userName,
                    Pwd = pwd,
                    HotelId = hotelId,
                    UserType = 2,
                    GId=2,//总店管理员
                    CDate = TypeConvert.DateTimeToInt(DateTime.Now)
                };
                if (UserBll.Reg(user))
                {
                    var hotel = new HotelModel()
                    {
                        Id = hotelId,
                        Name = hotelName,
                        Handler = handler,
                        Tel = tel,
                        CDate = TypeConvert.DateTimeToInt(DateTime.Now)
                    };
                    bool flag = HotelBll.RegHotel(hotel);
                    if (!flag)
                        throw new OperationExceptionFacade("酒店注册失败");
                }
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            return Json(apiResult);
        }
    }
}