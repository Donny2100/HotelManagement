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

namespace Hotel.Admin.Controllers.RoomReg
{
    public class RoomRegYhController : AdminBaseController
    {
        // GET: RoomRegYh
        public ActionResult Index(long roomRegId, long itemId = 0)
        {
            //获取前台优惠限额
            var hotelId = UserContext.CurrentUser.HotelId;
            var param = ParamBll.SingleOrDefault($"where hotelId={hotelId}");
            if (param == null)
            {
                ViewBag.QtYhMaxMoney = 0;
            }
            else
            {
                ViewBag.QtYhMaxMoney = param.QtYhMaxMoney;
            }

            if (itemId == 0)
            {
                //新增
                return View(new RoomRegYh() { HotelId = hotelId, RoomRegId = roomRegId });

            }
            else
            {
                //修改
                var model = RoomRegYhBll.GetById(itemId);
                if (model == null)
                    model = new RoomRegYh();
                return View(model);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegYh) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(RoomRegYh model)
        {
            var apiResult = new APIResult();
            try
            {
                var user = UserContext.CurrentUser;
                RoomRegYhBll.AddOrUpdate(model, user.HotelId, user.Id, user.Name);
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
                RoomRegYhBll.DeleteById(id);
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