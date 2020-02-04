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
    public class EmployeeController : AdminBaseController
    {
        // GET: Employee
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
                portrait = BitmapFormate.ImgSave("employee", portrait);
                if (portrait != string.Empty)
                {
                    EmployeeBll.ChangePortrait(UserContext.CurrentUser.Id, portrait);
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
                EmployeeBll.PersonalEdit(UserContext.CurrentUser.Id, name, sex, tel);
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
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new Hotel.Model.Employee());
            var info = EmployeeBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, string searchName = null)
        {
            var pager = EmployeeBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Employee) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Employee model)
        {
            var apiResult = new APIResult();
            try
            {
                EmployeeBll.AddOrUpdate(model, (UserEntity)UserContext.CurrentUser);
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
                EmployeeBll.Delete(id);
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
 
        public ActionResult _Search()
        {
            return View();
        }

        public string GetList(string searchName)
        {
            var list = EmployeeBll.GetList(UserContext.CurrentUser.HotelId,searchName);
            return JsonConvert.SerializeObject(list);
        }
    }
}