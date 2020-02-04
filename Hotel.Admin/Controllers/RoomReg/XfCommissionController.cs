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
    public class XfCommissionController : AdminBaseController
    {
        // GET: XfCommission
        public ActionResult Index(long roomRegId)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                roomReg = new Model.RoomReg();
            var model = new XfCommission() { Id = 0, RoomRegId = roomReg.Id, DjdanNum = roomReg.DanJuNum, RoomNO = roomReg.RoomNO };
            ViewBag.RoomRegId = roomRegId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId; ;
            return View(model);
        }

        public string GetList(long roomRegId)
        {
            var models = XfCommissionBll.GetList(roomRegId);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(XfCommission) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(XfCommission model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                XfCommissionBll.AddOrUpdate(model, user.HotelId, user.Id, user.UserName);
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
                XfCommissionBll.Delete(id);
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