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
    public class SupplierController : AdminBaseController
    {
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        public string GetPager(int page, int rows, string supplierName, string supplierStatus, long supplierType=0)
        {
            var pager = SupplierBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, supplierName, supplierStatus, supplierType);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            List<SupplierType> supTypeList= SupplierTypeBll.GetList(UserContext.CurrentUser.HotelId);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in supTypeList)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewData["hotelTypeList"] = items;
            if (id == 0)
                return View(new Hotel.Model.Inventory.Supplier() { HotelId = UserContext.CurrentUser.HotelId });
            var info = SupplierBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Inventory.Supplier) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.Inventory.Supplier model)
        {
            var apiResult = new APIResult();
            try
            {
                SupplierBll.AddOrUpdate(model, UserContext.CurrentUser.Name, UserContext.CurrentUser.HotelId);
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
            List<SupplierType> supTypeList = SupplierTypeBll.GetList(UserContext.CurrentUser.HotelId);
            TreeNode rootNode = new TreeNode()
            {
                id = 0,
                text = "全部分类",
                state = "open",
                children = findTreeChildren(supTypeList, 0)
            };

            var result = JsonConvert.SerializeObject(rootNode);

            return $"[{result}]";
        }

        private List<TreeNode> findTreeChildren(List<SupplierType> typeList, long parentId)
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
        public JsonResult EditSupplierType(FormCollection form)
        {
            var apiResult = new APIResult();
            try
            {
                var opt = form["opt"];
                SupplierType model = new SupplierType();
                model.Name = form["name"];
                if (opt == "0")         
                {
                    model.Pid = Convert.ToInt64(form["id"]);
                }
                else
                {
                    model.Id = Convert.ToInt64(form["id"]);
                }
                
                SupplierTypeBll.AddOrUpdate(model, UserContext.CurrentUser.Name, UserContext.CurrentUser.HotelId);
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
        /// 删除供应商
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult Delete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                SupplierBll.Delete(id);
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
        /// 删除供应商类型
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult DeleteSupplierType(long id)
        {
            var apiResult = new APIResult();
            try
            {
                SupplierTypeBll.Delete(id);
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