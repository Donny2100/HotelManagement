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
    public class RoomSetController : AdminBaseController
    {
        // GET: RoomSet
        public ActionResult Index()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var model = RoomSetBll.GetBy(hotelId);
            if (model == null)
                model = new RoomSet() { HotelId = hotelId };
            
            return View(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomSet) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(RoomSet model)
        {
            var apiResult = new APIResult();
            try
            {
                RoomSetBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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