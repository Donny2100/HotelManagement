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
    public class DisposableMaterialController : AdminBaseController
    {
        // GET: DisposableMaterial
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Setting(long RoomTypeId)
        {
            ViewBag.RoomTypeId = RoomTypeId;
            return View();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new DisposableMaterial());
            var info = DisposableMaterialBll.GetById(id);
            return View(info);
        }



        [HttpPost]
        public JsonResult SaveQuantity(string dataJson,long roomTypeId)
        {
            var apiResult = new APIResult();
            try
            {
                var data = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DisposableMaterial>>(dataJson);

                DisposableMaterialBll.SaveQuantity(data, roomTypeId);
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
        public string GetList()
        {
            var models = DisposableMaterialBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetListA(long roomTypeId)
        {
            var models = DisposableMaterialBll.GetList(UserContext.CurrentUser.HotelId);
            models = models.Where(a => a.IsChangeEveryday).ToList();


            LoadQuantity(models, roomTypeId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetListB(long roomTypeId)
        {
            var models = DisposableMaterialBll.GetList(UserContext.CurrentUser.HotelId);
            models = models.Where(a => !a.IsChangeEveryday).ToList();

            LoadQuantity(models, roomTypeId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetListA_Room(long roomId)
        {
            var room = RoomBll.GetById(roomId);
            var models = DisposableMaterialBll.GetList(UserContext.CurrentUser.HotelId);
            models = models.Where(a => a.IsChangeEveryday).ToList();


            LoadQuantity(models, room.RoomTypeId);
            LoadRoomQuantity(models, roomId);
            return JsonConvert.SerializeObject(models);
        }
        public string GetListB_Room(long roomId)
        {
            var room = RoomBll.GetById(roomId);
            var models = DisposableMaterialBll.GetList(UserContext.CurrentUser.HotelId);
            models = models.Where(a => !a.IsChangeEveryday).ToList();

            LoadQuantity(models, room.RoomTypeId);
            LoadRoomQuantity(models, roomId);
            return JsonConvert.SerializeObject(models);
        }
        private void LoadQuantity(List<DisposableMaterial> list, long roomTypeId)
        {
            var settting = DisposableMaterialBll.GetSetting(roomTypeId);

            foreach(var item in list)
            {
                var o = settting.Where(a => a.MaterialId == item.Id).FirstOrDefault();
                item.Quantity = o == null ? 0 : o.Number;
            }

        }
        private void LoadRoomQuantity(List<DisposableMaterial> list, long roomId)
        {
            var settting = DisposableMaterialBll.GetRoomSetting(roomId);

            foreach (var item in list)
            {
                var o = settting.Where(a => a.MaterialId == item.Id).FirstOrDefault();
                item.RoomQuantity = o == null ? item.Quantity : o.Number;
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(DisposableMaterial) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(DisposableMaterial model)
        {
            var apiResult = new APIResult();
            try
            {
                DisposableMaterialBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                DisposableMaterialBll.DeleteById(id);
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