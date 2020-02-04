using Hotel.Bll;
using Hotel.Model;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    /// <summary>
    /// 房态图
    /// </summary>
    public partial class RoomPatternController : AdminBaseController
    {
        // GET: RoomPattern
        public ActionResult Index_old(long buildId = 0, long floorId = 0, long roomTypeId = 0, int roomState = 0)
        {
            var hotelId = UserContext.CurrentUser.HotelId;

            var roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);

            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<Model.RoomType>();

            var models = RoomPatternBll.GetList(hotelId, buildId, floorId, roomTypeId, (FjStateEnum)roomState);
            ViewBag.HotelId = hotelId;
            ViewBag.RoomSet = roomSet;
            ViewBag.RoomTypeList = roomTypeList;

            //   StringBuilder content = new StringBuilder();
            //   if (models != null && models.Count > 0)
            //   {
            //       foreach (var item in models)
            //       {
            //           content.Append($@"<div data_id='{item.Id}' class='room' style='width:{roomSet.RoomWidth};height:{roomSet.RoomHeight};margin:{roomSet.IconMargin};'>
            //                       <div style='color:{roomSet.RoomNoColor};font-size:{roomSet.RoomNoFontSize};'>{item.RoomNO}</div>
            //                       <div style='margin-top:10px;
            //color:{roomSet.RoomTypeColor};font-size:{roomSet.RoomTypeFontSize};'>{item.RoomTypeName}</div>
            //                           <div style='float: right;position: absolute;bottom: 10px;right:10px;
            //color:{roomSet.RoomPriceColor};font-size:{roomSet.RoomPriceFontSize};'>{item.Price}</div>
            //                       </div>");
            //       }
            //   }

            return View(models);
        }

        public JsonResult GetRoomList(long buildId = 0, long floorId = 0, long roomTypeId = 0, int roomState = 0)
        {
            var hotelId = UserContext.CurrentUser.HotelId;

            var roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);



            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<Model.RoomType>();


            var models = RoomPatternBll.GetList(hotelId, buildId, floorId, roomTypeId, (FjStateEnum)roomState);

            StringBuilder content = new StringBuilder();
            if (models != null && models.Count > 0)
            {
                foreach (var item in models)
                {
                    string background_color = "goldenrod";
                    content.Append($@"

                                <div data_id='{item.Id}' class='room' style='width:{roomSet.RoomWidth}px;height:{roomSet.RoomHeight}px;margin:{roomSet.IconMargin}px;background-color:{background_color}'>
                                <div style='color:{roomSet.RoomNoColor};font-size:{roomSet.RoomNoFontSize}px;'>{item.RoomNO}</div>
                                <div style='margin-top:10px;
									color:{roomSet.RoomTypeColor};font-size:{roomSet.RoomTypeFontSize}px;'>{item.RoomTypeName}</div>
                                    <div style='float: right;position: absolute;bottom: 10px;right:10px;
									color:{roomSet.RoomPriceColor};font-size:{roomSet.RoomPriceFontSize}px;'>{item.Price}</div>
                                </div>");
                }
            }

            return Json(new { content = content.ToString() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 设置为干净房
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public JsonResult SetToClean(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.SetToClean(roomId);
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