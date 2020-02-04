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

namespace Hotel.Admin.Controllers.RoomReg
{
    public class RoomRegXykController : AdminBaseController
    {
        // GET: RoomRegXyk

        /// <summary>
        ///信用卡--借记卡（包含借记卡，所以卡类型要有信用卡的和借记卡的）
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(long roomRegId)
        {
            ViewBag.RoomRegId = roomRegId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetList(long roomRegId)
        {
            var models = RoomRegXykBll.GetListByRoomRegId(roomRegId);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegXyk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(RoomRegXyk model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegXykBll.AddOrUpdate(model, user.HotelId, user.Id, user.UserName);
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
                RoomRegXykBll.DeleteById(id);
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
        /// 撤销
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult YsqCancel(long id)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegXykBll.YsqCancel(id);
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