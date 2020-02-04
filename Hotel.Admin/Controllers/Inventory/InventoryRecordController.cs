using Hotel.Admin.App_Start;
using Hotel.Bll.Inventory;
using Hotel.Model.Enum;
using Hotel.Model.Inventory;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.Inventory
{
    public class InventoryRecordController : AdminBaseController
    {
        // GET: InventoryRecord
        public ActionResult Index()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            if (id == 0)
                return View(new InventoryRecordArgs() { Item1 = new InventoryRecord(), Item2 = new List<InventoryRecordDetails>() });


            InventoryRecord model = InventoryRecordBll.GetById(id);
            var info = new InventoryRecordArgs
            {
                Item1 = model,
                Item2 = model.InventoryRecordDetailsList
            };

            return View(info);
        }

        /// <summary>
        /// 预览
        /// </summary>
        [HttpGet]
        public ActionResult Preview(long id = 0)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            if (id == 0)
                return View(new InventoryRecordArgs() { Item1 = new InventoryRecord(), Item2 = new List<InventoryRecordDetails>() });


            InventoryRecord model = InventoryRecordBll.GetById(id);
            var info = new InventoryRecordArgs
            {
                Item1 = model,
                Item2 = model.InventoryRecordDetailsList
            };

            return View(info);
        }

        /// <summary>
        /// 添加/更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Inventory.InventoryRecord) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Save(InventoryRecordArgs model)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            var apiResult = new APIResult();
            try
            {
                InventoryRecord inventoryRecord = model.Item1;
                inventoryRecord.InventoryRecordDetailsList = model.Item2;
                InventoryRecordBll.AddOrUpdate(inventoryRecord, UserContext.CurrentUser.Name, hotelId);
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

        public string GetPager(int page, int rows, long warehourseId = 0, string reciveSDate = "", string reciveEDate = "", int IsAduit = -1)
        {

            var pager = InventoryRecordBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, warehourseId, reciveSDate, reciveEDate, IsAduit);

            return JsonConvert.SerializeObject(pager);
        }

        public string getById(long id)
        {

            InventoryRecord model = InventoryRecordBll.GetById(id);
            if (model != null)
            {
                var warehourse = WarehouseBll.GetById(model.WarehourseId);

                model.WarehourseName = warehourse == null ? "" : warehourse.Name;
            }

            return JsonConvert.SerializeObject(model);
        }

        [HttpPost]
        public ActionResult Aduit(AuditArgs args)
        {
            var apiResult = new APIResult();
            try
            {
                InventoryRecordBll.UpdateAduitById(args.id, args.status);
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

    public class InventoryRecordArgs
    {
        public InventoryRecord Item1 { get; set; }
        public List<InventoryRecordDetails> Item2 { get; set; }
    }
}