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
    public class RoomRegTkController : AdminBaseController
    {
        // GET: RoomRegTk

        public ActionResult Index(long roomRegId, long itemId = 0)
        {
            if (itemId == 0)
            {
                return View(new RoomRegTk()
                {
                    RoomRegId = roomRegId,
                    FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                });
            }
            else
            {
                var model = RoomRegTkBll.SingleOrDefault(itemId);
                if (model == null)
                {
                    model = new RoomRegTk()
                    {
                        HotelId = UserContext.CurrentUser.HotelId,
                        RoomRegId = roomRegId,
                        FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                    };
                }
                return View(model);
            }
        }
        public ActionResult Index2(long roomRegId, long itemId = 0)
        {
            if (itemId == 0)
            {
                return View(new RoomRegTk()
                {
                    RoomRegId = roomRegId,
                    FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                });
            }
            else
            {
                var model = RoomRegTkBll.SingleOrDefault(itemId);
                if (model == null)
                {
                    model = new RoomRegTk()
                    {
                        HotelId = UserContext.CurrentUser.HotelId,
                        RoomRegId = roomRegId,
                        FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                    };
                }
                return View(model);
            }
        }


        public ActionResult _Remark(long id, string remark)
        {
            return View(new RoomRegTk() { Id = id, Remark = remark });
        }

        public ActionResult _KdRemark(long id, string kdRemark)
        {
            return View(new RoomRegTk() { Id = id, KdRemark = kdRemark });
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegTk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult AddOrUpdate(RoomRegTk model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegTkBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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
                RoomRegTkBll.DeleteById(id);
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
        /// 编辑备注
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(RoomRegTk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditRemark(long id, string remark)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegTkBll.EditRemark(id, remark);
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
        /// 编辑客单备注
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(RoomRegTk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditKdRemark(long id, string kdRemark)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegTkBll.EditKdRemark(id, kdRemark);
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