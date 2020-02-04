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
    public class GuestGoodsController : AdminBaseController
    {
        // GET: GuestGoods
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new Hotel.Model.GuestGoods());
            var info = GuestGoodsBll.GetById(id);
            return View(info);
        }


        public string GetPager(int page, int rows, string goodType, string selectTime, string goodName, string guestName, string roomNo, string startTime, string endTime)
        {
            var pager = GuestGoodsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, goodType, selectTime, goodName, guestName, roomNo, startTime, endTime);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.GuestGoods) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.GuestGoods model)
        {
            var apiResult = new APIResult();
            try
            {
                GuestGoodsBll.AddOrUpdate(model, UserContext.CurrentUser.UserName, UserContext.CurrentUser.HotelId);
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
                GuestGoodsBll.Delete(id);
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
        public ActionResult Receive(FormCollection form)
        {
            var apiResult = new APIResult();
            try
            {
                long id= Convert.ToInt64(form["id"]);
                string receiveUser = form["receiveUser"];
                GuestGoodsBll.ReceiveById(id, receiveUser);
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

        public string GetById(long id)
        {
            GuestGoods model = GuestGoodsBll.GetById(id);
            return JsonConvert.SerializeObject(model);
        }
    }
}