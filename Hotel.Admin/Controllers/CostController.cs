using Hotel.Admin.App_Start;
using Hotel.Bll;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class CostController : AdminBaseController
    {
        // GET: Cost
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
                return View(new Hotel.Model.Cost() { HotelId = UserContext.CurrentUser.HotelId });
            var info = CostBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, long costCatId = 0, string searchName = null)
        {
            var pager = CostBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, costCatId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        public string GetList()
        {
            var pager = CostBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.Cost) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.Cost model)
        {
            var apiResult = new APIResult();
            try
            {
                CostBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                CostBll.Delete(id);
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