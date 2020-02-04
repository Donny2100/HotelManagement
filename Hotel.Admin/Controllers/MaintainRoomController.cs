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
    public class MaintainRoomController : AdminBaseController
    {
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        

        public ActionResult GetPage(int page, int rows, string EndStartDate, string EndEndDate, string RoomNO, bool isEnd = false)
        {
            var data = MaintainRoomBll.GetPager(page,rows,isEnd, EndStartDate,EndEndDate, RoomNO, UserContext.CurrentUser.HotelId);
            return Json(data);
        }

        public ActionResult EndMaRoomList(long[] ids)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MaintainRoomBll.EndMaRooms(ids);
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