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
    public class RoomRegGoodsController : AdminBaseController
    {
        // GET: RoomRegGoods

        /// <summary>
        /// 商品费用只做新增
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public ActionResult Index(long roomRegId, long itemId = 0)
        {
            if (itemId == 0)
            {
                var roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg == null)
                    roomReg = new Model.RoomReg();
                var model = new RoomRegGoods()
                {
                    Id = 0,
                    RoomRegId = roomRegId,
                    RoomNO = roomReg.RoomNO,
                    Name = roomReg.Name,
                    Sex = roomReg.Sex
                };
                ////绑定商品分类表格
                var details = RoomRegGoodsDetailsBll.GetDetails(UserContext.CurrentUser.HotelId);
                //绑定商品列表
                //再生 保命 舍生 灵敏 + 铁壁 反震 
                //ViewBag.Details = new List<RoomRegGoodsDetails>();
                ViewBag.Details = details;
                return View(model);
            }

            else
            {
                var detail = RoomRegGoodsDetailsBll.SingleOrDefault(itemId);
                //获取主表数据
                var model = new RoomRegGoods();
                if (detail != null)
                    model = RoomRegGoodsBll.SingleOrDefault(detail.ZbId);
                if (model == null)
                    model = new RoomRegGoods();
                //绑定商品分类表格
                var details = RoomRegGoodsDetailsBll.GetDetails(UserContext.CurrentUser.HotelId, model.Id);
                ViewBag.Details = details;
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult AddOrUpdate(RoomRegGoods model, List<RoomRegGoodsDetails> details)
        {
            var user = UserContext.CurrentUser;
            var apiResult = new APIResult();
            try
            {
                RoomRegGoodsBll.AddOrUpdate(model, details, user.Id, user.Name, user.HotelId);
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
                RoomRegGoodsDetailsBll.DeleteById(id);
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