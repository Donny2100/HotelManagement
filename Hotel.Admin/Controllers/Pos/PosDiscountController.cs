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
    public class PosDiscountController : AdminBaseController
    {
        public ActionResult Index(long posId)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.PosId = posId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long posId, long id = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.PosId = posId;
            if (id == 0)
                return View(new PosDiscount() { PosId = posId });
            var info = PosDiscountBll.GetById(id);
            if (info.PosId == 0) info.PosId = posId;
            return View(info);
        }
 

        public string GetList(long posId)
        {
            var models = PosDiscountBll.GetListByPos(posId, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(models);
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PosDiscount) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(PosDiscount model)
        {
            var apiResult = new APIResult();
            try
            {
                PosDiscountBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                PosDiscountBll.DeleteById(id);
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