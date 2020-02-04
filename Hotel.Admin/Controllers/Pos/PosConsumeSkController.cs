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
    public class PosConsumeSkController : AdminBaseController
    {
        // GET: PosConsumeSk

        public ActionResult Index(long ConsumeId, long itemId = 0)
        {
            if (itemId == 0)
            {
                return View(new PosConsumeSk()
                {
                    HotelId = UserContext.CurrentUser.HotelId,
                    RoomRegId = 0,
                    ConsumeId = ConsumeId,
                    FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                });
            }
            else
            {
                var model = PosConsumeSkBll.GetById(itemId);
                if (model == null)
                {
                    model = new PosConsumeSk()
                    {
                        HotelId = UserContext.CurrentUser.HotelId,
                        RoomRegId = 0,
                        ConsumeId = ConsumeId,
                        FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                    };
                }
                return View(model);
            }
        }

        

        public ActionResult _Remark(long id, string remark)
        {
            return View(new PosConsumeSk() { Id = id, Remark = remark });
        }

        public ActionResult _KdRemark(long id, string kdRemark)
        {
            return View(new PosConsumeSk() { Id = id, KdRemark = kdRemark });
        }

        public ActionResult _Cffy(long id)
        {
            var model = PosConsumeSkBll.GetById(id);
            if (model == null)
                model = new PosConsumeSk() { Id = 0 };//数据不存在
            return View(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PosConsumeSk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult AddOrUpdate(PosConsumeSk model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeSkBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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
                PosConsumeSkBll.DeleteById(id);
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
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(PosConsumeSk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditRemark(long id, string remark)
        {
            var apiResult = new APIResult();
            try
            {
                PosConsumeSkBll.EditRemark(id, remark);
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
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(PosConsumeSk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditKdRemark(long id, string kdRemark)
        {
            var apiResult = new APIResult();
            try
            {
                PosConsumeSkBll.EditKdRemark(id, kdRemark);
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
        /// 拆分费用
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(RoomRegTk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditCffy(long id, decimal money1, decimal money2)
        {
            var user = UserContext.CurrentUser;
            var apiResult = new APIResult();
            try
            {
                PosConsumeSkBll.EditCffy(id, money1, money2, user.Id, user.Name);
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