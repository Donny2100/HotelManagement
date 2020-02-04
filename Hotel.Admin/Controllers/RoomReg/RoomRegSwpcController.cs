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
using Newtonsoft.Json;

namespace Hotel.Admin.Controllers
{
    public class RoomRegSwpcController : AdminBaseController
    {
        // GET: RoomRegSwpc

        /// <summary>
        /// 损物赔偿 费用只做新增
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public ActionResult Index(long roomRegId, long itemId =0)
        {
            if (itemId == 0)
            {
                var roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg == null)
                    roomReg = new Model.RoomReg();
                var model = new RoomRegSwpc()
                {
                    Id = 0,
                    RoomRegId = roomRegId,
                    RoomNO = roomReg.RoomNO,
                    Name = roomReg.Name,
                    Sex = roomReg.Sex
                };
                ////绑定损物赔偿分类表格
                var details = RoomRegSwpcDetailsBll.GetDetails(UserContext.CurrentUser.HotelId);
                //var details = new List<RoomRegSwpcDetails>();
                ViewBag.Details = details;
                return View(model);
            }
            else
            {
                var detail = RoomRegSwpcDetailsBll.SingleOrDefault(itemId);

                //获取主表数据
                var model = new RoomRegSwpc();
                if (detail != null)
                    model = RoomRegSwpcBll.SingleOrDefault(detail.ZbId);
                if (model == null)
                    model = new RoomRegSwpc();
                //绑定损物赔偿分类表格
                var details = RoomRegSwpcDetailsBll.GetDetails(UserContext.CurrentUser.HotelId, model.Id);
                ViewBag.Details = details;
                return View(model);
            }
        }

        public ActionResult Index2()
        {
            var roomReg = new Model.RoomReg();
            var model = new RoomRegSwpc()
            {
                Id = 0,
                RoomRegId = 0,
                RoomNO = roomReg.RoomNO,
                Name = roomReg.Name,
                Sex = roomReg.Sex
            };
            ////绑定损物赔偿分类表格
            var details = RoomRegSwpcDetailsBll.GetDetails(UserContext.CurrentUser.HotelId);
            //var details = new List<RoomRegSwpcDetails>();
            ViewBag.Details = details;
            return View(model);
        }
        public ActionResult _SelHotel()
        {
            return View();
        }

        public ActionResult _SelHotel2()
        {
            return View();
        }
        public string GetHotelPager(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetPagerBySearch(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);
            return JsonConvert.SerializeObject(pager);
        }
        [HttpPost]
        public JsonResult AddOrUpdate(RoomRegSwpc model, List<RoomRegSwpcDetails> details)
        {
            var user = UserContext.CurrentUser;
            var apiResult = new APIResult();
            try
            {
                RoomRegSwpcBll.AddOrUpdate(model, details, user.Id, user.Name, user.HotelId);
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
                RoomRegSwpcDetailsBll.DeleteById(id);
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