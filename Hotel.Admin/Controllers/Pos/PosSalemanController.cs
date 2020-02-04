using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Bll;
using Hotel.Admin.App_Start;
using NIU.Core.Log;
using NIU.Forum.Common;
using Hotel.Model;

namespace Hotel.Admin.Controllers
{
    public class PosSalemanController : AdminBaseController
    {
        // GET: PosSaleman
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Personal()
        {

            return View(UserContext.CurrentUser);
        }

        /// <summary>
		/// 上传头像
		/// </summary>
		/// <returns></returns>
		[HttpPost]
        public ActionResult UploadPortrait(string portrait)
        {
            var apiResult = new APIResult();
            if (string.IsNullOrWhiteSpace(portrait))
            {
                apiResult.Ret = -1;
                apiResult.Msg = "头像数据为空";
                return Json(apiResult);
            }
            try
            {
                //上传头像
                portrait = BitmapFormate.ImgSave("user", portrait);
                if (portrait != string.Empty)
                {
                    PosSalemanBll.ChangePortrait(UserContext.CurrentUser.Id, portrait);
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

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <returns></returns>
        public JsonResult PersonalEdit(string name, int sex, string tel)
        {
            var apiResult = new APIResult();
            try
            {
                PosSalemanBll.PersonalEdit(UserContext.CurrentUser.Id, name, sex, tel);
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public JsonResult PwdEdit(string oldPwd, string pwd, string cfmPwd)
        {
            var apiResult = new APIResult();
            try
            {
                PosSalemanBll.PwdEdit(UserContext.CurrentUser.Id, oldPwd, pwd, cfmPwd);
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

        public string GetByUserNameAndPwd(string userName, string pwd)
        {
            var user = PosSalemanBll.Login(userName, pwd);
            if (user == null)
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "用户名或密码错误" });
            return JsonConvert.SerializeObject(new { Ret = 0, Msg = "", PosSaleman = user });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new Hotel.Model.PosSaleman());
            var info = PosSalemanBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, string searchName = null)
        {
            var pager = PosSalemanBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.PosSaleman) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(PosSaleman model)
        {
            var apiResult = new APIResult();
            try
            {
                PosSalemanBll.AddOrUpdate(model, (UserEntity)UserContext.CurrentUser);
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

        /// <summary>
        /// 删除
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult Delete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                PosSalemanBll.Delete(id);
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

        /// <summary>
        /// 营销人员查询
        /// </summary>
        /// <returns></returns>
        public ActionResult _YxrySearch()
        {
            return View();
        }

        public string GetList(string searchName)
        {
            var list = PosSalemanBll.GetList(UserContext.CurrentUser.HotelId,searchName);
            return JsonConvert.SerializeObject(list);
        }

        public string GetSearchList(string searchName)
        {
            var list = PosSalemanBll.GetList(UserContext.CurrentUser.HotelId, searchName);
            list = list.Where(a => a.IsEnabled).ToList();
            return JsonConvert.SerializeObject(list);
        }
        
    }
}