using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
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
    public class OtherInfoController : AdminBaseController
    {
        // GET: OtherInfo
        #region 试图

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new OtherInfo() { HotelId= UserContext.CurrentUser.HotelId });
            var info = OtherInfoBll.GetById(id);
            return View(info);
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(OtherInfo) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(OtherInfo model)
        {
            var apiResult = new APIResult();
            try
            {
                OtherInfoBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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

        #endregion

        #region 接口

        [HttpPost]
        public string GetList()
        {
            var parentList = OtherInfoBll.GetList(0, UserContext.CurrentUser.HotelId);
            parentList.ForEach(m =>
            {
                var kids = OtherInfoBll.GetList(m.Id,UserContext.CurrentUser.HotelId);
                m.children = kids;
            });
            return JsonConvert.SerializeObject(parentList);
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
                OtherInfoBll.Delete(id);
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

        #endregion
    }
}