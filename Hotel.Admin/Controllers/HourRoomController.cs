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
    public class HourRoomController : AdminBaseController
    {
        // GET: HourRoom
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var hourRoomBase = HourRoomBaseBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            if (hourRoomBase == null)
                hourRoomBase = new HourRoomBase();
            return View(hourRoomBase);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new HourRoom() { HotelId = UserContext.CurrentUser.HotelId });
            var info = HourRoomBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows)
        {
            var pager = HourRoomBll.GetPager(page, rows, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(HourRoom) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(HourRoom model)
        {
            var apiResult = new APIResult();
            try
            {
                HourRoomBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                HourRoomBll.DeleteById(id);
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
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(HourRoomBase) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult HourRoomBaseSave(HourRoomBase model)
        {
            var apiResult = new APIResult();
            try
            {
                HourRoomBaseBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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

        public ActionResult _HourRoomSearch()
        {
            return View();
        }

        public string GetHourRoomList() {
            var list = HourRoomBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(list);
        }
    }
}