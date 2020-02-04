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
    public class PeriodRoomController : AdminBaseController
    {
        // GET: PeriodRoom
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var periodRoomBase = PeriodRoomBaseBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            if (periodRoomBase == null)
                periodRoomBase = new PeriodRoomBase();
            return View(periodRoomBase);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new PeriodRoom() { HotelId = UserContext.CurrentUser.HotelId });
            var info = PeriodRoomBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows)
        {
            var pager = PeriodRoomBll.GetPager(page, rows, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PeriodRoom) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(PeriodRoom model)
        {
            var apiResult = new APIResult();
            try
            {
                PeriodRoomBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                PeriodRoomBll.DeleteById(id);
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

        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PeriodRoomBase) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult PeriodRoomBaseSave(PeriodRoomBase model)
        {
            var apiResult = new APIResult();
            try
            {
                PeriodRoomBaseBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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

        public ActionResult _PeriodRoomSearch()
        {
            return View();
        }

        public string GetPeriodRoomList()
        {
            var list = PeriodRoomBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(list);
        }
    }
}