using Hotel.Bll.Inventory;
using Hotel.Model.Inventory;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.Inventory
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(hotelId);
            List<SelectListItem> typeItems = new List<SelectListItem>();

            foreach (var item in comTypeList)
            {
                typeItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewData["commodityTypeList"] = typeItems;
            ViewData["warehouse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }

        public string GetInStockStats(int page, int rows, long warehouseId = 0, long commondityTypeId = 0, string commondityName = "")
        {
            var pager = StockBll.PagerStock(page, rows, warehouseId, commondityTypeId, commondityName);
            return JsonConvert.SerializeObject(pager);
        }

        public string GetPager(int page, int rows, long warehouseId = 0, long commodityTypeId = 0, string commodityName = "")
        {
            var pager = StockBll.PagerStock(page, rows, warehouseId, commodityTypeId, commodityName);
            return JsonConvert.SerializeObject(pager);
        }
        public ActionResult ExportStock(long warehouseId = 0, long commodityTypeId = 0, string commodityName = "")
        {
            var tb = new DataTable();
            tb.Columns.Add("货品名称");
            tb.Columns.Add("货品类别");
            tb.Columns.Add("仓库");
            tb.Columns.Add("库存单位");
            tb.Columns.Add("最低库存");
            tb.Columns.Add("最高库存");
            tb.Columns.Add("实际库存");
            tb.Columns.Add("预警状态");

            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;

            List<Stock> list = StockBll.ExportStock(warehouseId, commodityTypeId, commodityName);
            foreach (Stock item in list)
            {

                tb.Rows.Add(new string[] {
                      item.CommodityName,
                      item.CommodityTypeName,
                      item.WarehourseName,
                      item.CommodityUnitName,
                      item.LowStock.ToString(),
                      item.HighStock.ToString(),
                      item.TotalCount.ToString(),
                      (item.LowStock<= item.TotalCount || item.TotalCount<= item.HighStock)?"正常":"预警",
                });

            }

            ExcelHelper.ExportByWeb(tb, "库存报表", "库存报表.xls");
            return Json(apiResult);
        }

    }
}