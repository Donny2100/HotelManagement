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
    public class TransferStockController : AdminBaseController
    {
        // GET: TransferStock
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
                return View(new TransferStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });


            StockOrder model = StockOrderBll.GetById(id);
            var info = new TransferStockArgs
            {
                Item1 = model,
                Item2 = model.StockOrderDetailsList.FindAll(it=>it.DType == DocumentTypeEnum.调拨入库),
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
                return View(new TransferStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });


            StockOrder model = StockOrderBll.GetById(id);
            var info = new TransferStockArgs
            {
                Item1 = model,
                Item2 = model.StockOrderDetailsList.FindAll(it => it.DType == DocumentTypeEnum.调拨入库),
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
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Inventory.StockOrder) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Save(TransferStockArgs model)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            var apiResult = new APIResult();
            try
            {
                if(model.Item1.FromWarehourseId == model.Item1.ToWarehourseId)
                {
                    apiResult.Ret = -1;
                    apiResult.Msg = "调出仓库和调入仓库不能是同一仓库！";
                    return Json(apiResult);
                }

                StockOrder stock = model.Item1;
                stock.DType = DocumentTypeEnum.调拨;
                stock.StockOrderDetailsList = model.Item2;
                StockOrderBll.AddOrUpdateTransferStock(stock, UserContext.CurrentUser.Name, hotelId);
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

        public string GetPager(int page, int rows, long fromWarehourseId = 0, long toWarehourseId = 0, string reciveSDate = "", string reciveEDate = "", int IsAduit = -1)
        {

            var pager = StockOrderBll.GetPagerByTrabsfer(page, rows, UserContext.CurrentUser.HotelId, fromWarehourseId, toWarehourseId, reciveSDate, reciveEDate, DocumentTypeEnum.调拨, IsAduit);

            return JsonConvert.SerializeObject(pager);
        }

        public string getById(long id)
        {

            StockOrder model = StockOrderBll.GetById(id);
            if (model != null)
            {
                var recallWarehourse = WarehouseBll.GetById(model.FromWarehourseId);
                var transferWarehourse = WarehouseBll.GetById(model.ToWarehourseId);

                model.FromWarehourseName = recallWarehourse == null ? "" : recallWarehourse.Name;
                model.ToWarehourseName = transferWarehourse == null ? "" : transferWarehourse.Name;
            }

            return JsonConvert.SerializeObject(model);
        }

        [HttpPost]
        public ActionResult Aduit(AuditArgs args)
        {
            var apiResult = new APIResult();
            try
            {
                StockOrderBll.UpdateAduitById(args.id, args.status);
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

    public class TransferStockArgs
    {
        public StockOrder Item1 { get; set; }
        public List<StockOrderDetails> Item2 { get; set; }
    }
}