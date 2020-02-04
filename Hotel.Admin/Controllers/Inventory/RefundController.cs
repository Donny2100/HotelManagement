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
    public class RefundController : Controller
    {
        // GET: Refund
        public ActionResult Index()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["supplier"] = SupplierBll.GetAllList(hotelId);
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
            ViewData["supplier"] = SupplierBll.GetAllList(hotelId);
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            if (id == 0)
                return View(new RefundArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);
            var info = new RefundArgs
            {
                Item1 = model,
                Item2 = model.StockOrderDetailsList,
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
            ViewData["supplier"] = SupplierBll.GetAllList(hotelId);
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            if (id == 0)
                return View(new RefundArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);
            var info = new RefundArgs
            {
                Item1 = model,
                Item2 = model.StockOrderDetailsList,
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
        public JsonResult Save(RefundArgs model)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            var apiResult = new APIResult();
            try
            {
                StockOrder stock = model.Item1;
                stock.DType = Model.Enum.DocumentTypeEnum.退货;
                stock.StockOrderDetailsList = model.Item2;

                StockOrderBll.AddOrUpdate(stock, UserContext.CurrentUser.Name, hotelId);

                ViewData["supplier"] = SupplierBll.GetAllList(hotelId);
                ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
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

        public string GetPager(int page, int rows, long supplierId = 0, long warehourseId = 0, string reciveSDate = "", string reciveEDate = "", int AuditStatus = -1, long HotelId = 0)
        {

            var pager = StockOrderBll.GetPager(page, rows, HotelId, supplierId, warehourseId, reciveSDate, reciveEDate, DocumentTypeEnum.退货, AuditStatus);//

            return JsonConvert.SerializeObject(pager);
        }

        public string GetById(long id)
        {

            StockOrder model = StockOrderBll.GetById(id);
            if (model != null)
            {
                var supply = SupplierBll.GetById(model.SupplierId);
                var warehourse = WarehouseBll.GetById(model.FromWarehourseId);

                if (supply != null)
                {
                    model.SupplierName = supply.Name;
                }
                if (warehourse != null)
                {
                    model.FromWarehourseName = warehourse.Name;
                }
            }

            return JsonConvert.SerializeObject(model);
        }

    }
    public class RefundArgs
    {
        public StockOrder Item1 { get; set; }
        public List<StockOrderDetails> Item2 { get; set; }
    }
}