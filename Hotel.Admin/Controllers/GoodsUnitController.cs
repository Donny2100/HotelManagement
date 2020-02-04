using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class GoodsUnitController : AdminBaseController
    {
        // GET: GoodsUnit
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
                return View(new GoodsUnit());
            var info = GoodsUnitBll.GetById(id);
            return View(info);
        }

        public string GetList()
        {
            var models = GoodsUnitBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(GoodsUnit) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(GoodsUnit model)
        {
            var apiResult = new APIResult();
            try
            {
                GoodsUnitBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                GoodsUnitBll.DeleteById(id);
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