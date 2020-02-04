using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class HolidaySetController : AdminBaseController
    {
        // GET: HolidaySet
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var weekendSet = WeekendSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            if (weekendSet == null)
                weekendSet = new WeekendSet();
            return View(weekendSet);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new HolidaySet() { HotelId = UserContext.CurrentUser.HotelId });
            var info = HolidaySetBll.GetById(id);
            return View(info);
        }

        public string GetPager()
        {
            var pager = HolidaySetBll.GetList( UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(string name ,DateTime? startDate,DateTime? endDate)
        {
            var apiResult = new APIResult();
            try
            {
                var model = new HolidaySet() {
                    Name =name,
                    StartDate =startDate ==null?0:TypeConvert.DateTimeToInt(DateTime.Parse((startDate.Value.ToShortDateString()))),
                    EndDate =endDate ==null?0:TypeConvert.DateTimeToInt(DateTime.Parse((endDate.Value.ToShortDateString()+" 23:59:59")))
                };
                HolidaySetBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                HolidaySetBll.DeleteById(id);
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

        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(WeekendSet) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult WeekendSetSave(WeekendSet model)
        {
            var apiResult = new APIResult();
            try
            {
                WeekendSetBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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