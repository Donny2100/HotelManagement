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
    public class RoomPriceController : AdminBaseController
    {
        // GET: RoomPrice
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
                return View(new RoomPrice() { HotelId = UserContext.CurrentUser.HotelId });
            var info = RoomPriceBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, long roomTypeId = 0)
        {
            var pager = RoomPriceBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, roomTypeId);
            return JsonConvert.SerializeObject(pager);
        }

        public string GetList(long roomTypeId = 0)
        {
            var models = RoomPriceBll.GetList(UserContext.CurrentUser.HotelId, roomTypeId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetAllList()
        {
            var models = RoomPriceBll.GetAllList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetListForReg(long roomTypeId, string tip)
        {
            var models = RoomPriceBll.GetList(UserContext.CurrentUser.HotelId, roomTypeId);
            if (models == null)
                models = new List<RoomPrice>();
            models.Insert(0, new RoomPrice() { Id = 0, SaleName = tip });
            return JsonConvert.SerializeObject(models);
        }

        public string GetSingle(long id)
        {
            var model = RoomPriceBll.GetById(id);
            return JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(long id, string saleName, decimal sale, long roomTypeId, DateTime startDate, DateTime endDate, string remark)
        {
            var apiResult = new APIResult();
            try
            {
                var model = new RoomPrice()
                {
                    Id = id,
                    SaleName = saleName,
                    Sale = sale,
                    RoomTypeId = roomTypeId,
                    StartDate = TypeConvert.DateTimeToInt(startDate),
                    EndDate = TypeConvert.DateTimeToInt(endDate),
                    Remark = remark,
                };
                RoomPriceBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                RoomPriceBll.DeleteById(id);
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