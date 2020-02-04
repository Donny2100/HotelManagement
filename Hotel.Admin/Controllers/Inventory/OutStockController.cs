using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Bll.Inventory;
using Hotel.Model.Enum;
using Hotel.Model.Inventory;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.Inventory
{
    /// <summary>
    /// 出库
    /// </summary>
    public class OutStockController : AdminBaseController
    {
        // GET: OutStock
        public ActionResult Index()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["supplier"] = SupplierBll.GetAllList(hotelId);
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            ViewData["hotelList"] = HotelBll.GetAllList(hotelId);
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
                return View(new OutStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);
            var info = new OutStockArgs
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
                return View(new OutStockArgs() { Item1 = new StockOrder(), Item2 = new List<StockOrderDetails>() });

            StockOrder model = StockOrderBll.GetById(id);

            Warehouse house = WarehouseBll.GetById(model.FromWarehourseId);
            string WarehouseName = house?.Name;

            //给详情附上仓库名字
            List<StockOrderDetails> list = model.StockOrderDetailsList;
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].WarehouseName = WarehouseName;
                }
            }

            var info = new OutStockArgs
            {
                Item1 = model,
                Item2 = list,
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
        public JsonResult Save(OutStockArgs model)
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            var apiResult = new APIResult();
            try
            {
                StockOrder stock = model.Item1;
                stock.DType = Model.Enum.DocumentTypeEnum.销售出库;
                stock.StockOrderDetailsList = model.Item2;
                //stock.FromWarehourseId = OutStockWarehouseID;//这边系统会给定
                StockOrderBll.AddOrUpdateOutStack(stock, UserContext.CurrentUser.Name, hotelId);

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

        /// <summary>
        /// 出库单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="warehourseId"></param>
        /// <param name="reciveSDate"></param>
        /// <param name="reciveEDate"></param>
        /// <param name="AuditStatus"></param>
        /// <param name="HotelId"></param>
        /// <returns></returns>

        public string GetPager(int page, int rows, long warehourseId = 0, string reciveSDate = "", string reciveEDate = "", int AuditStatus = -1, long HotelId = 0)
        {

            var pager = StockOrderBll.GetPager(page, rows, HotelId, 0, warehourseId, reciveSDate, reciveEDate, DocumentTypeEnum.销售出库, AuditStatus);//

            return JsonConvert.SerializeObject(pager);
        }




        public string GetById(long id)
        {

            StockOrder model = StockOrderBll.GetById(id);
            if (model != null)
            {
                // var supply = SupplierBll.GetById(model.SupplierId);
                var warehourse = WarehouseBll.GetById(model.FromWarehourseId);

                //if (supply != null)
                //{
                //    model.SupplierName = supply.Name;
                //}
                if (warehourse != null)
                {
                    model.FromWarehourseName = warehourse.Name;
                }
            }

            return JsonConvert.SerializeObject(model);
        }

        [HttpPost]
        public ActionResult Aduit(long id)
        {
            var apiResult = new APIResult();
            try
            {
                StockOrderBll.UpdateAduitById(id);
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



        ///// <summary>
        ///// 出库从某个仓库出，还不知道，现在默认放在这里
        ///// </summary>
        //public long OutStockWarehouseID
        //{

        //    get
        //    {
        //        return ConfigurationManager.AppSettings["OutStockWarehouseID"] == null ?
        //              0 : Convert.ToInt64(ConfigurationManager.AppSettings["OutStockWarehouseID"].ToString());
        //    }
        //}


        /// <summary>
        /// 出库明细
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// 出库汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehouse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }

        /// <summary>
        /// 出库明细数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="reciveSDate"></param>
        /// <param name="reciveEDate"></param>
        /// <param name="creator">操作者</param>
        /// <param name="hotelId"></param>
        /// <param name="dType"></param>
        /// <param name="CommodityName"></param>
        /// <returns></returns>
        public string GetOutStockDetailsPager(int page, int rows, string reciveSDate = "", string reciveEDate = "", string creator = "", long hotelId = 0, int dType = 0, string CommodityName = "")
        {

            var pager = StockOrderDetailsBll.GetOutStockDetailsPager(page, rows, CommodityName, reciveSDate, reciveEDate, creator, hotelId, dType, CommodityName);//

            return JsonConvert.SerializeObject(pager);
        }
        /// <summary>
        /// index导出excel
        /// </summary>
        /// <param name="GuestType"></param>
        /// <returns></returns>
        public JsonResult ToExcel(string reciveSDate = "", string reciveEDate = "", string creator = "", long hotelId = 0, int dType = 0, string CommodityName = "")
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

            List<StockOrderDetails> list = StockOrderDetailsBll.GetOutStockDetailsPagerList(CommodityName, reciveSDate, reciveEDate, creator, hotelId, dType, CommodityName);//
            foreach (StockOrderDetails item in list)
            {

                tb.Rows.Add(new string[] {
                                item.OrderNumber,
                                ((DocumentTypeEnum)item.DType).ToString(),
                                item.stockOrder.OperateTime,
                                item.stockOrder.FromWarehourseName,
                                item.CommondityName,
                                item.CommondityUnit,
                                item.UnitPrice.ToString(),
                                item.Quantity.ToString(),
                                item.TotalPrice.ToString(),
                                item.stockOrder.HotelName,
                                item.stockOrder.Creator
                });

            }

            ExcelHelper.ExportByWeb(tb, "出库明细报表", "出库明细报表.xls");
            return Json(apiResult);
        }

        /// <summary>
        /// 出库汇总
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="hotelId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="reciveSDate"></param>
        /// <param name="reciveEDate"></param>
        /// <param name="CommodityName"></param>
        /// <returns></returns>
        public string GetOutStockSummary(int page, int rows, long hotelId = 0, long warehouseId = 0, string reciveSDate = "", string reciveEDate = "", string CommodityName = "")
        {
            var pager = StockOrderDetailsBll.PagerOutStockSummary(page, rows, hotelId, warehouseId, reciveSDate, reciveEDate, CommodityName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// Summary导出excel
        /// </summary>
        /// <param name="GuestType"></param>
        /// <returns></returns>
        public JsonResult ToSummaryExcel(long hotelId = 0, long warehouseId = 0, string reciveSDate = "", string reciveEDate = "", string CommodityName = "")
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

            List<StockOrderDetails> list = StockOrderDetailsBll.ExportOutstockSummary(hotelId, warehouseId, reciveSDate, reciveEDate, CommodityName);
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

            ExcelHelper.ExportByWeb(tb, "出库汇总报表", "出库汇总报表.xls");
            return Json(apiResult);
        }


    }


    public class OutStockArgs
    {
        public StockOrder Item1 { get; set; }
        public List<StockOrderDetails> Item2 { get; set; }
    }
}