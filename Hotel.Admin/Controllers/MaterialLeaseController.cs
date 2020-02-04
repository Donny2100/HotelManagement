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
    public class MaterialLeaseController : AdminBaseController
    {
        // GET: MaterialLease
        public ActionResult Index(long roomRegId)
        {
            ViewBag.RoomRegId = roomRegId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetList(long roomRegId, int state =-1)
        {
            var models = MaterialLeaseBll.GetList(roomRegId, state);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(MaterialLease) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(MaterialLease model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MaterialLeaseBll.AddOrUpdate(model, user.HotelId, user.Id, user.UserName);
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
                MaterialLeaseBll.Delete(id);
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
        /// 归还
        /// </summary>
        [OprtLogFilter(IsRecordLog = false, Method = "归还", IsFormPost = false, LogWay = OprtLogType.未知, IsFromCache = true)]
        public ActionResult Back(List<MaterialLease> models)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MaterialLeaseBll.Back(models, user.HotelId, user.Id, user.UserName);
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