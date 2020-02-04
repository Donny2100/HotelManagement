using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
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
    public class BillSetController : AdminBaseController
    {
        // GET: BillSet
        public ActionResult Index()
        {
            var billSet = BillSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            if (billSet == null)
                billSet = new BillSet();
            ViewBag.BillSet = billSet;
            return View();
        }

        public JsonResult GetBillSet()
        {
            var model = BillSetBll.GetByHotelId(UserContext.CurrentUser.Id);
            return Json(model ?? new Model.BillSet() { HotelId = UserContext.CurrentUser.HotelId });
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(BillSet) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditBillSet(BillSet model)
        {
            var apiResult = new APIResult();
            try
            {
                BillSetBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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