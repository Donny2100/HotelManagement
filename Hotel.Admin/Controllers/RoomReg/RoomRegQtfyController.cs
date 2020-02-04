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
    public class RoomRegQtfyController : AdminBaseController
    {
        // GET: RoomRegQtfy
        public ActionResult Index(long roomRegId, long itemId =0)
        {
            if (itemId == 0)
                return View(new RoomRegQtfy() { RoomRegId = roomRegId, FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
            else
            {
                var model = RoomRegQtfyBll.GetById(itemId);
                if (model == null)
                    model = new RoomRegQtfy();
                return View(model);
            }
        }
        public ActionResult Index2(long roomRegId = 0)
        {
            var RoomNO = "";
            if (roomRegId != 0)
            {
                var room = RoomRegBll.GetById(roomRegId);
                RoomNO = room.RoomNO;
            }

            var model = new RoomRegQtfy() {
                RoomRegId = roomRegId,
                RoomNO = RoomNO,
                FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };
            return View(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegQtfy) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult AddOrUpdate(RoomRegQtfy model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegQtfyBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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
        public JsonResult Del(long id)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegQtfyBll.DeleteById(id);
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