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
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.Inventory
{
    public class InStockController : AdminBaseController
    {
        // GET: InStock
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
                return View(new InStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);
            var info = new InStockArgs
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
                return View(new InStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);
            var info = new InStockArgs
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
        public JsonResult Save(InStockArgs model)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            var apiResult = new APIResult();
            try
            {
                StockOrder stock = model.Item1;
                stock.DType = Model.Enum.DocumentTypeEnum.入库;
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

            var pager = StockOrderBll.GetPager(page, rows, HotelId, supplierId, warehourseId, reciveSDate, reciveEDate, DocumentTypeEnum.入库, AuditStatus);//

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

        /// <summary>
        /// 审核/取消审核
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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


        public string GetInStockDetails(int page, int rows, long hotelId = 0, long warehouseId = 0, string sdate = "", string edate = "", string searcValue = "", string creator = "", int dtype = 0)
        {
            var pager = StockOrderDetailsBll.PagerInStockDetails(page, rows, hotelId, warehouseId, sdate, edate, searcValue, creator, dtype);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult Details()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehouse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }

        public ActionResult Stats()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehouse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }
        public string GetInStockStats(int page, int rows, long hotelId = 0, long warehouseId = 0, string sdate = "", string edate = "", string searcValue = "")
        {
            var pager = StockOrderDetailsBll.PagerInStockStats(page, rows, hotelId, warehouseId, sdate, edate, searcValue);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult ExportDetails(long hotelId = 0, long warehouseId = 0, string sdate = "", string edate = "", string commondityName = "", string creator = "", int dtype = 0)
        {
            var tb = new DataTable();
            tb.Columns.Add("单据号");
            tb.Columns.Add("单据类型");
            tb.Columns.Add("出库时间");
            tb.Columns.Add("仓库");
            tb.Columns.Add("货品名称");
            tb.Columns.Add("单位");
            tb.Columns.Add("单价");
            tb.Columns.Add("数量");
            tb.Columns.Add("合计金额");
            tb.Columns.Add("来源");
            tb.Columns.Add("操作人");
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;

            List<StockOrderDetails> list = StockOrderDetailsBll.ExportInstockDetails(hotelId, warehouseId, sdate, edate, commondityName, creator, dtype);
            foreach (StockOrderDetails item in list)
            {

                tb.Rows.Add(new string[] {
                                item.OrderNumber,
                                ((DocumentTypeEnum)item.DType).ToString(),
                                item.CreateTime,
                                item.WarehouseName,
                                item.CommondityName,
                                item.CommondityUnit,
                                item.UnitPrice.ToString(),
                                item.Quantity.ToString(),
                                item.TotalPrice.ToString(),
                                item.HotelName,
                                item.Creator
                });

            }

            ExcelHelper.ExportByWeb(tb, "入库明细报表", "入库明细报表.xls");
            return Json(apiResult);
        }

        public ActionResult ExportSummary(long hotelId = 0, long warehouseId = 0, string sdate = "", string edate = "", string searcValue = "")
        {
            var tb = new DataTable();
            tb.Columns.Add("仓库");
            tb.Columns.Add("货品名称");
            tb.Columns.Add("库存单位");
            tb.Columns.Add("总数量");
            tb.Columns.Add("总金额");
            tb.Columns.Add("来源");
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;

            List<StockOrderDetails> list = StockOrderDetailsBll.ExportInstockSummary(hotelId, warehouseId, sdate, edate, searcValue);
            foreach (StockOrderDetails item in list)
            {

                tb.Rows.Add(new string[] {
                                item.WarehouseName,
                                item.CommondityName,
                                item.CommondityUnit,
                                item.Quantity.ToString(),
                                item.TotalPrice.ToString(),
                                item.HotelName,

                });

            }

            ExcelHelper.ExportByWeb(tb, "入库明汇总表", "入库汇总报表.xls");
            return Json(apiResult);
        }

    }

    public class InStockArgs
    {
        public StockOrder Item1 { get; set; }
        public List<StockOrderDetails> Item2 { get; set; }
    }
    public class AuditArgs
    {
        public int id { get; set; }
        public int status { get; set; }
    }
}