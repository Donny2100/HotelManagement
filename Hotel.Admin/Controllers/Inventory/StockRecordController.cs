using Hotel.Admin.App_Start;
using Hotel.Bll.Inventory;
using Hotel.Model;
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
    public class StockRecordController : AdminBaseController
    {
        // GET: StockRecord
        public ActionResult Index()
        {
            long hotelId = UserContext.CurrentUser.HotelId;
            ViewData["warehourse"] = WarehouseBll.GetAllList(hotelId);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="selectTime"></param>
        /// <param name="commodityName"></param>
        /// <param name="warehouseId"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public string GetPager(int page, int rows, string selectTime, string commodityName, long warehouseId = 0, int orderType = 0)
        {
            var pager = StockOrderDetailsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, selectTime, warehouseId, commodityName, orderType);
            return JsonConvert.SerializeObject(pager);
        }
    }
}