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
    public class RoomRegGuestInfoController : AdminBaseController
    {
        // GET: RoomRegGuestInfo
        public ActionResult Index(long roomRegId)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                roomReg = new Model.RoomReg() { Id = roomRegId };
            ViewBag.RoomReg = roomReg;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public ActionResult IndexYd(long roomRegId)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                roomReg = new Model.RoomReg() { Id = roomRegId };
            ViewBag.RoomReg = roomReg;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetList(long roomRegId, string type)
        {
            if (type == "cn")
            {
                var datas = RoomRegGuestInfoCNBll.GetList(roomRegId);
                return JsonConvert.SerializeObject(datas);
            }
            else
            {
                var datas = RoomRegGuestInfoENBll.GetList(roomRegId);
                return JsonConvert.SerializeObject(datas);
            }
        }

        /// <summary>
        /// 保存--境内
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegGuestInfoCN) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditCN(RoomRegGuestInfoCN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                RoomRegGuestInfoCNBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId,ref code);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if (code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }

        /// <summary>
        /// 保存--境外
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegGuestInfoEN) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditEN(RoomRegGuestInfoEN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                RoomRegGuestInfoENBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId,ref code);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if(code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult Delete(long id, string type)
        {
            var apiResult = new APIResult();
            try
            {
                if (type == "cn")
                    RoomRegGuestInfoCNBll.DeleteById(id);
                else
                    RoomRegGuestInfoENBll.DeleteById(id);

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

        public string Search(int page, int rows,string key, string value, string type)
        {
            //搜索产讯是从历史客人中查询数据，方便录入数据，而GuestInfoCN存的就是历史客人数据，
            //RoomRegGuestInfoCN存的是当前登记但的客人数据，所以完全没毛病
            if (type == "cn")
            {
                var pager = GuestInfoCNBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, key, value);
                return JsonConvert.SerializeObject(pager);
            }
            else
            {
                var pager = GuestInfoENBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, key, value);
                return JsonConvert.SerializeObject(pager);
            }
        }

        /// <summary>
        /// 特殊事项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="xh"></param>
        /// <param name="tx"></param>
        /// <param name="sh"></param>
        /// <param name="zw"></param>
        /// <param name="sw"></param>
        /// <param name="ts"></param>
        /// <param name="qt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult Tssx(long id, string xh, string tx, string sh, string zw, string sw, string ts, string qt, string type)
        {
            var apiResult = new APIResult();
            try
            {
                if (type == "cn")
                    GuestInfoCNBll.Tssx(id, xh, tx, sh, zw, sw, ts, qt);
                else
                    GuestInfoENBll.Tssx(id, xh, tx, sh, zw, sw, ts, qt);
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

        #region 预订

        /// <summary>
        /// 保存--境内
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegGuestInfoCN) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditCNYd(RoomRegGuestInfoCN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                RoomRegGuestInfoCNBll.AddOrUpdateYd(model, UserContext.CurrentUser.HotelId, ref code);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if (code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }

        /// <summary>
        /// 保存--境外
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegGuestInfoEN) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditENYd(RoomRegGuestInfoEN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                RoomRegGuestInfoENBll.AddOrUpdateYd(model, UserContext.CurrentUser.HotelId, ref code);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if (code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }

        #endregion
    }
}