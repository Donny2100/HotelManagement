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
    public class PosController : AdminBaseController
    {

        public ActionResult Log(long consumeId)
        {
            ViewBag.ConsumeId = consumeId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetLogPager(int page, int rows, long consumeId)
        {
            var pager = PosLogBll.GetPager(page, rows, consumeId);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult Index(string id)
        {

            var o = PosDefineBll.GetByProjectNo(id);
            
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = o.Id;

            var types = PosCatBll.GetList(UserContext.CurrentUser.HotelId);
            ViewBag.Types = types;

            return View();
        }

        public ActionResult Input(long roomRegId)
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.RoomRegId = roomRegId;

            ViewBag.Room = RoomRegBll.GetById(roomRegId);
         

            return View();
        }

        

        public ActionResult InputBody(long posId, long roomRegId)
        { 
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.RoomRegId = roomRegId;
            ViewBag.PosId = posId;

            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var types = PosCatBll.GetListByPos(posId);
            ViewBag.Types = types;
            ViewBag.Room = RoomRegBll.GetById(roomRegId);
            ViewBag.CurrentOrderNo = PosConsumeBll.GetNewOrderNo(posId);
            var o = PosConsumeBll.GetById(posId);
            return View(new PosConsume()
            {
                OrderNo = PosConsumeBll.GetNewOrderNo(posId),
                OpUserName = UserContext.CurrentUser.UserName,
                RoomRegId = roomRegId,
                PosId = posId
            });
        }
      
    }
}