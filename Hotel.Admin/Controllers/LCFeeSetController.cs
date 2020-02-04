using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
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
    /// <summary>
    /// 全局--凌晨房计费设置
    /// </summary>
    public class LCFeeSetController : AdminBaseController
    {
        // GET: LCFeeSet
        public ActionResult Index()
        {
            var info = GlobalFeeSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            if (info == null)
                info = new GlobalFeeSet() { HotelId =UserContext.CurrentUser.HotelId};
            return View(info);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(GlobalFeeSet) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(GlobalFeeSet model)
        {
            var apiResult = new APIResult();
            try
            {
                GlobalFeeSetBll.AddOrUpdateLC(model, UserContext.CurrentUser.HotelId);
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