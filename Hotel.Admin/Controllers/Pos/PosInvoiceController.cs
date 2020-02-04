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
    public class PosInvoiceController : AdminBaseController
    {

        public ActionResult Index(long ConsumeId)
        {
            var consume = PosConsumeBll.GetById(ConsumeId);
            if (consume == null)
                consume = new Model.PosConsume();
            var model = new PosInvoice() { Id = 0, ConsumeId = consume.Id, DjdanNum = consume.OrderNo };
            ViewBag.Consume = consume;
            return View(model);
        }

        public string GetList(long ConsumeId)
        {
            var models = PosInvoiceBll.GetList(ConsumeId);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PosInvoice) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(PosInvoice model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosInvoiceBll.AddOrUpdate(model, user.HotelId, user.Id, user.UserName);
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
                PosInvoiceBll.Delete(id);
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