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
    public class MemberTypeSaleController : AdminBaseController
    {
        // GET: MemberTypeSale
        public ActionResult Index()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;

            var memberTypeList = MemberTypeBll.GetList(hotelId);
            if (memberTypeList != null && memberTypeList.Count > 0)
            {
                ViewBag.MemberTypeId = memberTypeList[0].Id;
            }
            else
            {
                ViewBag.MemberTypeId = 0;
            }

            return View();
        }

        public string GetList(long memberTypeId, int saleType)
        {
            var models = MemberTypeSaleBll.GetList(memberTypeId, UserContext.CurrentUser.HotelId, saleType);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(MemberTypeSale) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(MemberTypeSale model)
        {
            var apiResult = new APIResult();
            try
            {
                MemberTypeSaleBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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