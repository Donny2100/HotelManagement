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
    public class RoomController : AdminBaseController
    {
        // GET: Room
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new Hotel.Model.Room() { HotelId = UserContext.CurrentUser.HotelId });
            var info = RoomBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        [HttpGet]
        public ActionResult AddList()
        {
            return View(new Hotel.Model.Room() { HotelId = UserContext.CurrentUser.HotelId });
        }

        public JsonResult FunAddList(long buildId, long floorId, string roomNoPre, int roomNOFrom, int roomNOTo, string roomNOLast, long roomTypeId)
        {
            var apiResult = new APIResult();
            if (string.IsNullOrWhiteSpace(roomNoPre))
                roomNoPre = string.Empty;
            if (roomNOFrom <= 0)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "【房号从】必须从正整数开始";
                return Json(apiResult);
            }
            if (roomNOFrom > roomNOTo)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "【房号至】必须大于【房号从】";
                return Json(apiResult);
            }
            List<int> lastNum = new List<int>();
            if (!string.IsNullOrWhiteSpace(roomNOLast))
            {
                string[] arr = roomNOLast.Replace("，", ",").Split(',');
                if (arr.Length > 0)
                {
                    foreach (var str in arr)
                    {
                        int last = 0;
                        if (int.TryParse(str, out last))
                            lastNum.Add(last);
                    }
                }
            }
            List<Room> models = new List<Room>();
            for (var i = roomNOFrom; i <= roomNOTo; i++)
            {
                if (lastNum.Any(m => i.ToString().EndsWith(m.ToString())))
                    continue;
                var model = new Room();
                model.BuildId = buildId;
                model.FloorId = floorId;
                model.RoomNO = roomNoPre + i.ToString().PadLeft(roomNOTo.ToString().Length, '0');
                model.RoomTypeId = roomTypeId;
                model.HotelId = UserContext.CurrentUser.HotelId;
                models.Add(model);
            }
            if (models.Count == 0)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "无可添加的房号，请确认";
                return Json(apiResult);
            }
            try
            {
                foreach (var model in models)
                {
                    RoomBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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

        public string GetPager(int page, int rows, long floorId = 0, string searchName = null)
        {
            var pager = RoomBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, floorId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Room) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.Room model)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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

        public ActionResult DeleteByIds(long[] Ids)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.Deletes(Ids);
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
                RoomBll.Delete(id);
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