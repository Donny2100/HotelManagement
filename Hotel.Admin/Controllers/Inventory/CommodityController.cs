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
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.Inventory
{
    public class CommodityController : AdminBaseController
    {
        // GET: Commodity
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="commodityName"></param>
        /// <param name="commodityStatus"></param>
        /// <param name="commodityType"></param>
        /// <returns></returns>
        public string GetPager(int page, int rows, string commodityName, string commodityStatus, long commodityType = 0)
        {
            var pager = CommodityBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, commodityName, commodityStatus, commodityType);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 获取某个库存商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="WarehourseId"></param>
        /// <param name="commodityType"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public string GetStockPager(int page, int rows, long warehourseId, long commodityType = 0, string searchValue = "")
        {
            var pager = CommodityBll.GetStockPager(page, rows, UserContext.CurrentUser.HotelId, warehourseId, commodityType, searchValue);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(UserContext.CurrentUser.HotelId);
            List<SelectListItem> typeItems = new List<SelectListItem>();

            foreach (var item in comTypeList)
            {
                typeItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewData["commodityTypeList"] = typeItems;

            List<CommodityUnit> comUnitList = CommodityUnitBll.GetList(UserContext.CurrentUser.HotelId);
            List<SelectListItem> unitItems = new List<SelectListItem>();
            foreach (var item in comUnitList)
            {
                unitItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewData["commodityUnitList"] = unitItems;

            if (id == 0)
                return View(new Hotel.Model.Inventory.Commodity() { HotelId = UserContext.CurrentUser.HotelId });

            var info = CommodityBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Inventory.Commodity) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.Inventory.Commodity model)
        {
            var apiResult = new APIResult();
            try
            {

                if (model.CostStandardPrice > model.HighLimitPrice)
                {
                    apiResult.Ret = -1;
                    apiResult.Msg = "采购最高限价不能低于成本卡标准价！";
                    return Json(apiResult);
                }
                //最高库存最低库存判断
                if (model.HighStock < model.LowStock)
                {
                    apiResult.Ret = -1;
                    apiResult.Msg = "最高库存不能低于最低库存！";
                    return Json(apiResult);
                }

                CommodityBll.AddOrUpdate(model, UserContext.CurrentUser.Name, UserContext.CurrentUser.HotelId);
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
        /// 构建tree树
        /// </summary>
        /// <returns></returns>
        public string GetTreeView()
        {
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(UserContext.CurrentUser.HotelId);
            TreeNode rootNode = new TreeNode()
            {
                id = 0,
                text = "全部分类",
                state = "open",
                children = findTreeChildren(comTypeList, 0)
            };

            var result = JsonConvert.SerializeObject(rootNode);

            return $"[{result}]";
        }

        /// <summary>
        /// 查询所有子节点
        /// </summary>
        /// <param name="typeList"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<TreeNode> findTreeChildren(List<CommodityType> typeList, long parentId)
        {
            List<TreeNode> childrenTreeList = new List<TreeNode>();
            var nodeList = typeList.FindAll(it => it.Pid == parentId);
            //根据父级Id查询子节点集合，没有则返回空集合
            if (nodeList == null || nodeList.Count == 0)
                return childrenTreeList;

            foreach (var item in nodeList)
            {
                TreeNode node = new TreeNode()
                {
                    id = item.Id,
                    text = item.Name,
                    state = "open",
                    children = findTreeChildren(typeList, item.Id)
                };
                childrenTreeList.Add(node);
            }
            return childrenTreeList;
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "添加/修改", IsFormPost = true, LogWay = OprtLogType.新增和修改, IsFromCache = true)]
        public JsonResult EditCommodityType(FormCollection form)
        {
            var apiResult = new APIResult();
            try
            {
                var opt = form["opt"];
                CommodityType model = new CommodityType();
                model.Name = form["name"];
                if (opt == "0")
                {
                    model.Pid = Convert.ToInt64(form["id"]);
                }
                else
                {
                    model.Id = Convert.ToInt64(form["id"]);
                }

                CommodityTypeBll.AddOrUpdate(model, UserContext.CurrentUser.Name, UserContext.CurrentUser.HotelId);
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
        /// 删除货品
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult Delete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                CommodityBll.Delete(id);
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

        // <summary>
        /// 删除货品类型
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult DeleteCommodityType(long id)
        {
            var apiResult = new APIResult();
            try
            {
                CommodityTypeBll.Delete(id);
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

        public ActionResult _InStockSearch()
        {
            List<SelectListItem> typeItems = new List<SelectListItem>();
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(UserContext.CurrentUser.HotelId);
            foreach (var item in comTypeList)
            {
                typeItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewData["commodityTypeList"] = typeItems;
            return View();
        }

        public ActionResult _RefundSearch()
        {
            List<SelectListItem> typeItems = new List<SelectListItem>();
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(UserContext.CurrentUser.HotelId);
            foreach (var item in comTypeList)
            {
                typeItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewData["commodityTypeList"] = typeItems;
            return View();
        }

        /// <summary>
        /// 出库单商品列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult _OutStockSearch(long warehourseid)
        {
            List<SelectListItem> typeItems = new List<SelectListItem>();
            List<CommodityType> comTypeList = CommodityTypeBll.GetList(UserContext.CurrentUser.HotelId);
            foreach (var item in comTypeList)
            {
                typeItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            Warehouse house = WarehouseBll.GetById(warehourseid);
            if (house == null)
            {
                warehourseid = 0;
            }
            ViewData["commodityTypeList"] = typeItems;
            ViewData["warehourseid"] = warehourseid;
            return View();
        }

        /// <summary>
        /// 出库单商品列表页面商品分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="commodityType"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public string GetOutStockPager(int page, int rows, long commodityType = 0,long warehouseid=0, string searchValue = "")
        {
            var pager = CommodityBll.GetOutStockPager(page, rows, UserContext.CurrentUser.HotelId, warehouseid, commodityType, searchValue);
            return JsonConvert.SerializeObject(pager);
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


    }
}